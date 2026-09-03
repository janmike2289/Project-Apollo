using System.ComponentModel.DataAnnotations;

public sealed class CMItemDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Status { get; set; } = string.Empty;
}