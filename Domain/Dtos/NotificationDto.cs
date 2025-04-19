namespace Domain.Dtos;

public class NotificationDto
{
    public int NotificationTargetGroupId { get; set; }
    public int NotificationTypeId { get; set; }
    public string Image { get; set; } = null!;
    public string Message { get; set; } = null!;
}
