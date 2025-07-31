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
    /// Tool for creating new Unity scenes
    /// </summary>
    public class CreateSceneTool : McpToolBase
    {
        public CreateSceneTool()
        {
            Name = "create_scene";
            Description = "Creates a new Unity scene with specified name and optional template";
            IsAsync = true;
        }

        public override void ExecuteAsync(JObject parameters, System.Threading.Tasks.TaskCompletionSource<JObject> tcs)
        {
            try
            {
                var result = CreateScene(parameters);
                tcs.TrySetResult(result);
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"CreateSceneTool failed: {ex.Message}");
                tcs.TrySetException(ex);
            }
        }

        private JObject CreateScene(JObject parameters)
        {
            try
            {
                // Extract parameters
                string sceneName = parameters["sceneName"]?.ToString();
                string scenePath = parameters["scenePath"]?.ToString();
                string templateType = parameters["templateType"]?.ToString() ?? "empty"; // empty, basic, 2d, 3d
                bool setActive = parameters["setActive"]?.ToObject<bool>() ?? true;
                bool addToBuildSettings = parameters["addToBuildSettings"]?.ToObject<bool>() ?? false;

                if (string.IsNullOrEmpty(sceneName))
                {
                    return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                        "Scene name is required",
                        "invalid_parameter"
                    );
                }

                // Default path if not provided
                if (string.IsNullOrEmpty(scenePath))
                {
                    scenePath = $"Assets/Scenes/{sceneName}.unity";
                }
                else if (!scenePath.EndsWith(".unity"))
                {
                    scenePath += ".unity";
                }

                // Ensure directory exists
                string directoryPath = Path.GetDirectoryName(scenePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    AssetDatabase.Refresh();
                }

                // Check if scene already exists
                if (File.Exists(scenePath))
                {
                    return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                        $"Scene already exists at path: {scenePath}",
                        "scene_exists"
                    );
                }

                // Create new scene
                Scene newScene = CreateSceneWithTemplate(templateType);
                
                // Save the scene
                bool saved = EditorSceneManager.SaveScene(newScene, scenePath);
                
                if (!saved)
                {
                    return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                        "Failed to save scene",
                        "save_failed"
                    );
                }

                McpLogger.LogInfo($"Scene created successfully: {scenePath}");

                // Add to build settings if requested
                if (addToBuildSettings)
                {
                    AddSceneToBuildSettings(scenePath);
                }

                // Set as active scene if requested
                if (setActive)
                {
                    SceneManager.SetActiveScene(newScene);
                }

                // Refresh asset database
                AssetDatabase.Refresh();

                // Return success response
                var response = new JObject
                {
                    ["success"] = true,
                    ["scenePath"] = scenePath,
                    ["sceneName"] = sceneName,
                    ["templateType"] = templateType,
                    ["isActive"] = setActive,
                    ["addedToBuildSettings"] = addToBuildSettings,
                    ["message"] = $"Scene '{sceneName}' created successfully at {scenePath}"
                };

                return response;
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error creating scene: {ex.Message}");
                return McpUnity.Unity.McpUnitySocketHandler.CreateErrorResponse(
                    $"Error creating scene: {ex.Message}",
                    "creation_error"
                );
            }
        }

        private Scene CreateSceneWithTemplate(string templateType)
        {
            Scene newScene;
            
            switch (templateType.ToLower())
            {
                case "basic":
                    newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                    break;
                case "empty":
                    newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                    break;
                case "2d":
                    newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                    // Configure for 2D
                    if (Camera.main != null)
                    {
                        Camera.main.orthographic = true;
                        Camera.main.orthographicSize = 5f;
                    }
                    break;
                case "3d":
                default:
                    newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
                    // Configure for 3D (default camera settings)
                    if (Camera.main != null)
                    {
                        Camera.main.orthographic = false;
                        Camera.main.fieldOfView = 60f;
                    }
                    break;
            }

            return newScene;
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
                else
                {
                    McpLogger.LogInfo($"Scene already exists in build settings: {scenePath}");
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Failed to add scene to build settings: {ex.Message}");
            }
        }
    }
}