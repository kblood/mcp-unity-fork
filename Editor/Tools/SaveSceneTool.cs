using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using McpUnity.Utils;

namespace McpUnity.Tools
{
    /// <summary>
    /// Tool for saving Unity scenes
    /// </summary>
    public class SaveSceneTool : McpToolBase
    {
        public SaveSceneTool()
        {
            Name = "save_scene";
            Description = "Saves the current Unity scene or all open scenes";
            IsAsync = true;
        }

        public override void ExecuteAsync(JObject parameters, System.Threading.Tasks.TaskCompletionSource<JObject> tcs)
        {
            try
            {
                var result = SaveScene(parameters);
                tcs.TrySetResult(result);
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"SaveSceneTool failed: {ex.Message}");
                tcs.TrySetException(ex);
            }
        }

        private JObject SaveScene(JObject parameters)
        {
            try
            {
                // Extract parameters
                bool saveAll = parameters["saveAll"]?.ToObject<bool>() ?? false;
                string scenePath = parameters["scenePath"]?.ToString();
                bool saveAs = parameters["saveAs"]?.ToObject<bool>() ?? false;
                bool addToBuildSettings = parameters["addToBuildSettings"]?.ToObject<bool>() ?? false;

                JArray savedScenes = new JArray();
                bool success = true;
                string errorMessage = "";

                if (saveAll)
                {
                    // Save all open scenes - handle unsaved scenes programmatically
                    bool allSaved = SaveAllScenesWithoutDialogs();
                    
                    if (allSaved)
                    {
                        // Get info about all saved scenes
                        for (int i = 0; i < SceneManager.sceneCount; i++)
                        {
                            Scene scene = SceneManager.GetSceneAt(i);
                            if (scene.IsValid())
                            {
                                var sceneInfo = new JObject
                                {
                                    ["name"] = scene.name,
                                    ["path"] = scene.path,
                                    ["isDirty"] = scene.isDirty,
                                    ["isActive"] = SceneManager.GetActiveScene() == scene
                                };
                                savedScenes.Add(sceneInfo);
                            }
                        }
                        McpLogger.LogInfo("All open scenes saved successfully");
                    }
                    else
                    {
                        success = false;
                        errorMessage = "Failed to save all open scenes";
                    }
                }
                else
                {
                    // Save specific scene or current active scene
                    Scene targetScene;
                    
                    if (saveAs)
                    {
                        // For save-as, always use the active scene
                        targetScene = SceneManager.GetActiveScene();
                        if (!targetScene.IsValid())
                        {
                            return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                                "No active scene to save",
                                "no_active_scene"
                            );
                        }
                    }
                    else if (!string.IsNullOrEmpty(scenePath))
                    {
                        // Find scene by path
                        targetScene = default(Scene);
                        for (int i = 0; i < SceneManager.sceneCount; i++)
                        {
                            Scene scene = SceneManager.GetSceneAt(i);
                            if (scene.path.Equals(scenePath, StringComparison.OrdinalIgnoreCase))
                            {
                                targetScene = scene;
                                break;
                            }
                        }

                        if (!targetScene.IsValid())
                        {
                            return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                                $"Scene not found in open scenes: {scenePath}",
                                "scene_not_found"
                            );
                        }
                    }
                    else
                    {
                        // Use active scene
                        targetScene = SceneManager.GetActiveScene();
                        if (!targetScene.IsValid())
                        {
                            return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                                "No active scene to save",
                                "no_active_scene"
                            );
                        }
                    }

                    bool saved = false;
                    
