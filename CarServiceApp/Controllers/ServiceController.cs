using System.Linq;
using System.Web.Mvc;
using CarServiceApp.Models;

public class ServiceController : Controller
{
    private readonly CarServiceDbContext _context = new CarServiceDbContext();

    [HttpGet]
    public ActionResult Booking() => View();

    [HttpPost]
    public ActionResult Booking(Service service)
    {
        service.UserId = (int)Session["UserId"];
        service.Status = "Scheduled";
        _context.Services.Add(service);
        _context.SaveChanges();
        return RedirectToAction("History");
    }

    public ActionResult History()
    {
        int userId = (int)Session["UserId"];
        var services = _context.Services.Where(s => s.UserId == userId).ToList();
        return View(services);
    }

    public ActionResult Updates(int id)
    {
        var service = _context.Services.FirstOrDefault(s => s.Id == id);
        return View(service);
    }
}
    