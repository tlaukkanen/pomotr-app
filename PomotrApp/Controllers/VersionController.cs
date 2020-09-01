using Microsoft.AspNetCore.Mvc;

namespace PomotrApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : Controller
    {
        public class VersionInfo
        {
            public string Version { get; set; }
        }        

        [HttpGet]
        [Route("/api/version")]
        public ActionResult<VersionInfo> GetVersion()
        {
            // Version number is set during the build pipeline.
            // Version number is read from CHANGELOG.md that is using the http://keepachangelog.com/ format.
            // Ref.
            // https://semver.org/
            // https://docs.microsoft.com/en-us/dotnet/csharp/versioning
            var version = typeof(VersionController).Assembly.GetName().Version;
            var versionInfo = new VersionInfo
            {
                Version = version.ToString()
            };
            return versionInfo;
        }
        
    }
}