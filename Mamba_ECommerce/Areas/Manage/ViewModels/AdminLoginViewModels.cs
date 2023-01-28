using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Mamba_ECommerce.Areas.Manage.ViewModels;
public class AdminLoginViewModels
{
    [Required]
    [StringLength(maximumLength:50)]
    public string Username { get; set; }
    [Required]
    [StringLength(maximumLength: 20,MinimumLength =8),DataType(DataType.Password)]
    public string Password { get; set; }
}
