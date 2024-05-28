﻿namespace octo.Domain.Model
{
    public class MailKitOptions
    {
        public string? SmtpServer { get; set; } = null;
        public int SmtpPort { get; set; }
        public string? SmtpUsername { get; set; } = null;
        public string? SmtpPassword { get; set; } = null;
        public string? SenderEmail { get; set; } = null;
        public string? SenderName { get; set; } = null;
    }
}
