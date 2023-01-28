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
public class TeamController : Controller
{
    private readonly MambaDbContext _mambaDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TeamController(MambaDbContext mambaDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _mambaDbContext = mambaDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //Read---------------------------------------------------------------------------------------------------------------
    public IActionResult Index()
    {
        List<Team> teams = _mambaDbContext.Teams.Include(p => p.Position).Where(d => d.isDeleted == false).ToList();
        return View(teams);
    }
    //Create-------------------------------------------------------------------------------------------------------------
    public IActionResult Create()
    {
        ViewBag.Positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Team team)
    {
        ViewBag.Positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        if (!ModelState.IsValid) return View(team);
        if (team.ImageFile == null)
        {
            ModelState.AddModelError("ImageFile", "Image cannot be empty");
            return View(team);
        }
        if (team.ImageFile.ContentType != "image/jpeg" && team.ImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("ImageFile", "Image should be in jpeg or png format");
            return View(team);
        }
        if (team.ImageFile.Length > 2097152)
        {
            ModelState.AddModelError("ImageFile", "The size of the image should be less than 2 MB");
            return View(team);
        }

        team.ImageName = FileManager.SaveFile(_webHostEnvironment.WebRootPath, "uploads/team", team.ImageFile);

        _mambaDbContext.Teams.Add(team);
        _mambaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //Update-------------------------------------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        ViewBag.Positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        Team team = _mambaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return View("Error-404");

        return View(team);
    }
    [HttpPost]
    public IActionResult Update(Team newTeam)
    {
        ViewBag.Positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        Team existTeam = _mambaDbContext.Teams.FirstOrDefault(x => x.Id == newTeam.Id);
        if (existTeam == null) return View("Error-404");
        if (!ModelState.IsValid) return View(existTeam);
        if (newTeam.ImageFile != null)
        {
            if (newTeam.ImageFile.ContentType != "image/jpeg" && newTeam.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "Image should be in jpeg or png format");
                return View(newTeam);
            }
            if (newTeam.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "The size of the image should be less than 2 MB");
                return View(newTeam);
            }
            FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/team", existTeam.ImageName);
            existTeam.ImageName = FileManager.SaveFile(_webHostEnvironment.WebRootPath, "uploads/team", newTeam.ImageFile);
        }
        existTeam.PositionId = newTeam.PositionId;
        existTeam.Fullname = newTeam.Fullname;
        existTeam.TwUrl = newTeam.TwUrl;
        existTeam.FbUrl = newTeam.FbUrl;
        existTeam.InstUrl = newTeam.InstUrl;
        existTeam.LInUrl = newTeam.LInUrl;
        _mambaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //SoftDelete---------------------------------------------------------------------------------------------------------
    public IActionResult SoftDelete(int id)
    {
        ViewBag.Positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        Team team = _mambaDbContext.Teams.FirstOrDefault(x => x.Id == id);
        if (team == null) return View("Error-404");

        team.isDeleted = true;
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}