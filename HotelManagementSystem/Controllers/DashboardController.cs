﻿using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{

    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private HotelDbContext db;
        public DashboardController(HotelDbContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.db = db;
        }
        public IActionResult AdminDashboard()
        {
            return View(db.Rooms.ToList());
        }
        [HttpGet]
        public IActionResult AddRoom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRoom(Room room)
        {
            db.Rooms.Add(room);
            db.SaveChanges();
            return RedirectToAction("AdminDashboard");
        }
        [HttpGet]
        public IActionResult EditRoom(string roomName)
        {
            Room toEdit = db.Rooms.FirstOrDefault(x => x.RoomName.Equals(roomName));
            return View(toEdit);
        }
        [HttpPost]
        public IActionResult EditRoom(Room room)
        {
            Room toEdit = db.Rooms.FirstOrDefault(x => x.RoomName.Equals(room.RoomName));
            toEdit.Capacity = room.Capacity;
            toEdit.Fair = room.Fair;
            toEdit.Type = room.Type;
            toEdit.IsBooked = room.IsBooked;
            db.SaveChanges();
            return RedirectToAction("AdminDashboard");
        }
        public IActionResult UserDashboard()
        {
            User user = db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
            return View(user);
        }
        public IActionResult History()
        {
            List<Order> previous = new List<Order>();
            User user = db.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
            foreach (var order in db.Orders.ToList())
            {
                if(order.Email.Equals(user.Email))
                {
                    previous.Add(order);
                }
            }
            return View(previous);
        }
    }
}
