using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

//Image?
public class MemberEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string? FirstName { get; set; }

    [ProtectedPersonalData]
    public string? LastName { get; set; }

    [ProtectedPersonalData]
    public string? JobTitle { get; set; }

    [ProtectedPersonalData]
    public DateOnly? DateOfBirth { get; set; }

    [ProtectedPersonalData]
    public virtual MemberAdressEntity? Address { get; set; }

    public ICollection<ProjectMemberJunctionEntity> Projects { get; set; } = [];
}
