using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.Versioning;

namespace ExtratoBancario.Api.Controllers
{
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public IActionResult Instalacao()
        {
            var informationVersion = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            string versaocore = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

            return Ok($@"Instalação .NetCore [{versaocore}]{Environment.NewLine}" +
                       $"URL Base do app     [{HttpContext.Request.Host.Value}]{Environment.NewLine}" +
                       $"Versão Api          [{informationVersion}]{Environment.NewLine}");
        }
    }
}
