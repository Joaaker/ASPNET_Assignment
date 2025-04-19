using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class NotificationRepository(DataContext context) : BaseRepository<NotificationEntity, NotificationEntity>(context), INotificationRepository
{
    public async Task<IEnumerable<NotificationEntity>> GetNotificationsByUserId(string userId, int take = 10)
    {
        var dismissedIds = await _context.DismissedNotifications
            .Where(x => x.UserId == userId)
            .Select(x => x.NotificationId)
            .ToListAsync();

        var notifications = await _context.Notifications
            .Where(x => !dismissedIds.Contains(x.Id))
            .OrderByDescending(x => x.Created)
            .Take(take)
            .ToListAsync();

        return notifications;
    }
}