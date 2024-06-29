using System.ComponentModel.DataAnnotations;

namespace Noppes.E621.Tests;

public class E621Options
{
    public const string SectionName = "E621";

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string ApiKey { get; init; }
}
