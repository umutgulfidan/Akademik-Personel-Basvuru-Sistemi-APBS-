using Business.Abstracts;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using DotNetEnv;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class EmailManager : IEmailService
    {
        private readonly IUserDal _userDal;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpEmail;
        private readonly string _smtpPassword;
        private readonly bool _enableSsl;

        public EmailManager(IUserDal userDal)
        {
            _userDal = userDal;

            // DotNetEnv ile .env dosyasını yükle
            DotNetEnv.Env.Load(AppDomain.CurrentDomain.BaseDirectory + "\\.env");

            // Env değişkenlerini oku
            _smtpHost = Env.GetString("SMTP_HOST");
            _smtpPort = Env.GetInt("SMTP_PORT");
            _smtpEmail = Env.GetString("SMTP_EMAIL");
            _smtpPassword = Env.GetString("SMTP_PASSWORD");
            _enableSsl = Env.GetBool("SMTP_ENABLE_SSL");
        }

        public async Task<IResult> SendEmail(string to, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.EnableSsl = _enableSsl;
                    client.Credentials = new NetworkCredential(_smtpEmail, _smtpPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    using (var message = new MailMessage())
                    {
                        message.From = new MailAddress(_smtpEmail, "APBS Sistemi");
                        message.To.Add(to);
                        message.Subject = subject;
                        message.Body = body;
                        message.IsBodyHtml = true;

                        await client.SendMailAsync(message);
                        return new SuccessResult("E-posta başarıyla gönderildi");
                    }
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult($"E-posta gönderirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> SendEmailToUser(int userId, string subject, string body)
        {
            try
            {
                // Kullanıcı e-postasını veritabanından al
                var user = await _userDal.GetAsync(u => u.Id == userId);

                if (user == null)
                {
                    return new ErrorResult($"Kullanıcı bulunamadı. ID: {userId}");
                }

                if (string.IsNullOrEmpty(user.Email))
                {
                    return new ErrorResult($"Kullanıcı {userId} için e-posta adresi bulunamadı.");
                }

                // E-posta gönder
                return await SendEmail(user.Email, subject, body);
            }
            catch (Exception ex)
            {
                return new ErrorResult($"Kullanıcıya e-posta gönderme işlemi başarısız: {ex.Message}");
            }
        }
    }
}