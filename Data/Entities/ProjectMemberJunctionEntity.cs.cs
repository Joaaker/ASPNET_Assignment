namespace Data.Entities;

public class ProjectMemberJunctionEntity
{
    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public MemberEntity Member { get; set; } = null!;
}