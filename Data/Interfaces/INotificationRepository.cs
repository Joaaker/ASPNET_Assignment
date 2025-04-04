using Data.Entities;

namespace Data.Interfaces;

public interface INotificationRepository : IBaseRepository<NotificationEntity, NotificationEntity>
{
    Task<IEnumerable<NotificationEntity>> GetNotificationsByUserId(string userId, int take = 10);
}