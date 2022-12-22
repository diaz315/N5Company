using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermission.Domain;
using UserPermission.Domain.Interface.Business;

namespace UserPermission.Business
{
    public class ElasticSearchService : IElasticSearchService<Permission>
    {
        private readonly ILogger<ElasticSearchService> _logger;
        public ElasticSearchService(ILogger<ElasticSearchService> logger)
        {
            _logger = logger;
        }

        public void Register(Permission entity)
        {
            try
            {
                var settings = new ConnectionSettings(new Uri(Environment.GetEnvironmentVariable("ELASTICSEARCH")!))
               .DefaultIndex("permission");

                var client = new ElasticClient(settings);

                var asyncIndexResponse = client.IndexDocument(entity);
                _logger.LogDebug($"Key ElasticSearch: {asyncIndexResponse}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error ElasticSearchService: {ex.Message}");
            }

        }
    }
}
