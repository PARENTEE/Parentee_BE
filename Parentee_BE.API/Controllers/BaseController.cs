using Microsoft.AspNetCore.Mvc;
using Parentee_BE.Constants;

namespace Parentee_BE.API.Controllers;

[Route(APIEndpointsConstant.API_ENDPOINT)]
[ApiController]
public class BaseController<T>
    (ILogger<T> logger)
    : ControllerBase where T : BaseController<T>
{
    
}