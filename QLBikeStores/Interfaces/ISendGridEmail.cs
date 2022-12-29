using System.Threading.Tasks;

namespace QLBikeStores.Interfaces
{
    public interface ISendGridEmail
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
