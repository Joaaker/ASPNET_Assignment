using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class MemberEntity : IdentityUser
{
    public string? ImageUri { get; set; }

    [ProtectedPersonalData]
    public string? FirstName { get; set; }

    [ProtectedPersonalData]
    public string? LastName { get; set; }

    [ProtectedPersonalData]
    public string? JobTitle { get; set; }

    [ProtectedPersonalData]
    public DateOnly? DateOfBirth { get; set; }

    [ProtectedPersonalData]
    public virtual MemberAddressEntity? Address { get; set; }

    public ICollection<ProjectMemberJunctionEntity> Projects { get; set; } = [];

    public ICollection<NotificationDismissedEntity> DismissedNotifications { get; set; } = [];

}
