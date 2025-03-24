using Microsoft.AspNetCore.Http;

namespace Business.Dtos;

public class AddProjectForm {
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int Budget { get; set; }
        public int? ClientId { get; set; }
        public IFormFile ProjectImageFile { get; set; } = null!;
        public List<int> MembersIds { get; set; } = [];
}