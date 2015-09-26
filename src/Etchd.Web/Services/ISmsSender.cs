using System.Threading.Tasks;

namespace Etchd.Web.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}