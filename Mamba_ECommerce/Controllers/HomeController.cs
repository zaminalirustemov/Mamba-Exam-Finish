using Mamba_ECommerce.Context;
using Mamba_ECommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mamba_ECommerce.Controllers;
public class HomeController : Controller
{
    private readonly MambaDbContext _mambaDbContext;

    public HomeController(MambaDbContext mambaDbContext)
    {
        _mambaDbContext = mambaDbContext;
    }
    public IActionResult Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel
        {
            Teams = _mambaDbContext.Teams.Include(p => p.Position).Where(d => d.isDeleted == false).ToList()
        };
        return View(homeViewModel);
    }
}