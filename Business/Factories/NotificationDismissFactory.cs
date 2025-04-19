using Data.Entities;
using Domain.Dtos;

namespace Business.Factories;

public class NotificationDismissFactory
{
    public static NotificationDismissedEntity CreateEntity(string notificationId, string userId) => new()
    {
        NotificationId = notificationId,
        UserId = userId
    };
}
