using Data.Entities;
using Domain.Dtos;

namespace Business.Factories;

public class NotificationFactory
{
    public static NotificationEntity CreateEntity(NotificationDto dto) => new()
    {
        NotificationTargetGroupId = dto.NotificationTargetGroupId,
        NotificationTypeId = dto.NotificationTypeId,
        Icon = dto.Image,
        Message = dto.Message
    };

    public static NotificationDto CreateDto(int notificationTargetGroupId, int notificationTypeId, string message, string? image) => new()
    {
        NotificationTargetGroupId = notificationTargetGroupId,
        NotificationTypeId = notificationTypeId,
        Message = message,
        Image = image ?? "~/Images/Profiles/user-template.svg"
    };
}