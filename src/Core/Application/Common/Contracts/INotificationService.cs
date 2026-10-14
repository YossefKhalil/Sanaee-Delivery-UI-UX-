

namespace Core.Application.Common.Contracts
{
    public interface INotificationService
    {
        Task SendPushAsync(int UserId, string Title, string Message);
        Task SendPushNotificationAsync(int userId, string Title, string Message);
    }
}
