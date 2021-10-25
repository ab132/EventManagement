using EventManagement.Core;
using EventManagement.Core.ViewModels;
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
        private readonly IUnitOfWork _unitOfWork;
        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: EventsController
        public IActionResult Index()
        {
            var events = _unitOfWork.Events.GetEvents();

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
        public IActionResult Create(EventViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var eventModel = new Models.Event
            {
                EventName = viewModel.EventName,
                Date = viewModel.Date,
                Venue = viewModel.Venue,
                AdditionalInfo = viewModel.AdditionalInfo,
                NumberOfGuests = 0
            };

            _unitOfWork.Events.Add(eventModel);
            _unitOfWork.Complete();

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
        public ActionResult Delete(DeleteEventViewModel viewModel)
        {
            return View(viewModel);
        }

        // POST: EventsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var eventInDb = _unitOfWork.Events.Find(id);

            if (eventInDb != null)
            { 
                _unitOfWork.Events.Delete(eventInDb);
                _unitOfWork.Complete();

                return RedirectToAction("Index", "Events");
            }

            return View();
        }
    }
}
