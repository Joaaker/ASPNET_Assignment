using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class NotificationDismissRepository(DataContext context) : BaseRepository<NotificationDismissedEntity, NotificationDismissedEntity>(context), INotificationDismissRepository
{
}
