using EventManagement.Models;
using EventManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Controllers
{
    public class GuestsController : Controller
    {
        private readonly EventModelContext _context;
        public GuestsController(EventModelContext context)
        {
            _context = context;
        }

        // GET: GuestsController
        public IActionResult Index()
        {
            return View();
        }

        // GET: GuestsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult List(int id)
        {
            JoinTable joinTable = new JoinTable();
            joinTable.PrivateGuests = _context.PrivateGuests.Where(p => p.EventId == id).ToList();
            joinTable.LegalPersons = _context.LegalPersons.Where(p => p.EventId == id).ToList();

            joinTable.EventId = id;

            return View(joinTable);
        }

        // GET: GuestsController/Add
        public IActionResult Add(int id)
        {
            List<string> guestTypesList = new List<string>();
            List<string> paymentOptionsList = new List<string>();

            var privateGuestString = nameof(PrivateGuest);
            var legalPersonString = nameof(LegalPerson);

            var cashPaymentString = nameof(PaymentOption.CashPayment);
            var creditCardPaymentString = nameof(PaymentOption.CreditCardPayment);

            guestTypesList.Add(privateGuestString);
            guestTypesList.Add(legalPersonString);

            paymentOptionsList.Add(cashPaymentString);
            paymentOptionsList.Add(creditCardPaymentString);

            ViewBag.GuestTypes = guestTypesList;
            ViewBag.PaymentOptions = paymentOptionsList;

            var viewModel = new GuestViewModel
            {
                PrivateGuestViewModel = new PrivateGuestViewModel(),
                LegalPersonViewModel = new LegalPersonViewModel(),
                EventId = id,
                PaymentOptions = paymentOptionsList
            };

            return View(viewModel);
        }

        // POST: GuestsController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(GuestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                GetGuestAndPaymentTypes();

                return View(viewModel);
            }

            if (viewModel.PrivateGuestViewModel != null)
            {
                var privateGuestInDb = _context.PrivateGuests.Where(p => p.PersonalIdentificationCode == viewModel.PrivateGuestViewModel.PersonalIdentificationCode && p.EventId == viewModel.EventId).FirstOrDefault();
                if (privateGuestInDb == null)
                {
                    var privateGuest = new PrivateGuest
                    {
                        FirstName = viewModel.PrivateGuestViewModel.FirstName,
                        LastName = viewModel.PrivateGuestViewModel.LastName,
                        PersonalIdentificationCode = viewModel.PrivateGuestViewModel.PersonalIdentificationCode,
                        PaymentOption = viewModel.PrivateGuestViewModel.PaymentOption,
                        AdditionalInfo = viewModel.PrivateGuestViewModel.AdditionalInfo,
                        EventId = viewModel.EventId
                    };

                    var eventInDb = _context.Events.Where(e => e.Id == viewModel.EventId).FirstOrDefault();
                    if (eventInDb != null)
                    {
                        eventInDb.NumberOfGuests += 1;

                        _context.Events.Update(eventInDb);
                        _context.PrivateGuests.Add(privateGuest);

                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Events");
                    }
                    else 
                    {
                        return NotFound();
                    }
                }
                else
                {
                    GetGuestAndPaymentTypes();
                    return View(viewModel);
                }
            }

            if (viewModel.LegalPersonViewModel != null)
            {
                var legalPersonInDb = _context.LegalPersons.Where(p => p.RegistryCode == viewModel.LegalPersonViewModel.RegistryCode && p.EventId == viewModel.EventId).FirstOrDefault();
                if (legalPersonInDb == null)
                {
                    var legalPerson = new LegalPerson
                    {
                        Name = viewModel.LegalPersonViewModel.Name,
                        RegistryCode = viewModel.LegalPersonViewModel.RegistryCode,
                        NumberOfGuests = viewModel.LegalPersonViewModel.NumberOfGuests,
                        PaymentOption = viewModel.LegalPersonViewModel.PaymentOption,
                        AdditionalInfo = viewModel.LegalPersonViewModel.AdditionalInfo,
                        EventId = viewModel.EventId
                    };

                    var eventInDb = _context.Events.Where(e => e.Id == viewModel.EventId).FirstOrDefault();
                    if (eventInDb != null)
                    {
                        eventInDb.NumberOfGuests += viewModel.LegalPersonViewModel.NumberOfGuests;

                        _context.Events.Update(eventInDb);
                        _context.LegalPersons.Add(legalPerson);

                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Events");
                    }
                    else 
                    {
                        return NotFound();
                    }
                }
                else
                {
                    GetGuestAndPaymentTypes();
                    return View(viewModel);
                }
            }

            return RedirectToAction("Index", "Events");
        }

        public void GetGuestAndPaymentTypes()
        {
            List<string> guestTypesList = new List<string>();
            List<string> paymentOptionsList = new List<string>();

            var privateGuestString = nameof(PrivateGuest);
            var legalPersonString = nameof(LegalPerson);

            var cashPaymentString = nameof(PaymentOption.CashPayment);
            var creditCardPaymentString = nameof(PaymentOption.CreditCardPayment);

            guestTypesList.Add(privateGuestString);
            guestTypesList.Add(legalPersonString);

            paymentOptionsList.Add(cashPaymentString);
            paymentOptionsList.Add(creditCardPaymentString);

            ViewBag.GuestTypes = guestTypesList;
            ViewBag.PaymentOptions = paymentOptionsList;
        }

        // GET: GuestsController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: GuestsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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

        // GET: GuestsController/Delete/5
        public IActionResult Delete(DeleteGuestViewModel viewModel)
        {
            return View(viewModel);
        }

        // POST: GuestsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, string guesttype, int eventid)
        {
            if (guesttype == "privateguest")
            { 
                var privateGuest = _context.PrivateGuests.Find(id);
                if (privateGuest == null)
                {
                    return NotFound();
                }

                privateGuest.EventId = null;
                _context.PrivateGuests.Update(privateGuest);

                var eventInDb = _context.Events.Find(eventid);
                eventInDb.NumberOfGuests -= 1;
                _context.Events.Update(eventInDb);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Events");
            }

            if (guesttype == "legalperson")
            {
                var legalPerson = _context.LegalPersons.Find(id);
                if (legalPerson == null)
                {
                    return NotFound();
                }

                legalPerson.EventId = null;
                _context.LegalPersons.Update(legalPerson);

                var eventInDb = _context.Events.Find(eventid);
                var count = legalPerson.NumberOfGuests;

                eventInDb.NumberOfGuests -= count;
                _context.Events.Update(eventInDb);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Events");
            }

            return RedirectToAction("Index", "Events");
        }
    }
}