                    if (saveAs && !string.IsNullOrEmpty(scenePath))
                    {
                        // Ensure directory exists for save-as
                        string directoryPath = Path.GetDirectoryName(scenePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                            AssetDatabase.Refresh();
                        }
                        
                        // Save scene as new file
                        saved = EditorSceneManager.SaveScene(targetScene, scenePath);
                        
                        if (saved && addToBuildSettings)
                        {
                            AddSceneToBuildSettings(scenePath);
                        }
                    }
                    else
                    {
                        // Save existing scene - handle unsaved scenes programmatically
                        if (string.IsNullOrEmpty(targetScene.path))
                        {
                            // Scene has never been saved, generate a path
                            string defaultPath = $"Assets/Scenes/{targetScene.name}.unity";
                            // Ensure directory exists
                            string directoryPath = Path.GetDirectoryName(defaultPath);
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                                AssetDatabase.Refresh();
                            }
                            saved = EditorSceneManager.SaveScene(targetScene, defaultPath);
                        }
                        else
                        {
                            saved = EditorSceneManager.SaveScene(targetScene);
                        }
                    }

                    if (saved)
                    {
                        var sceneInfo = new JObject
                        {
                            ["name"] = targetScene.name,
                            ["path"] = targetScene.path,
                            ["isDirty"] = targetScene.isDirty,
                            ["isActive"] = SceneManager.GetActiveScene() == targetScene
                        };
                        savedScenes.Add(sceneInfo);
                        
                        McpLogger.LogInfo($"Scene saved successfully: {targetScene.path}");
                    }
                    else
                    {
                        success = false;
                        errorMessage = $"Failed to save scene: {targetScene.name}";
                    }
                }

                if (!success)
                {
                    return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                        errorMessage,
                        "save_failed"
                    );
                }

                // Refresh asset database
                AssetDatabase.Refresh();

                // Return success response
                var response = new JObject
                {
                    ["success"] = true,
                    ["savedScenes"] = savedScenes,
                    ["saveAll"] = saveAll,
                    ["message"] = saveAll ? "All scenes saved successfully" : $"Scene saved successfully"
                };

                return response;
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error saving scene: {ex.Message}");
                return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                    $"Error saving scene: {ex.Message}",
                    "save_error"
                );
            }
        }

        private bool SaveAllScenesWithoutDialogs()
        {
            try
            {
                bool allSaved = true;
                
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.IsValid() && scene.isDirty)
                    {
                        if (!string.IsNullOrEmpty(scene.path))
                        {
                            // Scene has a path, save it
                            bool saved = EditorSceneManager.SaveScene(scene);
                            if (!saved)
                            {
                                allSaved = false;
                                McpLogger.LogError($"Failed to save scene: {scene.name}");
                            }
                        }
                        else
                        {
                            // Scene has never been saved, generate a path
                            string defaultPath = $"Assets/Scenes/{scene.name}.unity";
                            
                            // Ensure directory exists
                            string directoryPath = Path.GetDirectoryName(defaultPath);
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                                AssetDatabase.Refresh();
                            }
                            
                            // Make path unique if file already exists
                            int counter = 1;
                            string uniquePath = defaultPath;
                            while (File.Exists(uniquePath))
                            {
                                uniquePath = $"Assets/Scenes/{scene.name}_{counter}.unity";
                                counter++;
                            }
                            
                            bool saved = EditorSceneManager.SaveScene(scene, uniquePath);
                            if (!saved)
                            {
                                allSaved = false;
                                McpLogger.LogError($"Failed to save scene: {scene.name} to {uniquePath}");
                            }
                            else
                            {
                                McpLogger.LogInfo($"Saved unsaved scene: {scene.name} to {uniquePath}");
                            }
                        }
                    }
                }
                
                return allSaved;
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error in SaveAllScenesWithoutDialogs: {ex.Message}");
                return false;
            }
        }
        
        private void AddSceneToBuildSettings(string scenePath)
        {
            try
            {
                var buildScenes = new System.Collections.Generic.List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
                
                // Check if scene is already in build settings
                bool alreadyExists = false;
                foreach (var scene in buildScenes)
                {
                    if (scene.path == scenePath)
                    {
                        alreadyExists = true;
                        break;
                    }
                }

                if (!alreadyExists)
                {
                    buildScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                    EditorBuildSettings.scenes = buildScenes.ToArray();
                    McpLogger.LogInfo($"Scene added to build settings: {scenePath}");
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Failed to add scene to build settings: {ex.Message}");
            }
        }
    }
}