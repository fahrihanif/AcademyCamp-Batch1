using API.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("Api/[controller]")]
[Authorize(Roles = RoleHandler.Employee)]
public class BaseController : ControllerBase { }
