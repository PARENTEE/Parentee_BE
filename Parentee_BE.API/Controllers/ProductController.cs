using Microsoft.AspNetCore.Mvc;
using Parentee_BE.API.Constants;
using Parentee_BE.BLL.Services.Interfaces;
using Parentee_BE.DAL.Data.Metadatas;
using Parentee_BE.DAL.Data.RequestDTO.Ai;

namespace Parentee_BE.API.Controllers;

public class ProductController(ILogger<ProductController> logger, IProductService productService) : BaseController<ProductController>(logger)
{
    [HttpGet("product")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Handle chat successful",
            data: await productService.GetAllProduct()
        ));
    }
    
    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProductById(Guid id, Guid priceId)
    {
        return Ok(ApiResponseBuilder.BuildResponse(
            statusCode: StatusCodes.Status200OK,
            isSuccess: true,
            message: "Handle chat successful",
            data: await productService.GetProductAndPriceAsync(id, priceId)
        ));
    }
    
    
}