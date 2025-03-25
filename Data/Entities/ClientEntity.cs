using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ClientEntity
{
    [Key]
    public int Id { get; set; }

    public string ClientName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}