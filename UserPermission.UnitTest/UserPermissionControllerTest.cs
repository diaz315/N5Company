using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Web.Http.Results;
using System.Web.Http;
using UserPermission.Api.Controllers;
using UserPermission.Business;
using UserPermission.Domain;
using UserPermission.Domain.Interface.Business;
using System.Data.Entity.Core.Objects;
using Bogus.Bson;
using Newtonsoft.Json;

namespace UserPermission.UnitTest
{
    [TestClass]
    public class UserPermissionControllerTest
    {
        Mock<IPermissionService<Permission>> _permissionService;
        Mock<ICLog<UserPermissionController>> _logger;
        Mock<IElasticSearchService<Permission>> _elasticSearchService;
        UserPermissionController _cont;


        [TestInitialize]
        public void Inicializar()
        {
            var data = new List<Permission>{
                new Permission { EmployeeForename="Jose",EmployeeSurname="Diaz",Id=1, PermissionDate=DateTime.Today,PermissionTypes=new PermissionType{ Id=1,Description="Desd" } },
                new Permission { EmployeeForename="Antonio",EmployeeSurname="Lara",Id=2, PermissionDate=DateTime.Today,PermissionTypes=new PermissionType{ Id=1,Description="Desd" } },
                new Permission { EmployeeForename="Tony",EmployeeSurname="Medina",Id=3, PermissionDate=DateTime.Today,PermissionTypes=new PermissionType{ Id=1,Description="Desd" } },
            };

            _permissionService = new Mock<IPermissionService<Permission>>();
            _permissionService.Setup(x => x.Add(It.IsAny<Permission>()));
            _permissionService.Setup(x => x.Modify(It.IsAny<Permission>()));
            _permissionService.Setup(x => x.Select(It.IsAny<Permission>())).Returns(data);

            _elasticSearchService = new Mock<IElasticSearchService<Permission>>();
            _elasticSearchService.Setup(x => x.Register(It.IsAny<Permission>()));

            _logger = new Mock<ICLog<UserPermissionController>>();
            _logger.Setup(x => x.Info(It.IsAny<string>()));
            _logger.Setup(x => x.Debug(It.IsAny<string>()));
            _logger.Setup(x => x.Error(It.IsAny<string>()));

            _cont = new UserPermissionController(_logger.Object, _permissionService.Object, _elasticSearchService.Object);
        }

        [TestMethod]
        public void RequestPermission_ShouldReturnStatus200()
        {
            IActionResult actionResult = _cont.RequestPermission(new Permission { }); ;
            var okResult = actionResult as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(okResult.Value?.ToString()?.Equals("True"));
        }

        [TestMethod]
        public void ModifyPermission_ShouldReturnStatus200()
        {
            IActionResult actionResult = _cont.ModifyPermission(new Permission { }); ;
            var okResult = actionResult as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(okResult.Value?.ToString()?.Equals("True"));
        }

        [TestMethod]
        public void GetPermissions_ShouldReturnStatus200()
        {
            IActionResult actionResult = _cont.GetPermissions(new Permission { }); ;
            var okResult = actionResult as OkObjectResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            var data = okResult.Value as List<Permission>;
            Assert.AreEqual(data?.Count, 3);
        }
    }
}