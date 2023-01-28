using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mamba_ECommerce.Models;
public class AppUser:IdentityUser
{
    [StringLength(maximumLength:50)]
    public string Fullname { get; set; }
}