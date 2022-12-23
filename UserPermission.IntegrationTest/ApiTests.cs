using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UserPermission.Domain;

namespace MinimalAPIDemo.Test
{
  [TestClass]
  public class ApiTests
  {
    private HttpClient _httpClient;
    private Permission data;


    public ApiTests()
    {
      var webAppFactory = new WebApplicationFactory<Program>();
      _httpClient = webAppFactory.CreateDefaultClient();
        data = new Permission
        {
            Id = 1,
            EmployeeForename= "FTX",
            EmployeeSurname="FTX",
            PermissionDate=DateTime.Today,
            PermissionTypes = new PermissionType {
                Id= 1,
                Description="Admin"
            }
        };
    }

    [TestMethod]
    public async Task RequestPermission_ReturnsTrue()
    {
      var response = await _httpClient.PostAsJsonAsync("/UserPermission/RequestPermission", data);
      var stringResult = await response.Content.ReadAsStringAsync();

      Assert.AreEqual("True", stringResult);
    }
        
    [TestMethod]
    public async Task ModifyPermission_ReturnsTrue()
    {
      var response = await _httpClient.PostAsJsonAsync("/UserPermission/ModifyPermission", data);
      var stringResult = await response.Content.ReadAsStringAsync();

      Assert.AreEqual("True", stringResult);
    }
        
    [TestMethod]
    public async Task GetPermissions_ReturnsList()
    {
      var response = await _httpClient.PostAsJsonAsync("/UserPermission/GetPermissions", data);
      var stringResult = await response.Content.ReadAsStringAsync();
      var result = JsonConvert.DeserializeObject<List<Permission>>(stringResult);
      Assert.IsTrue(result.Any());
    }
  }
}