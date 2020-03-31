using Microsoft.AspNetCore.Mvc;
using WorkingMonintor.Services;

namespace WorkingMonintor.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class WebSiteController : ControllerBase {
        private WebSiteService _WebSite;

        public WebSiteController (WebSiteService WebSite) {
            _WebSite = WebSite;
        }

        /// <summary>
        ///     GET，迴圈方式測試 Setting_WebSite.json中設定的站台
        /// </summary>
        /// <returns>
        ///     [{"url":"","statusCode":"","msg":""}]
        /// </returns>
        [HttpGet]
        public IActionResult Get () {
            var result = _WebSite.GetWebSiteStatus ();

            return Ok (result);
        }
    }
}