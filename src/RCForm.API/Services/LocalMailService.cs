﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API.Services
{
    public class LocalMailService : IMailService
    {
        private string to = Startup.Configuration["mailSettings:mailToAddress"];
        private string from = Startup.Configuration["MailSettings:mailFromAddress"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Mail from {from} to {to}, with LocalMailService");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
