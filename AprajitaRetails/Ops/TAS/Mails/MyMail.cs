using MailKit.Net.Smtp;
using MimeKit;

namespace AprajitaRetails.Ops.TAS.Mails
{


    public static class MyMail
    {

        public static void SendEmail(string subjects, string messages, string toAddress)
        {
            var message = new MimeMessage ();
            message.From.Add (new MailboxAddress ("Aprajita Retails", "aprajitaretailsdumka@gmail.com"));
            message.To.Add (new MailboxAddress (/*"",*/toAddress));
            message.Subject = subjects;
            message.Body = new TextPart ("plain") { Text = messages };

            using var client = new SmtpClient
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };
            client.Connect ("smtp.gmail.com", 25, false);
            // Note: only needed if the SMTP server requires authentication
            client.Authenticate ("aprajitaretailsdumka@gmail.com", "ferrari265");
            client.Send (message);
            client.Disconnect (true);
        }
    }
}


//S.No Email Provider SMTP Server(Host ) Port Number
//1	Gmail smtp.gmail.com  587
//2	Outlook smtp.live.com   587
//3	Yahoo Mail  smtp.mail.yahoo.com 465
//5	Hotmail smtp.live.com   465
//6	Office365.com smtp.office365.com  587