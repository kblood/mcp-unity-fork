import * as z from 'zod';
import { McpUnity } from '../unity/mcpUnity.js';
import { Logger } from '../utils/logger.js';
import { McpServer } from '@modelcontextprotocol/sdk/server/mcp.js';
import { McpUnityError, ErrorType } from '../utils/errors.js';
import { CallToolResult } from '@modelcontextprotocol/sdk/types.js';

// Constants for the tool
const toolName = 'create_scene';
const toolDescription = 'Creates a new Unity scene with specified name and optional template';
const paramsSchema = z.object({
  sceneName: z.string().describe('The name of the scene to create'),
  scenePath: z.string().optional().describe('The path where the scene should be saved (defaults to Assets/Scenes/{sceneName}.unity)'),
  templateType: z.enum(['empty', 'basic', '2d', '3d']).default('empty').describe('The type of scene template to use'),
  setActive: z.boolean().default(true).describe('Whether to set the new scene as the active scene'),
  addToBuildSettings: z.boolean().default(false).describe('Whether to add the scene to build settings'),
});

/**
 * Creates and registers the Create Scene tool with the MCP server
 * This tool allows creating new Unity scenes
 * 
 * @param server The MCP server instance to register with
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param logger The logger instance for diagnostic information
 */
export function registerCreateSceneTool(server: McpServer, mcpUnity: McpUnity, logger: Logger) {
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
 * Handles creating a new Unity scene
 * 
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param params The parameters for the tool
 * @returns A promise that resolves to the tool execution result
 * @throws McpUnityError if the request to Unity fails
 */
async function toolHandler(mcpUnity: McpUnity, params: any): Promise<CallToolResult> {
    // Validate scene name
    if (!params.sceneName || params.sceneName.trim() === '') {
        throw new McpUnityError(
            ErrorType.VALIDATION,
            "Scene name is required and cannot be empty"
        );
    }

    const response = await mcpUnity.sendRequest({
        method: 'create_scene',
        params: {
            sceneName: params.sceneName,
            scenePath: params.scenePath,
            templateType: params.templateType,
            setActive: params.setActive,
            addToBuildSettings: params.addToBuildSettings,
        }
    });

    if (!response.success) {
        throw new McpUnityError(
            ErrorType.TOOL_EXECUTION,
            response.message || `Failed to create scene '${params.sceneName}'`
        );
    }

    return {
        content: [{
            type: 'text',
            text: response.message || `Successfully created scene '${params.sceneName}' at ${response.scenePath}`
        }]
    };
}