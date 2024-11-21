using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace HealthTrracke.APIs.Controllers.V1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class BaseApiController : ControllerBase
{
    
}