using System.Collections.Generic;

namespace WorkingMonintor.Models {
    public class Setting_WebSite {
        public List<WebSiteSettings> WebSiteSettings { get; set; }
    }

    public class WebSiteSettings {
        public string WebSite { get; set; }
        public string URL { get; set; }
        public string Desc { get; set; }
    }
}