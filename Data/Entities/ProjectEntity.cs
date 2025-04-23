using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    public string ProjectImageUri { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateOnly StartDate { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateOnly EndDate { get; set; }

    public int? Budget { get; set; }

    public int StatusId { get; set; }
    public StatusEntity Status { get; set; } = null!;

    public int ClientId { get; set; }
    public ClientEntity Client { get; set; } = null!;

    public ICollection<ProjectMemberJunctionEntity> ProjectMembers { get; set; } = [];
}