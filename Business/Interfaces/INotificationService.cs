using Data.Entities;
using Domain.Dtos;

namespace Business.Interfaces;

public interface INotificationService
{
    Task<IResponseResult> AddNotificationAsync(NotificationDto dto);
    Task<IResponseResult> DismissNotificationAsync(string notificationId, string userId);
    Task<IResponseResult<IEnumerable<NotificationEntity>>> GetNotificationsAsync(string userId);
}
