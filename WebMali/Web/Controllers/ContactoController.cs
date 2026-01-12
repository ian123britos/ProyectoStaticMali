using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Web.Controllers
{
    public class ContactoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enviar(string Nombre, string Correo, int telefono ,string Mensaje)
        {
            try
            {
                // Correo emisor: tu cuenta de Gmail con contraseña de aplicación
                var fromAddress = new MailAddress("maliproducciones@gmail.com", "Mali Fiestas"); // tu correo emisor
                // Correo receptor: donde llegarán los mensajes
                var toAddress = new MailAddress("maliproducciones@gmail.com"); // correo receptor final
                const string fromPassword = "peyv ynbl fhyv ebhz"; // contraseña de aplicación
                string subject = "Nuevo mensaje de contacto";
                //aca recibo los datos que el cliente ah ingresado en el formulario
                string body = $"Nombre: {Nombre}\nCorreo: {Correo}\nTelefono: {telefono}\nConsulta: {Mensaje}";


                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                TempData["Success"] = "Mensaje enviado correctamente ✅";
            }
            catch
            {
                TempData["Error"] = "Hubo un problema al enviar el mensaje ❌";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}

