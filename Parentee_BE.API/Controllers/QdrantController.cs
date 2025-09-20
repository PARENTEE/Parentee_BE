using Microsoft.AspNetCore.Mvc;
using Parentee_BE.AI.Services;
using Parentee_BE.Controllers;
using Parentee_BE.DAL.Data.Metadatas;

namespace Parentee_BE.API.Controllers;

public class QdrantController(ILogger<QdrantController> logger, IVectorStoreService vectorStoreService) : BaseController<QdrantController>(logger)
{
     /// <summary>
    /// Get all collections.
    /// </summary>
    [HttpGet("collections")]
    public async Task<IActionResult> GetCollections()
    {
        var collections = await vectorStoreService.GetCollectionList();
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Retrieved collections successfully",
            data: collections
        ));
    }

    /// <summary>
    /// Get collection info by name.
    /// </summary>
    [HttpGet("collections/{collectionName}")]
    public async Task<IActionResult> GetCollectionInfo(string collectionName)
    {
        var info = await vectorStoreService.GetCollectionInfo(collectionName);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: $"Retrieved collection '{collectionName}' info successfully",
            data: info
        ));
    }

    /// <summary>
    /// Create a new collection.
    /// </summary>
    [HttpPost("collections")]
    public async Task<IActionResult> CreateCollection([FromQuery] string collectionName, [FromQuery] ulong dimension)
    {
        await vectorStoreService.CreateCollection(collectionName, dimension);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: $"Collection '{collectionName}' created successfully",
            data: true
        ));
    }

    /// <summary>
    /// Delete a collection by name.
    /// </summary>
    [HttpDelete("collections/{collectionName}")]
    public async Task<IActionResult> DeleteCollection(string collectionName)
    {
        await vectorStoreService.DeleteCollection(collectionName);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: $"Collection '{collectionName}' deleted successfully",
            data: true
        ));
    }
    /// <summary>
    /// Create a new point.
    /// </summary>
    [HttpGet("{collectionName}/points/{pointId}")]
    public async Task<IActionResult> GetPoint([FromRoute] string collectionName, [FromRoute] ulong pointId)
    {
        var point = await vectorStoreService.GetPoint(collectionName, pointId);

        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: $"Retrieved point '{pointId}' from collection '{collectionName}'",
            data: point
        ));
    }
    
    
    /// <summary>
    /// Create a new point.
    /// </summary>
    [HttpPost("points")]
    public async Task<IActionResult> CreatePoint([FromQuery] string collectionName, [FromBody] string text)
    {
        await vectorStoreService.CreatePoint(collectionName, text);
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status201Created,
            isSuccess: true,
            message: $"Collection '{collectionName}' created successfully",
            data: true
        ));
    }

}