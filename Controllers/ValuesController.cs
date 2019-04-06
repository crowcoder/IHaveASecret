using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IHaveASecret.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IConfiguration _config;
        public ValuesController(IConfiguration config)
        {
            _config = config;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            string CmdLineConfigValue =_config.GetValue<string>("cmdlinearg") ?? "No Cmd Line Value Set";
            
            string EnvVarValue =_config.GetValue<string>("SECRET_ENV_VAR") ?? "No Env Var Value Set";
            
            string usrsecret1 = _config.GetValue<string>("user-secret-1") ?? "No user secret 1 Set";
            
            //string kvValue = _config.GetValue<string>("kvsecret1") ?? "No Azure KeyVault secret found";

            return new string[] { CmdLineConfigValue, EnvVarValue, usrsecret1 };
        }
    }
}
