namespace ProjectManager.Application.Dtos;

public class ProjectDto
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int StatusId { get; set; }
    public string Banner { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public List<TaskDto> Tasks { get; set; } = null!;
    public List<UserDto> Members { get; set; } = null!;
}