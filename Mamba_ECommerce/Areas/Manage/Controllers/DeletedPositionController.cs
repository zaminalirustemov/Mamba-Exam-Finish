using Mamba_ECommerce.Context;
using Mamba_ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mamba_ECommerce.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin,Admin,Editor")]
public class DeletedPositionController : Controller
{
    private readonly MambaDbContext _mambaDbContext;

    public DeletedPositionController(MambaDbContext mambaDbContext)
    {
        _mambaDbContext = mambaDbContext;
    }
    //Read---------------------------------------------------------------------------------------------------------------
    public IActionResult Index()
    {
        List<Position> positions = _mambaDbContext.Positions.Where(d => d.isDeleted == true).ToList();
        return View(positions);
    }
    //Restore---------------------------------------------------------------------------------------------------------
    public IActionResult Restore(int id)
    {
        Position position = _mambaDbContext.Positions.FirstOrDefault(x => x.Id == id);
        if (position == null) return View("Error-404");

        position.isDeleted = false;
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    //HardDelete---------------------------------------------------------------------------------------------------------
    public IActionResult HardDelete(int id)
    {
        Position position = _mambaDbContext.Positions.FirstOrDefault(x => x.Id == id);
        if (position == null) return View("Error-404");

        _mambaDbContext.Positions.Remove(position);
        _mambaDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
