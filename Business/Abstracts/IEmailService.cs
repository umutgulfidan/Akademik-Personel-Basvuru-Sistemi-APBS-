using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IEmailService
    {
        Task<IResult> SendEmail(string to, string subject, string body);
        Task<IResult> SendEmailToUser(int userId, string subject, string body);
    }
}