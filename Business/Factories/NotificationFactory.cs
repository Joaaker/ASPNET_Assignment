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
}