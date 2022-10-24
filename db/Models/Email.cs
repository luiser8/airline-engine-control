using Microsoft.AspNetCore.Mvc;

namespace EngineMonitoring.Models
{
    public class Email
    {
        public string ?Template { get; set; }
        public string ?Title { get; set; }
        public string ?Subject { get; set; }
        public bool withCCO { get; set; }
        public string ?TO { get; set; }
        public string ?fileName { get; set; }
        public byte[] ?file { get; set; }
    }
}