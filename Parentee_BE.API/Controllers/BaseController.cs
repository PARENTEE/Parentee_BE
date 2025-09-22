using Parentee_BE.API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Parentee_BE.API.Controllers;

[Route(APIEndpointsConstant.API_ENDPOINT)]
[ApiController]
public class BaseController<T>
    (ILogger<T> logger)
    : ControllerBase where T : BaseController<T>
{
    
}