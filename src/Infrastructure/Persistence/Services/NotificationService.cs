

using Core.Application.Common.Contracts;

namespace Infrastructure.Persistence.Services
{
    public class NotificationService : INotificationService
    {
        public async Task SendPushAsync(int UserId, string Title, string Message)
        {
            await Task.CompletedTask;
        }

        public async Task SendPushNotificationAsync(int userId, string Title, string Message)
        {
            await Task.CompletedTask;
        }
    }
}
