using Mamba_ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mamba_ECommerce.Context;
public class MambaDbContext: IdentityDbContext
{
	public MambaDbContext(DbContextOptions<MambaDbContext> options) : base(options) { }

	public DbSet<Position> Positions { get; set; }
	public DbSet<Team> Teams { get; set; }
	public DbSet<AppUser> AppUsers { get; set; }
}
