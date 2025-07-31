using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using McpUnity.Utils;

namespace McpUnity.Tools
{
    /// <summary>
    /// Tool for loading Unity scenes
    /// </summary>
    public class LoadSceneTool : McpToolBase
    {
        public LoadSceneTool()
        {
            Name = "load_scene";
            Description = "Loads a Unity scene by path or name";
            IsAsync = true;
        }

        public override void ExecuteAsync(JObject parameters, System.Threading.Tasks.TaskCompletionSource<JObject> tcs)
        {
            try
            {
                var result = LoadScene(parameters);
                tcs.TrySetResult(result);
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"LoadSceneTool failed: {ex.Message}");
                tcs.TrySetException(ex);
            }
        }

        private JObject LoadScene(JObject parameters)
        {
            try
            {
                // Extract parameters
                string scenePath = parameters["scenePath"]?.ToString();
                string sceneName = parameters["sceneName"]?.ToString();
                string loadMode = parameters["loadMode"]?.ToString() ?? "single"; // single, additive
                bool saveCurrentScene = parameters["saveCurrentScene"]?.ToObject<bool>() ?? true;

                if (string.IsNullOrEmpty(scenePath) && string.IsNullOrEmpty(sceneName))
                {
                    return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                        "Either scenePath or sceneName is required",
                        "invalid_parameter"
                    );
                }

                // Resolve scene path if only name is provided
                if (string.IsNullOrEmpty(scenePath) && !string.IsNullOrEmpty(sceneName))
                {
                    scenePath = FindScenePathByName(sceneName);
                    if (string.IsNullOrEmpty(scenePath))
                    {
                        return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                            $"Scene not found: {sceneName}",
                            "scene_not_found"
                        );
                    }
                }

                // Check if scene exists
                if (!System.IO.File.Exists(scenePath))
                {
                    return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                        $"Scene file does not exist: {scenePath}",
                        "scene_not_found"
                    );
                }

                // Save current scene if requested
                if (saveCurrentScene)
                {
                    Scene currentScene = SceneManager.GetActiveScene();
                    if (currentScene.isDirty)
                    {
                        bool saved = EditorSceneManager.SaveScene(currentScene);
                        if (!saved)
                        {
                            McpLogger.LogWarning("Failed to save current scene before loading new scene");
                        }
                    }
                }

                // Determine load mode
                OpenSceneMode openMode = loadMode.ToLower() == "additive" 
                    ? OpenSceneMode.Additive 
                    : OpenSceneMode.Single;

                // Load the scene
                Scene loadedScene = EditorSceneManager.OpenScene(scenePath, openMode);
                
                if (!loadedScene.IsValid())
                {
                    return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                        "Failed to load scene",
                        "load_failed"
                    );
                }

                // Set as active scene if loaded in single mode
                if (openMode == OpenSceneMode.Single)
                {
                    SceneManager.SetActiveScene(loadedScene);
                }

                McpLogger.LogInfo($"Scene loaded successfully: {scenePath}");

                // Return success response
                var response = new JObject
                {
                    ["success"] = true,
                    ["scenePath"] = scenePath,
                    ["sceneName"] = loadedScene.name,
                    ["loadMode"] = loadMode,
                    ["isActive"] = SceneManager.GetActiveScene() == loadedScene,
                    ["buildIndex"] = loadedScene.buildIndex,
                    ["message"] = $"Scene '{loadedScene.name}' loaded successfully"
                };

                return response;
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error loading scene: {ex.Message}");
                return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                    $"Error loading scene: {ex.Message}",
                    "load_error"
                );
            }
        }

        private string FindScenePathByName(string sceneName)
        {
            try
            {
                // Search for scene in Assets folder
                string[] guids = AssetDatabase.FindAssets($"{sceneName} t:Scene");
                
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                    
                    if (fileName.Equals(sceneName, StringComparison.OrdinalIgnoreCase))
                    {
                        return path;
                    }
                }

                // Also check build settings
                foreach (var buildScene in EditorBuildSettings.scenes)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(buildScene.path);
                    if (fileName.Equals(sceneName, StringComparison.OrdinalIgnoreCase))
                    {
                        return buildScene.path;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error searching for scene: {ex.Message}");
                return null;
            }
        }
    }
}