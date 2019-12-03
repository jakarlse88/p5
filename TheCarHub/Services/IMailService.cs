using System.Threading.Tasks;

namespace TheCarHub.Services
{
    public interface IMailService
    {
        void SendEmail(string subject, string message);
    }
}