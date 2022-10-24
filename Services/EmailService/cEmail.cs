using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace EngineMonitoring.Services
{
    public class cEmail
    {
        private string? _SMTP;
        private string? _SECURITY;
        private int _Puerto;
        public bool SSL = true;
        public bool TLS = true;
        public string? DE { get; set; }
        public string? PASSWORD { get; set; }
        public string? PARA { get; set; }
        public string? CC { get; set; }
        public string? CCO { get; set; }
        public string? Asunto { get; set; }
        private System.Net.Mail.SmtpClient Servidor = new System.Net.Mail.SmtpClient();     //Configuración del servidor
        private System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();     //Configuración del Correo
        private AlternateView? plainView;
        private AlternateView? htmlView;
        private bool _ErrorEstatus;
        public bool ErrorEstatus
        {
            get { return _ErrorEstatus; }
        }
        private string? _ErrorMsg;
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
        }

        public string SMTP
        {
            get { return _SMTP; }

            set { _SMTP = value; }
        }
        public string SECURITY
        {
            get { return _SECURITY; }

            set { _SECURITY = value; }
        }

        public int Puerto
        {
            get { return _Puerto; }

            set { _Puerto = value; }
        }

        public System.Net.Mail.MailPriority Prioridad
        {
            get { return correo.Priority; }

            set { correo.Priority = value; }
        }

        /// <summary>
        /// Obtiene un valor boolean que determina si la configuración del server de correo fue cargada exitosamente o no
        /// </summary>
        /// <returns>
        /// Un valor Boolean
        /// </returns>
        /// <remarks></remarks>
        public bool configServer()
        {
            try
            {
                Servidor.Host = SMTP;
                Servidor.Port = Puerto;
                Servidor.EnableSsl = SSL;

                _ErrorEstatus = true; _ErrorMsg = "";
            }
            catch (Exception ex)
            {
                _ErrorEstatus = false;
                _ErrorMsg = ex.Message;
            }

            return ErrorEstatus;
        }

        /// <summary>
        /// Se encarga de adjuntar archivos al correo que se genera, archivos que se crean de forma dinámica y que estan en memoria.
        /// </summary>
        /// <param name="Adjunto">
        ///
        /// </param>
        /// <param name="archivoADJ">
        /// Archivo que se quiere adjuntar cargado en memoria, puede tener valor null
        /// </param>
        /// <returns>
        /// Un valor Boolean indicado si la carga del archivo adjunto fue exitosa o no
        /// </returns>
        /// <remarks></remarks>
        public bool Adjuntar(string Adjunto, byte[] archivoADJ)
        {
            try
            {
                System.IO.MemoryStream json = new System.IO.MemoryStream(archivoADJ);
                System.Net.Mail.Attachment archivo = new System.Net.Mail.Attachment(json, Adjunto);
                correo.Attachments.Add(archivo);
                _ErrorEstatus = true; _ErrorMsg = "";
            }
            catch (Exception ex)
            {
                _ErrorEstatus = false;
                _ErrorMsg = ex.Message;
            }

            return ErrorEstatus;
        }

        /// <summary>
        /// Se encarga de agregar archivos al correo que se genera. Embebido dentro del propio email, para este fin el cuerpo debe ser formato HTML
        /// </summary>
        /// <param name="rutaImg">
        /// Ubicación en el disco de la imagen que se quier embeber dentro del correo.
        /// </param>
        /// <param name="formato">
        /// Formato del archivo que se quiere insertar JPG, PNG, etc
        /// </param>
        /// <param name="nameImg">
        /// nombre de la etiqueta IMG, en el cuerpo del correo que sera suplantada por la imagen
        /// </param>
        /// <returns>
        /// Un valor Boolean indicado si la carga del archivo adjunto fue exitosa o no
        /// </returns>
        /// <remarks></remarks>
        public bool addEmbeddedObject(string rutaImg, string formato, string nameImg)
        {
            //formato es una variable para determinar el System.Net.Mime.MediaTypeNames.Image,
            //esto sera una actualización para agrega no solo jpeg sino cualquier tipo de imagen al cuerpo del correo
            System.Net.Mime.ContentType contentType = new System.Net.Mime.ContentType();

            if (formato.ToUpper() == "JPG" || formato.ToUpper() == "JPEG")
            {
                contentType.MediaType = System.Net.Mime.MediaTypeNames.Image.Jpeg;
                contentType.Name = "image/jpeg";
            }
            else if (formato.ToUpper() == "GIF")
            {
                contentType.MediaType = System.Net.Mime.MediaTypeNames.Image.Gif;
                contentType.Name = "image/gif";
            }
            else if (formato.ToUpper() == "TIFF")
            {
                contentType.MediaType = System.Net.Mime.MediaTypeNames.Image.Tiff;
                contentType.Name = "image/tiff";
            }
            else if (formato.ToUpper() == "PNG")
            {
                contentType.MediaType = "image/png";
                contentType.Name = "image/png";
            }

            try
            {
                correo.IsBodyHtml = true;       //Determina si el cuerpo del correo es html
                                                //Create a LinkedResource object for each embedded image
                System.Net.Mail.LinkedResource pic1 = new System.Net.Mail.LinkedResource(rutaImg, contentType);
                pic1.ContentId = nameImg;
                htmlView.LinkedResources.Add(pic1);

                _ErrorEstatus = true; _ErrorMsg = "";
            }
            catch (Exception ex)
            {
                _ErrorEstatus = false;
                _ErrorMsg = ex.Message;
            }

            return ErrorEstatus;
        }

        /// <summary>
        /// Se encarga de adjuntar archivos al correo que se genera.
        /// </summary>
        /// <param name="rutaFile">
        /// Ubicación en el disco del archivo que se quiere adjuntar al correo.
        /// </param>
        /// <returns>
        /// Un valor Boolean indicado si la carga del archivo adjunto fue exitosa o no
        /// </returns>
        /// <remarks></remarks>
        public bool addAttachment(string rutaFile)
        {
            try
            {
                System.Net.Mail.Attachment archivo = new System.Net.Mail.Attachment(rutaFile);
                correo.Attachments.Add(archivo);
                _ErrorEstatus = true; _ErrorMsg = "";
            }
            catch (Exception ex)
            {
                _ErrorEstatus = false;
                _ErrorMsg = ex.Message;
            }

            return ErrorEstatus;
        }

        /// <summary>
        /// Se encarga de configurar el formato en que se enviara el correo, y se carga del contenido que el usuario visualizara en formato de texto plano.
        /// </summary>
        /// <param name="cuerpo">
        /// contenido que sera colocado en el body del documento a enviar
        /// </param>
        /// <returns>
        /// Un valor Boolean indicado si la operacion fue exitosa o no
        /// </returns>
        /// <remarks></remarks>
        public bool SetPlainText(string cuerpo)
        {
            try
            {
                plainView = AlternateView.CreateAlternateViewFromString(cuerpo, Encoding.UTF8, MediaTypeNames.Text.Plain);
                correo.AlternateViews.Add(plainView);
                _ErrorEstatus = true; _ErrorMsg = "";
            }
            catch (Exception ex)
            {
                _ErrorEstatus = false;
                _ErrorMsg = ex.Message;
            }

            return ErrorEstatus;
        }

        /// <summary>
        /// Se encarga de configurar el formato en que se enviara el correo, y se carga del contenido que el usuario visualizara en formato HTML.
        /// </summary>
        /// <param name="html">
        /// contenido que sera colocado en el body del documento a enviar
        /// </param>
        /// <returns>
        /// Un valor Boolean indicado si la operacion fue exitosa o no
        /// </returns>
        /// <remarks></remarks>
        public bool SetHtmlText(string html)
        {
            try
            {
                htmlView = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, MediaTypeNames.Text.Html);
                correo.AlternateViews.Add(htmlView);
                _ErrorEstatus = true; _ErrorMsg = "";
            }
            catch (Exception ex)
            {
                _ErrorEstatus = false;
                _ErrorMsg = ex.Message;
            }

            return ErrorEstatus;
        }

        /// <summary>
        /// Función que se encarga de enviar los email ya configurado y cargado con toda la información.
        /// </summary>
        /// <returns>
        /// Un valor Boolean indicado si el envió del email fue exitosa o no
        /// </returns>
        /// <remarks></remarks>
        public bool enviarEmail()
        {
            correo.To.Clear();
            correo.CC.Clear();
            correo.Bcc.Clear();

            try
            {
                correo.From = new System.Net.Mail.MailAddress(DE);
                correo.Subject = Asunto;
                //Correo Para :
                if ((PARA != null))
                {
                    correo.To.Add(PARA);
                }
                //Correo Copia
                if ((CC != null))
                {
                    correo.CC.Add(CC);
                }
                //Correo oculto
                if ((CCO != null))
                {
                    correo.Bcc.Add(CCO);
                }

                if (SSL)
                {
                    Servidor.Credentials = new System.Net.NetworkCredential(DE, PASSWORD);
                }

                if (TLS)
                {
                    Servidor.Credentials = new System.Net.NetworkCredential(DE, PASSWORD);
                }

                Servidor.Send(correo);
                _ErrorEstatus = true; _ErrorMsg = "";
            }
            catch (Exception ex)
            {
                _ErrorEstatus = false;
                _ErrorMsg = ex.Message;
            }

            return ErrorEstatus;
        }
    }
}