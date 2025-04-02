using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class NotificationDismissedEntity
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Member))]
    public string UserId { get; set; } = null!;
    public MemberEntity Member { get; set; } = null!;

    [ForeignKey(nameof(Notification))]
    public string NotificationId { get; set; } = null!;
    public NotificationEntity Notification { get; set; } = null!;

}
