using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class UserController : Controller
    {
        HotelDbContext db;
        public UserController(HotelDbContext db)
        {
            this.db = db;
        }
        public IActionResult HotelRoom()
        {
            return View(db.Rooms.ToList());
        }
    }
}
