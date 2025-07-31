import * as z from 'zod';
import { McpUnity } from '../unity/mcpUnity.js';
import { Logger } from '../utils/logger.js';
import { McpServer } from '@modelcontextprotocol/sdk/server/mcp.js';
import { McpUnityError, ErrorType } from '../utils/errors.js';
import { CallToolResult } from '@modelcontextprotocol/sdk/types.js';

// Constants for the tool
const toolName = 'load_scene';
const toolDescription = 'Loads a Unity scene by path or name';
const paramsSchema = z.object({
  scenePath: z.string().optional().describe('The path of the scene to load'),
  sceneName: z.string().optional().describe('The name of the scene to load (alternative to scenePath)'),
  loadMode: z.enum(['single', 'additive']).default('single').describe('The load mode for the scene'),
  saveCurrentScene: z.boolean().default(true).describe('Whether to save the current scene before loading the new one'),
}).refine(data => data.scenePath || data.sceneName, {
  message: 'Either scenePath or sceneName must be provided',
});

/**
 * Creates and registers the Load Scene tool with the MCP server
 * This tool allows loading Unity scenes
 * 
 * @param server The MCP server instance to register with
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param logger The logger instance for diagnostic information
 */
export function registerLoadSceneTool(server: McpServer, mcpUnity: McpUnity, logger: Logger) {
    logger.info(`Registering tool: ${toolName}`);
        
    // Register this tool with the MCP server
    server.tool(
      toolName,
      toolDescription,
      paramsSchema.shape,
      async (params: any) => {
        try {
          logger.info(`Executing tool: ${toolName}`, params);
          const result = await toolHandler(mcpUnity, params);
          logger.info(`Tool execution successful: ${toolName}`);
          return result;
        } catch (error) {
          logger.error(`Tool execution failed: ${toolName}`, error);
          throw error;
        }
      }
    );
}

/**
 * Handles loading a Unity scene
 * 
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param params The parameters for the tool
 * @returns A promise that resolves to the tool execution result
 * @throws McpUnityError if the request to Unity fails
 */
async function toolHandler(mcpUnity: McpUnity, params: any): Promise<CallToolResult> {
    const response = await mcpUnity.sendRequest({
        method: 'load_scene',
        params: {
            scenePath: params.scenePath,
            sceneName: params.sceneName,
            loadMode: params.loadMode,
            saveCurrentScene: params.saveCurrentScene,
        }
    });

    if (!response.success) {
        throw new McpUnityError(
            ErrorType.TOOL_EXECUTION,
            response.message || `Failed to load scene`
        );
    }

    const sceneIdentifier = params.scenePath || params.sceneName;
    return {
        content: [{
            type: 'text',
            text: response.message || `Successfully loaded scene '${sceneIdentifier}' in ${params.loadMode} mode`
        }]
    };
}