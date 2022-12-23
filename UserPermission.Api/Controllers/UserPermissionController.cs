using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using UserPermission.Business;
using UserPermission.Domain;
using UserPermission.Domain.Interface.Business;
using UserPermission.Domain.Interface.Repository;
using UserPermission.Repository;

namespace UserPermission.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserPermissionController : ControllerBase
    {
        private readonly ICLog<UserPermissionController> _logger;
        private readonly IPermissionService<Permission> _permissionService;
        private readonly IElasticSearchService<Permission> _elasticSearchService;

        public UserPermissionController(ICLog<UserPermissionController> logger, IPermissionService<Permission> permissionService, IElasticSearchService<Permission> elasticSearchService)
        {
            _logger = logger;
            _permissionService = permissionService;
            _elasticSearchService = elasticSearchService;
        }

        [HttpPost]
        [Route("RequestPermission")]
        public IActionResult RequestPermission([FromBody] Permission permission)
        {
            bool result = false;
            try
            {
                _elasticSearchService.Register(permission);
                _permissionService.Add(permission);
                result = true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error RequestPermission {ex.Message}");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("ModifyPermission")]
        public IActionResult ModifyPermission([FromBody] Permission permission)
        {
            bool result = false;
            try
            {
                _elasticSearchService.Register(permission);
                _permissionService.Modify(permission);
                result = true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error ModifyPermission {ex.Message}");
            }

            return Ok(result);
        }       
        
        [HttpPost]
        [Route("GetPermissions")]
        public IActionResult GetPermissions([FromBody] Permission permission)
        {
            IList<Permission> permissions = new List<Permission>();

            try
            {
                _elasticSearchService.Register(permission);
                permissions = _permissionService.Select(permission);
                if (!permissions.Any())
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error GetPermissions {ex.Message}");
            }

            return Ok(permissions);
        }
    }
}