using EventManagement.Models;
using EventManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventModelContext _context;
        public EventsController(EventModelContext context)
        {
            _context = context;
        }
        // GET: EventsController
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();

            return View(events);
        }

        // GET: EventsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: EventsController/Add
        public IActionResult Create()
        {
            var viewModel = new EventViewModel();

            return View(viewModel);
        }

        // POST: EventsController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var eventModel = new Event
            {
                EventName = viewModel.EventName,
                Date = viewModel.Date,
                Venue = viewModel.Venue,
                AdditionalInfo = viewModel.AdditionalInfo,
                NumberOfGuests = 0
            };

            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: EventsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
