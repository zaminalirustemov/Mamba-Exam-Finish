using Mamba_ECommerce.Context;
using Mamba_ECommerce.Helpers;
using Mamba_ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mamba_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class PositionController : Controller
{
    private readonly MambaDbContext _mambaDbContext;

    public PositionController(MambaDbContext mambaDbContext)
    {
        _mambaDbContext = mambaDbContext;
    }
    //Read---------------------------------------------------------------------------------------------------------------
    public IActionResult Index()
    {
        List<Position> positions = _mambaDbContext.Positions.Where(d => d.isDeleted == false).ToList();
        return View(positions);
    }
    //Create-------------------------------------------------------------------------------------------------------------
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Position position)
    {
        if (!ModelState.IsValid) return View(position);

        _mambaDbContext.Positions.Add(position);
        _mambaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //Update-------------------------------------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        Position position = _mambaDbContext.Positions.FirstOrDefault(x => x.Id == id);
        if (position == null) return View("Error-404");

        return View(position);
    }
    [HttpPost]
    public IActionResult Update(Position newPosition)
    {
        Position existPosition = _mambaDbContext.Positions.FirstOrDefault(x => x.Id == newPosition.Id);
        if (existPosition == null) return View("Error-404");
        if (!ModelState.IsValid) return View(existPosition);

        existPosition.Name = newPosition.Name;
        _mambaDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //SoftDelete---------------------------------------------------------------------------------------------------------
    public IActionResult SoftDelete(int id)
    {
        Position position = _mambaDbContext.Positions.FirstOrDefault(x => x.Id == id);
        if (position == null) return View("Error-404");

        position.isDeleted = true;
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}