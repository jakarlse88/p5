using System.Threading.Tasks;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Services
{
    public interface IMessageService
    {
        Task<bool> SendEmail(ContactInputModel inputModel);
    }
}