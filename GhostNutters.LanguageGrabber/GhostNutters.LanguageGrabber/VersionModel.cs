using Newtonsoft.Json;

namespace GhostNutters.LanguageGrabber
{
    public class VersionModel
    {
        [JsonProperty("betaVersion")]
        public string BetaVersion { get; set; }
        
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}