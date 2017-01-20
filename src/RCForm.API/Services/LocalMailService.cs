using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API.Services
{
    public class LocalMailService
    {
        private string _mailTo = "josepherech.etorne@emerson.com";
        private string _mailFrom = "josepherech.etorne@emerson.com";

        public void Send(string subject, string message)
        {
            //send mail - out to debug window
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");

            //test
        }
    }
}
