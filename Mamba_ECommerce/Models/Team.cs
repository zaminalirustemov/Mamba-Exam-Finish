using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mamba_ECommerce.Models;
public class Team
{
    public int Id { get; set; }
    public int PositionId { get; set; }

    [StringLength(maximumLength: 100)]
    public string? ImageName { get; set; }
    [StringLength(maximumLength: 50)]
    public string Fullname { get; set; }
    [StringLength(maximumLength: 150)]
    public string? TwUrl { get; set; }
    [StringLength(maximumLength: 150)]
    public string? FbUrl { get; set; }
    [StringLength(maximumLength: 15)]
    public string? InstUrl { get; set; }
    [StringLength(maximumLength: 150)]
    public string? LInUrl { get; set; }
    public bool isDeleted { get; set; }

    public Position? Position { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
