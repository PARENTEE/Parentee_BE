using Parentee_BE.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Parentee_BE.Controllers;

[Route(APIEndpointsConstant.API_ENDPOINT)]
[ApiController]
public class BaseController<T>
    (ILogger<T> logger)
    : ControllerBase where T : BaseController<T>
{
    
}