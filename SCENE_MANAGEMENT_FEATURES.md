# 🎬 Scene Management Features - MCP Unity Enhanced

This fork adds comprehensive scene management capabilities to MCP Unity, allowing AI assistants to create, load, save, and manage Unity scenes programmatically.

## 🆕 New Tools

### `create_scene` - Create New Unity Scenes
Creates a new Unity scene with specified name and optional template.

**Parameters:**
- `sceneName` (required): The name of the scene to create
- `scenePath` (optional): The path where the scene should be saved (defaults to Assets/Scenes/{sceneName}.unity)
- `templateType` (optional): The type of scene template to use - `empty`, `basic`, `2d`, `3d` (default: empty)
- `setActive` (optional): Whether to set the new scene as the active scene (default: true)
- `addToBuildSettings` (optional): Whether to add the scene to build settings (default: false)

**Example prompts:**
- "Create a new empty scene called 'MainMenu'"
- "Create a 3D scene called 'GameLevel1' and add it to build settings"
- "Create a 2D scene at Assets/Levels/Level1.unity"

### `load_scene` - Load Unity Scenes
Loads a Unity scene by path or name.

**Parameters:**
- `scenePath` (optional): The path of the scene to load
- `sceneName` (optional): The name of the scene to load (alternative to scenePath)
- `loadMode` (optional): The load mode - `single` or `additive` (default: single)
- `saveCurrentScene` (optional): Whether to save the current scene before loading (default: true)

**Example prompts:**
- "Load the scene called 'MainMenu'"
- "Load Assets/Scenes/Level1.unity in additive mode"
- "Switch to the GameLevel scene without saving current changes"

### `save_scene` - Save Unity Scenes
Saves the current Unity scene or all open scenes.

**Parameters:**
- `saveAll` (optional): Whether to save all open scenes (default: false)
- `scenePath` (optional): The path of a specific scene to save
- `saveAs` (optional): Whether to save the scene as a new file (default: false)
- `addToBuildSettings` (optional): Whether to add to build settings when using saveAs (default: false)

**Example prompts:**
- "Save the current scene"
- "Save all open scenes"
- "Save the current scene as Assets/Backup/SceneBackup.unity"

## 🆕 New Resource

### `unity://scenes` - Scene Information
Retrieves comprehensive information about Unity scenes.

**Resource URIs:**
- `unity://scenes` - All scene information (open, build settings, assets, active scene)
- `unity://scenes/open` - Currently open scenes only
- `unity://scenes/build` - Scenes in build settings only
- `unity://scenes/assets` - All scene assets in project only

**Example prompts:**
- "Show me all scenes in the project"
- "List currently open scenes"
- "What scenes are in the build settings?"
- "Show me all scene assets with their file sizes"

## 🚀 Usage Examples

### Creating a Complete Game Structure
```
"Create three scenes: 'MainMenu', 'GameLevel1', and 'GameOver'. 
Add them all to build settings. Set MainMenu as a 2D scene and 
GameLevel1 as a 3D scene."
```

### Scene Workflow Management
```
"Load the GameLevel1 scene, make some modifications to objects, 
then save it. After that, create a backup by saving it as 
GameLevel1_Backup.unity"
```

### Project Analysis
```
"Show me all scenes in the project, then tell me which ones are 
currently open and which ones are in the build settings"
```

## 🎯 Benefits

### For Game Development
- **Rapid Prototyping**: Quickly create test scenes for different game mechanics
- **Level Management**: Efficiently organize and manage game levels
- **Build Pipeline**: Automatically manage which scenes are included in builds
- **Version Control**: Create scene backups and variants programmatically

### for AI-Assisted Development
- **Scene Templates**: AI can create scenes with predefined setups
- **Workflow Automation**: Automate common scene management tasks
- **Project Organization**: AI can analyze and organize scene structures
- **Testing Support**: Create temporary test scenes for specific features

## 🔧 Technical Implementation

### C# Unity Editor Tools
- `CreateSceneTool.cs` - Handles scene creation with templates
- `LoadSceneTool.cs` - Manages scene loading with different modes
- `SaveSceneTool.cs` - Handles scene saving and backup operations
- `GetScenesResource.cs` - Provides comprehensive scene information

### TypeScript MCP Server Tools
- `createSceneTool.ts` - MCP interface for scene creation
- `loadSceneTool.ts` - MCP interface for scene loading  
- `saveSceneTool.ts` - MCP interface for scene saving
- `getScenesResource.ts` - MCP interface for scene information

### Features
- ✅ **Async Operations**: All tools use async patterns for non-blocking execution
- ✅ **Error Handling**: Comprehensive error handling with meaningful messages
- ✅ **Validation**: Parameter validation with clear error messages
- ✅ **Build Integration**: Automatic build settings management
- ✅ **Template Support**: Multiple scene templates (empty, basic, 2D, 3D)
- ✅ **Flexible Loading**: Single and additive scene loading modes
- ✅ **Batch Operations**: Save all scenes at once
- ✅ **Resource Queries**: Detailed scene information and metadata

## 🌟 Comparison with Original MCP Unity

| Feature | Original MCP Unity | Enhanced MCP Unity |
|---------|-------------------|-------------------|
| Scene Creation | ❌ Not supported | ✅ Full scene creation with templates |
| Scene Loading | ❌ Not supported | ✅ Load by name/path, single/additive modes |
| Scene Saving | ❌ Not supported | ✅ Save current/all/saveAs operations |
| Scene Information | ❌ Limited hierarchy only | ✅ Comprehensive scene metadata |
| Build Settings | ❌ Not supported | ✅ Automatic build settings management |
| Scene Templates | ❌ Not supported | ✅ Empty, Basic, 2D, 3D templates |

## 🎮 Perfect for Game Development

This enhanced MCP Unity is particularly powerful for:

- **Rapid Game Prototyping** - Quickly create and test game ideas
- **Educational Game Development** - AI can help students learn scene management  
- **Procedural Content Creation** - Generate scenes programmatically
- **Team Collaboration** - Standardize scene creation and management workflows
- **Testing Automation** - Create test scenes for automated testing scenarios

---

*This enhanced version maintains full compatibility with the original MCP Unity while adding powerful scene management capabilities that make Unity development with AI assistants even more productive.*