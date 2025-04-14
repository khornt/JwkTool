using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwkTool2.Models
{
    public class AppConfig
    {
        public string navn => ConfigurationManager.AppSettings["kristian"] ?? throw new ConfigurationErrorsException("Æsj");
        public List<string> rsaKeys
        {
            get
            {
                var listAsString = ConfigurationManager.AppSettings["rsaKeys"] ?? throw new ConfigurationErrorsException($"Missing app.config for rsaKeys");
                return listAsString.Split(",").ToList();
            }
        }
    }
}

