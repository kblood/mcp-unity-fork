using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using McpUnity.Utils;

namespace McpUnity.Resources
{
    /// <summary>
    /// Resource for retrieving information about Unity scenes
    /// </summary>
    public class GetScenesResource : McpResourceBase
    {
        public GetScenesResource()
        {
            Name = "get_scenes";
            Description = "Retrieves information about Unity scenes including open scenes, scenes in build settings, and all scene assets";
            Uri = "unity://scenes/{sceneType}";
            IsAsync = false;
        }

        public override JObject Fetch(JObject parameters)
        {
            try
            {
                // Extract scene type from parameters
                string sceneType = parameters?["sceneType"]?.ToString() ?? "all";

                var result = new JObject();

                switch (sceneType.ToLower())
                {
                    case "open":
                        result["openScenes"] = GetOpenScenes();
                        break;
                    case "build":
                        result["buildScenes"] = GetBuildScenes();
                        break;
                    case "assets":
                        result["sceneAssets"] = GetSceneAssets();
                        break;
                    case "all":
                    default:
                        result["openScenes"] = GetOpenScenes();
                        result["buildScenes"] = GetBuildScenes();
                        result["sceneAssets"] = GetSceneAssets();
                        result["activeScene"] = GetActiveSceneInfo();
                        break;
                }

                result["success"] = true;
                result["message"] = $"Retrieved scene information for type: {sceneType}";
                return result;
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error getting scenes resource: {ex.Message}");
                return new JObject
                {
                    ["success"] = false,
                    ["error"] = ex.Message,
                    ["message"] = $"Failed to retrieve scene information: {ex.Message}"
                };
            }
        }

        private JArray GetOpenScenes()
        {
            var openScenes = new JArray();

            try
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.IsValid())
                    {
                        var sceneInfo = new JObject
                        {
                            ["name"] = scene.name,
                            ["path"] = scene.path,
                            ["buildIndex"] = scene.buildIndex,
                            ["isLoaded"] = scene.isLoaded,
                            ["isDirty"] = scene.isDirty,
                            ["isActive"] = SceneManager.GetActiveScene() == scene,
                            ["rootCount"] = scene.rootCount
                        };
                        openScenes.Add(sceneInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error getting open scenes: {ex.Message}");
            }

            return openScenes;
        }

        private JArray GetBuildScenes()
        {
            var buildScenes = new JArray();

            try
            {
                var scenes = EditorBuildSettings.scenes;
                for (int i = 0; i < scenes.Length; i++)
                {
                    var buildScene = scenes[i];
                    var sceneInfo = new JObject
                    {
                        ["path"] = buildScene.path,
                        ["enabled"] = buildScene.enabled,
                        ["buildIndex"] = i,
                        ["guid"] = buildScene.guid.ToString(),
                        ["name"] = Path.GetFileNameWithoutExtension(buildScene.path),
                        ["exists"] = File.Exists(buildScene.path)
                    };
                    buildScenes.Add(sceneInfo);
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error getting build scenes: {ex.Message}");
            }

            return buildScenes;
        }

        private JArray GetSceneAssets()
        {
            var sceneAssets = new JArray();

            try
            {
                // Find all scene assets in the project
                string[] guids = AssetDatabase.FindAssets("t:Scene");
                
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    
                    if (!string.IsNullOrEmpty(path))
                    {
                        var sceneInfo = new JObject
                        {
                            ["name"] = Path.GetFileNameWithoutExtension(path),
                            ["path"] = path,
                            ["guid"] = guid,
                            ["directory"] = Path.GetDirectoryName(path),
                            ["size"] = GetFileSize(path),
                            ["lastModified"] = GetLastModifiedTime(path),
                            ["inBuildSettings"] = IsInBuildSettings(path)
                        };
                        sceneAssets.Add(sceneInfo);
                    }
                }

                // Sort by name
                var sortedAssets = sceneAssets.OrderBy(s => s["name"]?.ToString()).ToArray();
                sceneAssets.Clear();
                foreach (var asset in sortedAssets)
                {
                    sceneAssets.Add(asset);
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error getting scene assets: {ex.Message}");
            }

            return sceneAssets;
        }

        private JObject GetActiveSceneInfo()
        {
            try
            {
                Scene activeScene = SceneManager.GetActiveScene();
                if (activeScene.IsValid())
                {
                    return new JObject
                    {
                        ["name"] = activeScene.name,
                        ["path"] = activeScene.path,
                        ["buildIndex"] = activeScene.buildIndex,
                        ["isLoaded"] = activeScene.isLoaded,
                        ["isDirty"] = activeScene.isDirty,
                        ["rootCount"] = activeScene.rootCount
                    };
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogError($"Error getting active scene info: {ex.Message}");
            }

            return new JObject { ["error"] = "No active scene" };
        }

        private long GetFileSize(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return new FileInfo(path).Length;
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogWarning($"Could not get file size for {path}: {ex.Message}");
            }
            return 0;
        }

        private string GetLastModifiedTime(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return File.GetLastWriteTime(path).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                McpLogger.LogWarning($"Could not get last modified time for {path}: {ex.Message}");
            }
            return "Unknown";
        }

        private bool IsInBuildSettings(string path)
        {
            try
            {
                return EditorBuildSettings.scenes.Any(s => s.path.Equals(path, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                McpLogger.LogWarning($"Could not check build settings for {path}: {ex.Message}");
                return false;
            }
        }
    }
}