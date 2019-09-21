using System.Collections.Generic;
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
            string EnvVarValue =_config.GetValue<string>("SECRET_ENV_VAR") ?? "NOT SET";

            string CmdLineConfigValue =_config.GetValue<string>("cmdlinearg") ?? "NOT SET";            
            
            string usrsecret1 = _config.GetValue<string>("user-secret-1") ?? "NOT SET";
            
            string kvValue = _config.GetValue<string>("kvsecret1") ?? "NOT SET";

            return new string[] { 
                $"Environment Variable: {EnvVarValue}",
                $"Command Line Arg: {CmdLineConfigValue}", 
                $"User Secret: {usrsecret1}",
                $"KeyVault: {kvValue}"
             };
        }
    }
}
