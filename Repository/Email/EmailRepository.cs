using EngineMonitoring.Models;
using EngineMonitoring.Services;

namespace EngineMonitoring.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;
        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Send(Email email)
        {
            var resp = "";
            try
            {
                var smtp = _configuration.GetSection("EmailConfig")["smtp"];
                var port = _configuration.GetSection("EmailConfig")["port"];
                var from = _configuration.GetSection("EmailConfig")["from"];
                var cco = _configuration.GetSection("EmailConfig")["cco"];
                var pass = _configuration.GetSection("EmailConfig")["pass"];
                var security = _configuration.GetSection("EmailConfig")["security"];

                string currentDirectory = Directory.GetCurrentDirectory();
                string path ="Templates/";

                StreamReader objLector;
                objLector = System.IO.File.OpenText(path + email.Template);

                string HTML = objLector.ReadToEnd();
                objLector.Close();

                HTML = HTML.Replace("@Title", email.Title);
                HTML = HTML.Replace("@fecha", DateTime.Now.ToString("dd/MM/yyyy"));
                HTML = HTML.Replace("@hora", DateTime.Now.ToString("hh:mm:ss"));

                cEmail cEmail = new cEmail();

                cEmail.Asunto = email.Subject;
                cEmail.SMTP = smtp;
                cEmail.Puerto = Convert.ToInt32(port);
                cEmail.SECURITY = security;
                cEmail.PASSWORD = pass;

                cEmail.configServer();

                cEmail.DE = from;
                cEmail.PARA = email.TO;
                if (email.withCCO)
                {
                    cEmail.CCO = cco;
                }

                System.Net.Mail.AlternateView avHtml = System.Net.Mail.AlternateView.CreateAlternateViewFromString(HTML, null, System.Net.Mime.MediaTypeNames.Text.Html);

                cEmail.SetHtmlText(HTML);
                cEmail.Adjuntar(email.fileName + ".json", email.file);
                cEmail.enviarEmail();
            }
            catch (Exception error)
            {
                resp = "ERROR - " + error;
            }
            return resp;
        }
    }
}