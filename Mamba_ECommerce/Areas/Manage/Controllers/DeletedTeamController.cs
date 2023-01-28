using Mamba_ECommerce.Context;
using Mamba_ECommerce.Helpers;
using Mamba_ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Mamba_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class DeletedTeamController : Controller
{
    private readonly MambaDbContext _mambaDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DeletedTeamController(MambaDbContext mambaDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _mambaDbContext = mambaDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //Read---------------------------------------------------------------------------------------------------------------
    public IActionResult Index()
    {
        List<Team> teams = _mambaDbContext.Teams.Include(p => p.Position).Where(d => d.isDeleted == true).ToList();
        return View(teams);
    }
    //Restore-------------------------------------------------------------------------------------------------------------
    public IActionResult Restore(int id)
    {
        ViewBag.Positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        Team team = _mambaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return View("Error-404");

        team.isDeleted = false;
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    //HardDelete---------------------------------------------------------------------------------------------------------
    public IActionResult HardDelete(int id)
    {
        ViewBag.Positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        Team team = _mambaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return View("Error-404");

        FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/team", team.ImageName);
        _mambaDbContext.Teams.Remove(team);
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
