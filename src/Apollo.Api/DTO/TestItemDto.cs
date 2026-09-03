using System.ComponentModel.DataAnnotations;

public sealed class TestItemDto
{
    [Required]
    public int Id { get; set; } = 0;

    [Required]
    public string Name { get; set; } = string.Empty;

}