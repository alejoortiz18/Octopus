using Constant;
using Helpers.Interfaces;
using Models.Entities.Domain.DBOctopus.OctopusEntities;
using System.Net.Mail;

namespace Helpers
{
    public class EmailHelper : IEmailHelper
    {

        public async Task EnviarCorreoAsync(string destinatario, string codigo, string subject, string body)
        {
            try
            {
                string cuerpo = body.Replace("{codigo}", codigo);
                EnviarEmailAsync(destinatario, subject, cuerpo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }

        public async Task EnviarCorreoCrearUsuarioNuevoAsync(Usuario usuario, string subject, string body)
        {
            try
            {
                body = body.Replace("{codigo}", usuario.TokenVerificacion);
                string cuerpo = body.Replace("{email}", usuario.Email);
                EnviarEmailAsync(usuario.Email, subject, cuerpo);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }

        private async Task EnviarEmailAsync(string destinatario, string subject, string body)
        {
            try
            {
                string EmailOrigen = ParametrosEmailConstant.Remitente;
                string Contrasena = ParametrosEmailConstant.Contrasena;

                string asunto = $"{subject} - Octopus";
                string cuerpo = body;

                MailMessage oMailMessage = new MailMessage()
                {
                    From = new MailAddress(EmailOrigen),
                    Subject = asunto,
                    Body = cuerpo,
                    IsBodyHtml = true
                };

                oMailMessage.To.Add(destinatario);
                // Configurar encabezados adicionales para evitar spam
                oMailMessage.Headers.Add("X-Priority", "1"); // Alta prioridad
                oMailMessage.Headers.Add("X-MSMail-Priority", "High");
                oMailMessage.Headers.Add("Importance", "High");
                SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com")
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential(EmailOrigen, Contrasena)
                };

                await oSmtpClient.SendMailAsync(oMailMessage);
                oSmtpClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }
    }
}
