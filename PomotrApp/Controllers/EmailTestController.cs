using System;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PomotrApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public EmailTestController(IConfiguration config)
        {
            Configuration = config;
        }

        [HttpGet]
        public ActionResult Test(String secret)
        {
            var username = Configuration["Smtp:Username"];
            var password = Configuration["Smtp:Password"];
            var smtpHost = Configuration["Smtp:Host"];
            var smtpPort = Int32.Parse( Configuration["Smtp:Port"] );
            var sharedSecret = Configuration["Smtp:Secret"];
            if(sharedSecret!=secret) {
                return Unauthorized();
            }

            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.Credentials = new System.Net.NetworkCredential(username, password);

            string body = 
@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
    <head>
        <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
        <title>A Simple Responsive HTML Email</title>
        <style type=""text/css"">
        body {margin: 0; padding: 0; min-width: 100%!important;}
        .content {width: 100%; max-width: 600px;}  
        </style>
    </head>
    <body yahoo bgcolor=""#f6f8f1"">
        <table width=""100%"" bgcolor=""#f6f8f1"" border=""0"" cellpadding=""0"" cellspacing=""0"">
            <tr>
                <td>
                    <table class=""content"" align=""center"" cellpadding=""0"" cellspacing=""0"" border=""0"">
                        <tr>
                            <td>
                                Hello!
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </body>
</html>";

            MailMessage email = new MailMessage(
                from: "tlaukkanen@gmail.com",
                to: "tlaukkanen@gmail.com",
                subject: "Test email",
                body: body
            ) {
                IsBodyHtml = true
            };

            client.Send(email);
            return Ok();
        }

    }

}