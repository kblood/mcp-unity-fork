# MCP Unity Editor Enhanced (Game Engine) 🎬

[![](https://badge.mcpx.dev?status=on 'MCP Enabled')](https://modelcontextprotocol.io/introduction)
[![](https://img.shields.io/badge/Unity-000000?style=flat&logo=unity&logoColor=white 'Unity')](https://unity.com/releases/editor/archive)
[![](https://img.shields.io/badge/Node.js-339933?style=flat&logo=nodedotjs&logoColor=white 'Node.js')](https://nodejs.org/en/download/)
[![](https://img.shields.io/github/stars/kblood/mcp-unity-fork 'Stars')](https://github.com/kblood/mcp-unity-fork/stargazers)
[![](https://img.shields.io/github/last-commit/kblood/mcp-unity-fork 'Last Commit')](https://github.com/kblood/mcp-unity-fork/commits/main)
[![](https://img.shields.io/badge/License-MIT-red.svg 'MIT License')](https://opensource.org/licenses/MIT)
[![](https://img.shields.io/badge/AI--Generated-Claude%20Code-purple?logo=data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iMjQiIGhlaWdodD0iMjQiIHZpZXdCb3g9IjAgMCAyNCAyNCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPHBhdGggZD0iTTEyIDJMMTMuMDkgOC4yNkwyMCA5TDEzLjA5IDE1Ljc0TDEyIDIyTDEwLjkxIDE1Ljc0TDQgOUwxMC45MSA4LjI2TDEyIDJaIiBmaWxsPSJ3aGl0ZSIvPgo8L3N2Zz4K 'AI Generated with Claude Code')](https://claude.ai/code)

> **🚀 Enhanced Fork**: This is an enhanced version of the original [MCP Unity](https://github.com/CoderGamester/mcp-unity) with comprehensive **Scene Management** capabilities added by [Claude Code](https://claude.ai/code).

| [English](README.md) | [🇨🇳简体中文](README_zh-CN.md) | [🇯🇵日本語](README-ja.md) |
|----------------------|---------------------------------|----------------------|

```                                                                        
                              ,/(/.   *(/,                                  
                          */(((((/.   *((((((*.                             
                     .*((((((((((/.   *((((((((((/.                         
                 ./((((((((((((((/    *((((((((((((((/,                     
             ,/(((((((((((((/*.           */(((((((((((((/*.                
            ,%%#((/((((((*                    ,/(((((/(#&@@(                
            ,%%##%%##((((((/*.             ,/((((/(#&@@@@@@(                
            ,%%######%%##((/(((/*.    .*/(((//(%@@@@@@@@@@@(                
            ,%%####%#(%%#%%##((/((((((((//#&@@@@@@&@@@@@@@@(                
            ,%%####%(    /#%#%%%##(//(#@@@@@@@%,   #@@@@@@@(                
            ,%%####%(        *#%###%@@@@@@(        #@@@@@@@(                
            ,%%####%(           #%#%@@@@,          #@@@@@@@(                
            ,%%##%%%(           #%#%@@@@,          #@@@@@@@(                
            ,%%%#*              #%#%@@@@,             *%@@@(                
            .,      ,/##*.      #%#%@@@@,     ./&@#*      *`                
                ,/#%#####%%#/,  #%#%@@@@, ,/&@@@@@@@@@&\.                    
                 `*#########%%%%###%@@@@@@@@@@@@@@@@@@&*´                   
                    `*%%###########%@@@@@@@@@@@@@@&*´                        
                        `*%%%######%@@@@@@@@@@&*´                            
                            `*#%%##%@@@@@&*´                                 
                               `*%#%@&*´                                     
                                                       
     ███╗   ███╗ ██████╗██████╗         ██╗   ██╗███╗   ██╗██╗████████╗██╗   ██╗
     ████╗ ████║██╔════╝██╔══██╗        ██║   ██║████╗  ██║██║╚══██╔══╝╚██╗ ██╔╝
     ██╔████╔██║██║     ██████╔╝        ██║   ██║██╔██╗ ██║██║   ██║    ╚████╔╝ 
     ██║╚██╔╝██║██║     ██╔═══╝         ██║   ██║██║╚██╗██║██║   ██║     ╚██╔╝  
     ██║ ╚═╝ ██║╚██████╗██║             ╚██████╔╝██║ ╚████║██║   ██║      ██║   
     ╚═╝     ╚═╝ ╚═════╝╚═╝              ╚═════╝ ╚═╝  ╚═══╝╚═╝   ╚═╝      ╚═╝   
```       

MCP Unity Enhanced is an implementation of the Model Context Protocol for Unity Editor, allowing AI assistants to interact with your Unity projects. This package provides a bridge between Unity and a Node.js server that implements the MCP protocol, enabling AI agents like Claude, Windsurf, and Cursor to execute operations within the Unity Editor.

## 🆕 **New Scene Management Features**

This enhanced fork adds comprehensive scene management capabilities:

- **🎬 Create Scenes** with templates (empty, basic, 2D, 3D)
- **📂 Load Scenes** by name or path (single/additive modes)  
- **💾 Save Scenes** (current, all, or save-as operations)
- **📊 Scene Information** (open scenes, build settings, assets)
- **🏗️ Build Integration** (automatic build settings management)

*All scene management features were AI-generated using [Claude Code](https://claude.ai/code) to enhance Unity development workflows.*

<a href="https://glama.ai/mcp/servers/@CoderGamester/mcp-unity">
  <img width="400" height="200" src="https://glama.ai/mcp/servers/@CoderGamester/mcp-unity/badge" alt="Unity MCP server" />
</a>

## Features

### IDE Integration - Package Cache Access

MCP Unity provides automatic integration with VSCode-like IDEs (Visual Studio Code, Cursor, Windsurf) by adding the Unity `Library/PackedCache` folder to your workspace. This feature:

- Improves code intelligence for Unity packages
- Enables better autocompletion and type information for Unity packages
- Helps AI coding assistants understand your project's dependencies

### MCP Server Tools

The following tools are available for manipulating and querying Unity scenes and GameObjects via MCP:

- `execute_menu_item`: Executes Unity menu items (functions tagged with the MenuItem attribute)
  > **Example prompt:** "Execute the menu item 'GameObject/Create Empty' to create a new empty GameObject"

- `select_gameobject`: Selects game objects in the Unity hierarchy by path or instance ID
  > **Example prompt:** "Select the Main Camera object in my scene"

- `update_gameobject`: Updates a GameObject's core properties (name, tag, layer, active/static state), or creates the GameObject if it does not exist
  > **Example prompt:** "Set the Player object's tag to 'Enemy' and make it inactive"

- `update_component`: Updates component fields on a GameObject or adds it to the GameObject if it does not contain the component
  > **Example prompt:** "Add a Rigidbody component to the Player object and set its mass to 5"

- `add_package`: Installs new packages in the Unity Package Manager
  > **Example prompt:** "Add the TextMeshPro package to my project"

- `run_tests`: Runs tests using the Unity Test Runner
  > **Example prompt:** "Run all the EditMode tests in my project"

- `send_console_log`: Send a console log to Unity
  > **Example prompt:** "Send a console log to Unity Editor"

- `add_asset_to_scene`: Adds an asset from the AssetDatabase to the Unity scene
  > **Example prompt:** "Add the Player prefab from my project to the current scene"

#### 🆕 **Scene Management Tools** *(AI-Generated)*

- `create_scene`: Creates a new Unity scene with specified name and optional template
  > **Example prompt:** "Create a new 3D scene called 'GameLevel1' and add it to build settings"

- `load_scene`: Loads a Unity scene by path or name
  > **Example prompt:** "Load the MainMenu scene" or "Load Assets/Scenes/Level1.unity in additive mode"

- `save_scene`: Saves the current Unity scene or all open scenes
  > **Example prompt:** "Save all open scenes" or "Save the current scene as Assets/Backup/SceneBackup.unity"

### MCP Server Resources

- `unity://menu-items`: Retrieves a list of all available menu items in the Unity Editor to facilitate `execute_menu_item` tool
  > **Example prompt:** "Show me all available menu items related to GameObject creation"

- `unity://scenes-hierarchy`: Retrieves a list of all game objects in the current Unity scene hierarchy
  > **Example prompt:** "Show me the current scenes hierarchy structure"

- `unity://gameobject/{id}`: Retrieves detailed information about a specific GameObject by instance ID or object path in the scene hierarchy, including all GameObject components with it's serialized properties and fields
  > **Example prompt:** "Get me detailed information about the Player GameObject"

- `unity://logs`: Retrieves a list of all logs from the Unity console
  > **Example prompt:** "Show me the recent error messages from the Unity console"

- `unity://packages`: Retrieves information about installed and available packages from the Unity Package Manager
  > **Example prompt:** "List all the packages currently installed in my Unity project"

- `unity://assets`: Retrieves information about assets in the Unity Asset Database
  > **Example prompt:** "Find all texture assets in my project"

- `unity://tests/{testMode}`: Retrieves information about tests in the Unity Test Runner
  > **Example prompt:** "List all available tests in my Unity project"

#### 🆕 **Scene Management Resources** *(AI-Generated)*

- `unity://scenes`: Retrieves comprehensive information about Unity scenes
  > **Example prompt:** "Show me all scenes in the project" or "List currently open scenes"
  > **Resource URIs:** `unity://scenes`, `unity://scenes/open`, `unity://scenes/build`, `unity://scenes/assets`

## Requirements
- Unity 2022.3 or later - to [install the server](#install-server)
- Node.js 18 or later - to [start the server](#start-server)
- npm 9 or later - to [debug the server](#debug-server)

> [!IMPORTANT]
> **Project Path Cannot Contain Spaces**
>
> It is crucial that the file path to your Unity project **does not contain any spaces**.
> If your project path includes spaces, the MCP Client (e.g., Cursor, Claude, Windsurf) will fail to connect to the MCP Unity server.
>
> **Examples:**
> -   ✅ **Works:** `C:\Users\YourUser\Documents\UnityProjects\MyAwesomeGame`
> -   ❌ **Fails:** `C:\Users\Your User\Documents\Unity Projects\My Awesome Game`
>
> Please ensure your project is located in a path without spaces before proceeding with the installation.

## <a name="install-server"></a>Installation

Installing this MCP Unity Server is a multi-step process:

### Step 1: Install Node.js 
> To run MCP Unity server, you'll need to have Node.js 18 or later installed on your computer:

![node](docs/node.jpg)

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Windows</span></summary>

1. Visit the [Node.js download page](https://nodejs.org/en/download/)
2. Download the Windows Installer (.msi) for the LTS version (recommended)
3. Run the installer and follow the installation wizard
4. Verify the installation by opening PowerShell and running:
   ```bash
   node --version
   ```
</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">macOS</span></summary>

1. Visit the [Node.js download page](https://nodejs.org/en/download/)
2. Download the macOS Installer (.pkg) for the LTS version (recommended)
3. Run the installer and follow the installation wizard
4. Alternatively, if you have Homebrew installed, you can run:
   ```bash
   brew install node@18
   ```
5. Verify the installation by opening Terminal and running:
   ```bash
   node --version
   ```
</details>

### Step 2: Install Unity MCP Server Enhanced package via Unity Package Manager

> **🚀 Choose Enhanced Version**: This enhanced fork includes powerful scene management features!

#### Option A: Install Enhanced Fork (Recommended)
1. Open the Unity Package Manager (Window > Package Manager)
2. Click the "+" button in the top-left corner
3. Select "Add package from git URL..."
4. Enter: `https://github.com/kblood/mcp-unity-fork.git`
5. Click "Add"

#### Option B: Install Original Version
1. Open the Unity Package Manager (Window > Package Manager)
2. Click the "+" button in the top-left corner
3. Select "Add package from git URL..."
4. Enter: `https://github.com/CoderGamester/mcp-unity.git`
5. Click "Add"

*Note: The enhanced fork is fully compatible with the original but adds scene management capabilities.*

![package manager](https://github.com/user-attachments/assets/a72bfca4-ae52-48e7-a876-e99c701b0497)

### Step 3: Configure AI LLM Client

<details open>
<summary><span style="font-size: 1.1em; font-weight: bold;">Option 1: Configure using Unity Editor</span></summary>

1. Open the Unity Editor
2. Navigate to Tools > MCP Unity > Server Window
3. Click on the "Configure" button for your AI LLM client as shown in the image below

![image](docs/configure.jpg)

4. Confirm the configuration installation with the given popup

![image](https://github.com/user-attachments/assets/b1f05d33-3694-4256-a57b-8556005021ba)

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Option 2: Configure Manually</span></summary>

Open the MCP configuration file of your AI client (e.g. claude_desktop_config.json in Claude Desktop) and copy the following text:

> Replace `ABSOLUTE/PATH/TO` with the absolute path to your MCP Unity installation or just copy the text from the Unity Editor MCP Server window (Tools > MCP Unity > Server Window).

```json
{
   "mcpServers": {
       "mcp-unity": {
          "command": "node",
          "args": [
             "ABSOLUTE/PATH/TO/mcp-unity/Server~/build/index.js"
          ]
       }
   }
}
```

</details>

## <a name="start-server"></a>Start Unity Editor MCP Server
1. Open the Unity Editor
2. Navigate to Tools > MCP Unity > Server Window
3. Click "Start Server" to start the WebSocket server
4. Open Claude Desktop or your AI Coding IDE (e.g. Cursor IDE, Windsurf IDE, etc.) and start executing Unity tools
   
![connect](https://github.com/user-attachments/assets/2e266a8b-8ba3-4902-b585-b220b11ab9a2)

> When the AI client connects to the WebSocket server, it will automatically show in the green box in the window

## Optional: Set WebSocket Port
By default, the WebSocket server runs on port '8090'. You can change this port in two ways:

1. Open the Unity Editor
2. Navigate to Tools > MCP Unity > Server Window
3. Change the "WebSocket Port" value to your desired port number
4. Unity will setup the system environment variable UNITY_PORT to the new port number
5. Restart the Node.js server
6. Click again on "Start Server" to reconnect the Unity Editor web socket to the Node.js MCP Server

## Optional: Set Timeout

By default, the timeout between the MCP server and the WebSocket is 10 seconds.
You can change depending on the OS you are using:

1. Open the Unity Editor
2. Navigate to Tools > MCP Unity > Server Window
3. Change the "Request Timeout (seconds)" value to your desired timeout seconds
4. Unity will setup the system environment variable UNITY_REQUEST_TIMEOUT to the new timeout value
5. Restart the Node.js server
6. Click again on "Start Server" to reconnect the Unity Editor web socket to the Node.js MCP Server

> [!TIP]  
> The timeout between your AI Coding IDE (e.g., Claude Desktop, Cursor IDE, Windsurf IDE) and the MCP Server depends on the IDE.

## Optional: Allow Remote MCP Bridge Connections

By default, the WebSocket server binds to 'localhost'. To allow MCP bridge connections from other machines:

1. Open the Unity Editor
2. Navigate to Tools > MCP Unity > Server Window
3. Enable the "Allow Remote Connections" checkbox
4. Unity will bind the WebSocket server to '0.0.0.0' (all interfaces)
5. Restart the Node.js server to apply the new host configuration
6. Set the environment variable UNITY_HOST to your Unity machine's IP address when running the MCP bridge remotely: `UNITY_HOST=192.168.1.100 node server.js`

## <a name="debug-server"></a>Debugging the Server

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Building the Node.js Server</span></summary>

The MCP Unity server is built using Node.js . It requires to compile the TypeScript code to JavaScript in the `build` directory.
In case of issues, you can force install it in by:

1. Open the Unity Editor
2. Navigate to Tools > MCP Unity > Server Window
3. Click on "Force Install Server" button

![install](docs/install.jpg)

If you want to build it manually, you can follow these steps:

1. Open a terminal/PowerShell/Command Prompt

2. Navigate to the Server directory:
   ```bash
   cd ABSOLUTE/PATH/TO/mcp-unity/Server~
   ```

3. Install dependencies:
   ```bash
   npm install
   ```

4. Build the server:
   ```bash
   npm run build
   ```

5. Run the server:
   ```bash
   node build/index.js
   ```

</details>
   
<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Debugging with MCP Inspector</span></summary>

Debug the server with [@modelcontextprotocol/inspector](https://github.com/modelcontextprotocol/inspector):
   - Powershell
   ```powershell
   npx @modelcontextprotocol/inspector node Server~/build/index.js
   ```
   - Command Prompt/Terminal
   ```cmd
   npx @modelcontextprotocol/inspector node Server~/build/index.js
   ```

Don't forget to shutdown the server with `Ctrl + C` before closing the terminal or debugging it with the [MCP Inspector](https://github.com/modelcontextprotocol/inspector).

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Enable Console Logs</span></summary>

1. Enable logging on your terminal or into a log.txt file:
   - Powershell
   ```powershell
   $env:LOGGING = "true"
   $env:LOGGING_FILE = "true"
   ```
   - Command Prompt/Terminal
   ```cmd
   set LOGGING=true
   set LOGGING_FILE=true
   ```

</details>

## Frequently Asked Questions

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">What is MCP Unity?</span></summary>

MCP Unity is a powerful bridge that connects your Unity Editor environment to AI assistants LLM tools using the Model Context Protocol (MCP).

In essence, MCP Unity:
-   Exposes Unity Editor functionalities (like creating objects, modifying components, running tests, etc.) as "tools" and "resources" that an AI can understand and use.
-   Runs a WebSocket server inside Unity and a Node.js server (acting as a WebSocket client to Unity) that implements the MCP. This allows AI assistants to send commands to Unity and receive information back.
-   Enables you to use natural language prompts with your AI assistant to perform complex tasks within your Unity project, significantly speeding up development workflows.

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Why use MCP Unity?</span></summary>

MCP Unity offers several compelling advantages for developers, artists, and project managers:

-   **Accelerated Development:** Automate repetitive tasks, generate boilerplate code, and manage assets using AI prompts. This frees up your time to focus on creative and complex problem-solving.
-   **Enhanced Productivity:** Interact with Unity Editor features without needing to manually click through menus or write scripts for simple operations. Your AI assistant becomes a direct extension of your capabilities within Unity.
-   **Improved Accessibility:** Allows users who are less familiar with the deep intricacies of the Unity Editor or C# scripting to still make meaningful contributions and modifications to a project through AI guidance.
-   **Seamless Integration:** Designed to work with various AI assistants and IDEs that support MCP, providing a consistent way to leverage AI across your development toolkit.
-   **Extensibility:** The protocol and the toolset can be expanded. You can define new tools and resources to expose more of your project-specific or Unity's functionality to AI.
-   **Collaborative Potential:** Facilitates a new way of collaborating where AI can assist in tasks traditionally done by team members, or help in onboarding new developers by guiding them through project structures and operations.

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">How does MCP Unity compare with the upcoming Unity 6.2 AI features?</span></summary>

Unity 6.2 is set to introduce new built-in AI tools, including the previous Unity Muse (for generative AI capabilities like texture and animation generation) and Unity Sentis (for running neural networks in Unity runtime). As Unity 6.2 is not yet fully released, this comparison is based on publicly available information and anticipated functionalities:

-   **Focus:**
    -   **MCP Unity:** Primarily focuses on **Editor automation and interaction**. It allows external AI (like LLM-based coding assistants) to *control and query the Unity Editor itself* to manipulate scenes, assets, and project settings. It's about augmenting the *developer's workflow* within the Editor.
    -   **Unity 6.2 AI:**
        -   Aims at in-Editor content creation (generating textures, sprites, animations, behaviors, scripts) and AI-powered assistance for common tasks, directly integrated into the Unity Editor interface.
        -   A fine-tuned model to ask any question about Unity's documentation and API structure, with customized examples more accurate to Unity's environment.
        -   Adds the functionality to run AI model inference, allowing developers to deploy and run pre-trained neural networks *within your game or application* for features like NPC behavior, image recognition, etc.

-   **Use Cases:**
    -   **MCP Unity:** "Create a new 3D object, name it 'Player', add a Rigidbody, and set its mass to 10." "Run all Play Mode tests." "Ask to fix the error on the console log." "Execute the custom menu item 'Prepare build for iOS' and fix any errors that may occur."
    -   **Unity 6.2 AI:** "Generate a sci-fi texture for this material." "Update all trees position in the scene to be placed inside of terrain zones tagged with 'forest'." "Create a walking animation for this character." "Generate 2D sprites to complete the character." "Ask details about the error on the console log."

-   **Complementary, Not Mutually Exclusive:**
    MCP Unity and Unity's native AI tools can be seen as complementary. You might use MCP Unity with your AI coding assistant to set up a scene or batch-modify assets, and then use Unity AI tools to generate a specific texture, or to create animations, or 2D sprites for one of those assets. MCP Unity provides a flexible, protocol-based way to interact with the Editor, which can be powerful for developers who want to integrate with a broader range of external AI services or build custom automation workflows.

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">What MCP hosts and IDEs currently support MCP Unity?</span></summary>

MCP Unity is designed to work with any AI assistant or development environment that can act as an MCP client. The ecosystem is growing, but current known integrations or compatible platforms include:
-  Windsurf
-  Cursor
-  GitHub Copilot
-  Claude Desktop

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Can I extend MCP Unity with custom tools for my project?</span></summary>

Yes, absolutely! One of the significant benefits of the MCP Unity architecture is its extensibility.
-   **In Unity (C#):** You can create new C# classes that inherit from `McpToolBase` (or a similar base for resources) to expose custom Unity Editor functionality. These tools would then be registered in `McpUnityServer.cs`. For example, you could write a tool to automate a specific asset import pipeline unique to your project.
-   **In Node.js Server (TypeScript):** You would then define the corresponding TypeScript tool handler in the `Server/src/tools/` directory, including its Zod schema for inputs/outputs, and register it in `Server/src/index.ts`. This Node.js part will forward the request to your new C# tool in Unity.

This allows you to tailor the AI's capabilities to the specific needs and workflows of your game or application.

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Is MCP Unity free to use?</span></summary>

Yes, MCP Unity is an open-source project distributed under the MIT License. You are free to use, modify, and distribute it according to the license terms.

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Why am I unable to connect to MCP Unity?</span></summary>

- Ensure the WebSocket server is running (check the Server Window in Unity)
- Send a console log message from MCP client to force a reconnection between MCP client and Unity server
- Change the port number in the Unity Editor MCP Server window. (Tools > MCP Unity > Server Window)

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Why won't the MCP Unity server start?</span></summary>

- Check the Unity Console for error messages
- Ensure Node.js is properly installed and accessible in your PATH
- Verify that all dependencies are installed in the Server directory

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">Why do I get a connection failed error when running Play Mode tests?</span></summary>

The `run_tests` tool returns the following response:
```
Error:
Connection failed: Unknown error
```

This error occurs because the bridge connection is lost when the domain reloads upon switching to Play Mode.  
The workaround is to turn off **Reload Domain** in **Edit > Project Settings > Editor > "Enter Play Mode Settings"**.

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">🆕 How do I test the new scene management features?</span></summary>

After installing the enhanced fork, you can test the scene management features:

1. **Test Scene Creation**: Ask your AI assistant: *"Create a new 3D scene called 'TestScene'"*
2. **Test Scene Loading**: Ask: *"Load the TestScene"* 
3. **Test Scene Saving**: Ask: *"Save all open scenes"*
4. **Test Scene Information**: Ask: *"Show me all scenes in the project"*

The enhanced features are fully compatible with the original MCP Unity functionality.

</details>

<details>
<summary><span style="font-size: 1.1em; font-weight: bold;">🆕 What's different in the enhanced fork?</span></summary>

The enhanced fork adds comprehensive scene management capabilities while maintaining full compatibility:

**New Tools:**
- `create_scene` - Create scenes with templates
- `load_scene` - Load scenes by name/path  
- `save_scene` - Save current/all scenes
- `unity://scenes` - Scene information resource

**Key Benefits:**
- AI can now fully manage Unity projects
- Better organization for complex game projects
- Automated build settings management
- No breaking changes to existing functionality

*All enhancements were AI-generated using [Claude Code](https://claude.ai/code).*

</details>

## Support & Feedback

### For Enhanced Fork (Scene Management Features)
If you have questions about the enhanced scene management features, please open an [issue](https://github.com/kblood/mcp-unity-fork/issues) on this repository.

### For Original MCP Unity Features  
For questions about the core MCP Unity functionality, please refer to the [original repository](https://github.com/CoderGamester/mcp-unity/issues) or reach out to:
- Linkedin: [![](https://img.shields.io/badge/LinkedIn-0077B5?style=flat&logo=linkedin&logoColor=white 'LinkedIn')](https://www.linkedin.com/in/miguel-tomas/)
- Discord: gamester7178
- Email: game.gamester@gmail.com

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request or open an Issue with your request.

**Commit your changes** following the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) format.

### 🤖 AI-Generated Enhancements
The scene management features in this fork were generated using [Claude Code](https://claude.ai/code), demonstrating the power of AI-assisted development for enhancing open-source tools.

## License

This project is under [MIT License](License.md)

## Acknowledgements

- [Model Context Protocol](https://modelcontextprotocol.io)
- [Unity Technologies](https://unity.com)
- [Node.js](https://nodejs.org)
- [WebSocket-Sharp](https://github.com/sta/websocket-sharp)
- **🤖 [Claude Code](https://claude.ai/code)** - AI assistant that generated the scene management enhancements
- **👨‍💻 [CoderGamester/mcp-unity](https://github.com/CoderGamester/mcp-unity)** - Original MCP Unity implementation

---

## 🎬 Scene Management Features Documentation

For detailed information about the new scene management capabilities, see [SCENE_MANAGEMENT_FEATURES.md](SCENE_MANAGEMENT_FEATURES.md).

**🚀 Ready to enhance your Unity development with AI-powered scene management!**
