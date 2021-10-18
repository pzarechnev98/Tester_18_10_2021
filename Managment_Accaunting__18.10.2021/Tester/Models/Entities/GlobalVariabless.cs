using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Tester.Models.Entities
{
    public class GlobalVariabless
    {
        public static Random RANDOM = new Random();

        IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();


        string _newPass = new string(Enumerable.Repeat("AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789", 10).
        Select(s => s[GlobalVariables.RANDOM.
        Next(s.Length)]).
        ToArray());
    }


    private void SendEmail(User user)
    {
        var senderEmail = new MailAddress(configuration.GetSection("Smtp")["Email"], "Org");
        var EmailReceiver = new MailAddress(user.Email, "Пользователь");

        string _password = configuration.GetSection("Smtp")["Password"];
        string _sub = "Создание пользователя";
              string _body = string.Format("<h1>Доброго времени суток, {0}!</h1><br><hr>" +
              "<p>Ваша учетная запись успешно создана! </p>" +
              "<p>Ваш логин: <h1>{0}</h1></p>" +
              "<p>Ваш пароль: <h1>{1}</h1></p>" +
              "<p>С уважением, Ваша организация!</p>", user.Email, user.Password);


    var smtp = new SmtpClient {
        Host = configuration.GetSection("Smtp")["Host"],
        Port = int.Parse(configuration.GetSection("Smtp")["Port"]),
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        UseDefaultCredentials = false,
        Credentials = new NetworkCredential(senderEmail.Address, _password)
    };


    using (var message = new MailMessage(senderEmail, EmailReceiver)) {
        message.Subject = _sub;
        message.Body = _body;
        message.IsBodyHtml = true;
        ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
        smtp.Send(message);
    }
        smtp.Dispose();
    }


}
