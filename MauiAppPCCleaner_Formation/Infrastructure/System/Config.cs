using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class Config
    {
        public string UrlInfo { get; set; } = "https://www.anthony-cardinale.fr/tools/pccleaner/";
        public string Version { get; set; } = "v0.0.1";
        public string? SaveOptionNettoyage { get; set; }
        public string? UrlVersion { get; set; }
        public string? UrlFacebook { get; set; }
        public string? UrlTwitter { get; set; }
        public string? UrlYoutube { get; set; }
        public string? UrlGitHub { get; set; }
    }
}
