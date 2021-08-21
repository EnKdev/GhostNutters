using Newtonsoft.Json;

namespace GhostNutters.LanguageGrabber
{
    public class VersionModel
    {
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}