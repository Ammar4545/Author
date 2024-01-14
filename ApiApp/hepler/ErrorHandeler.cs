using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.hepler
{
    public class ErrorHandeler
    {
        private readonly IConfiguration config;

        public ErrorHandeler(IConfiguration Config)
        {
            config = Config;
            
        }
        public string ErrorCode { get; set; }
        public string ErrorProp { get; set; }
        public string ErrorMessage { get; set; }

        public void LoadError(string ErrorCode)
        {
            this.ErrorCode = ErrorCode;
            var section= config.GetSection("Errors");
            section.Bind(ErrorCode, this);
        }
    }
}
