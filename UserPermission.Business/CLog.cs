using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPermission.Domain.Interface.Business;

namespace UserPermission.Business
{
    public class CLog : ICLog<object>
    {
        private readonly ILogger<object> _logger;

        public CLog(ILogger<object> logger) {
            _logger = logger;
        }
        public void Debug(string info)
        {
            _logger.LogDebug(info);
        }

        public void Error(string info)
        {
            _logger.LogError(info);
        }

        public void Info(string info)
        {
            _logger.LogInformation(info);
        }
    }
}
