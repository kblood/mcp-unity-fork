import { McpUnity } from '../unity/mcpUnity.js';
import { Logger } from '../utils/logger.js';
import { McpServer } from '@modelcontextprotocol/sdk/server/mcp.js';
import { McpUnityError, ErrorType } from '../utils/errors.js';
import { ReadResourceResult } from '@modelcontextprotocol/sdk/types.js';

/**
 * Creates and registers the Get Scenes resource with the MCP server
 * This resource provides information about Unity scenes including open scenes,
 * scenes in build settings, and all scene assets
 * 
 * @param server The MCP server instance to register with
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param logger The logger instance for diagnostic information
 */
export function registerGetScenesResource(server: McpServer, mcpUnity: McpUnity, logger: Logger) {
    logger.info('Registering resource: unity://scenes');
    
    // Register different scene resource endpoints
    const sceneResources = [
        'unity://scenes',       // All scene information
        'unity://scenes/open',  // Only open scenes
        'unity://scenes/build', // Only build settings scenes
        'unity://scenes/assets' // Only scene assets
    ];

    sceneResources.forEach(uri => {
        server.resource(
            uri,
            `Unity scenes information - ${getResourceDescription(uri)}`,
            'application/json',
            async (uri: string) => {
                try {
                    logger.info(`Fetching resource: ${uri}`);
                    const result = await resourceHandler(mcpUnity, uri);
                    logger.info(`Resource fetch successful: ${uri}`);
                    return result;
                } catch (error) {
                    logger.error(`Resource fetch failed: ${uri}`, error);
                    throw error;
                }
            }
        );
    });
}

/**
 * Gets a description for the resource based on its URI
 */
function getResourceDescription(uri: string): string {
    if (uri.endsWith('/open')) return 'currently open scenes';
    if (uri.endsWith('/build')) return 'scenes in build settings';
    if (uri.endsWith('/assets')) return 'all scene assets in project';
    return 'comprehensive scene information';
}

/**
 * Handles requests for Unity scenes information
 * 
 * @param mcpUnity The McpUnity instance to communicate with Unity
 * @param uri The resource URI being requested
 * @returns A promise that resolves to the resource content
 * @throws McpUnityError if the request to Unity fails
 */
async function resourceHandler(mcpUnity: McpUnity, uri: string): Promise<ReadResourceResult> {
    const response = await mcpUnity.sendRequest({
        method: 'get_resource',
        params: {
            name: 'scenes',
            uri: uri
        }
    });

    if (response.error) {
        throw new McpUnityError(
            ErrorType.RESOURCE_ACCESS,
            response.error || 'Failed to retrieve scenes information from Unity'
        );
    }

    // Format the response data
    let formattedContent: string;
    
    try {
        // Check if response is already a JSON object
        const data = typeof response === 'string' ? JSON.parse(response) : response;
        formattedContent = JSON.stringify(data, null, 2);
    } catch (parseError) {
        // If parsing fails, treat as plain text
        formattedContent = typeof response === 'string' ? response : JSON.stringify(response, null, 2);
    }

    return {
        contents: [{
            type: 'text',
            text: formattedContent
        }]
    };
}