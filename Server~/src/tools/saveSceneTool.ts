import * as z from 'zod';
import { McpUnity } from '../unity/mcpUnity.js';
import { Logger } from '../utils/logger.js';
import { McpServer } from '@modelcontextprotocol/sdk/server/mcp.js';
import { McpUnityError, ErrorType } from '../utils/errors.js';
import { CallToolResult } from '@modelcontextprotocol/sdk/types.js';

// Constants for the tool
const toolName = 'save_scene';
const toolDescription = 'Saves the current Unity scene or all open scenes';
const paramsSchema = z.object({
  saveAll: z.boolean().default(false).describe('Whether to save all open scenes'),
  scenePath: z.string().optional().describe('The path of a specific scene to save (if not saving all)'),
  saveAs: z.boolean().default(false).describe('Whether to save the scene as a new file'),
  addToBuildSettings: z.boolean().default(false).describe('Whether to add the scene to build settings (only for saveAs)'),
});

/**
 * Creates and registers the Save Scene tool with the MCP server
 * This tool allows saving Unity scenes
 * 
 * @param server The MCP server instance to register with
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param logger The logger instance for diagnostic information
 */
export function registerSaveSceneTool(server: McpServer, mcpUnity: McpUnity, logger: Logger) {
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
 * Handles saving Unity scenes
 * 
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param params The parameters for the tool
 * @returns A promise that resolves to the tool execution result
 * @throws McpUnityError if the request to Unity fails
 */
async function toolHandler(mcpUnity: McpUnity, params: any): Promise<CallToolResult> {
    const response = await mcpUnity.sendRequest({
        method: 'save_scene',
        params: {
            saveAll: params.saveAll,
            scenePath: params.scenePath,
            saveAs: params.saveAs,
            addToBuildSettings: params.addToBuildSettings,
        }
    });

    if (!response.success) {
        throw new McpUnityError(
            ErrorType.TOOL_EXECUTION,
            response.message || `Failed to save scene(s)`
        );
    }

    const actionDescription = params.saveAll ? 'all open scenes' : 
                             params.scenePath ? `scene at '${params.scenePath}'` : 
                             'active scene';

    return {
        content: [{
            type: 'text',
            text: response.message || `Successfully saved ${actionDescription}`
        }]
    };
}