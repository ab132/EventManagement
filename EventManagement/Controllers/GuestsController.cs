using EventManagement.Core;
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
        private readonly IUnitOfWork _unitOfWork;
        public GuestsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            
            joinTable.PrivateGuests = _unitOfWork.PrivateGuests.GetPrivateGuests(id);
            joinTable.LegalPersons = _unitOfWork.LegalPersons.GetLegalPersons(id);

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
        public IActionResult Add(GuestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                GetGuestAndPaymentTypes();

                return View(viewModel);
            }

            if (viewModel.PrivateGuestViewModel != null)
            {
                PrivateGuest newPrivateGuest = new PrivateGuest();

                newPrivateGuest.FirstName = viewModel.PrivateGuestViewModel.FirstName;
                newPrivateGuest.LastName = viewModel.PrivateGuestViewModel.LastName;
                newPrivateGuest.PersonalIdentificationCode = viewModel.PrivateGuestViewModel.PersonalIdentificationCode;
                newPrivateGuest.PaymentOption = viewModel.PrivateGuestViewModel.PaymentOption;
                newPrivateGuest.AdditionalInfo = viewModel.PrivateGuestViewModel.AdditionalInfo;
                newPrivateGuest.EventId = viewModel.EventId;

                _unitOfWork.PrivateGuests.Add(newPrivateGuest);

                var eventInDb = _unitOfWork.Events.GetEvent(viewModel.EventId);
                eventInDb.NumberOfGuests += 1;
                _unitOfWork.Events.Update(eventInDb);

                _unitOfWork.Complete();
                return RedirectToAction("Index", "Events");
            }

            if (viewModel.LegalPersonViewModel != null)
            {
                LegalPerson newLegalPerson = new LegalPerson();

                newLegalPerson.Name = viewModel.LegalPersonViewModel.Name;
                newLegalPerson.RegistryCode = viewModel.LegalPersonViewModel.RegistryCode;
                newLegalPerson.NumberOfGuests = viewModel.LegalPersonViewModel.NumberOfGuests;
                newLegalPerson.PaymentOption = viewModel.LegalPersonViewModel.PaymentOption;
                newLegalPerson.AdditionalInfo = viewModel.LegalPersonViewModel.AdditionalInfo;
                newLegalPerson.EventId = viewModel.EventId;

                _unitOfWork.LegalPersons.Add(newLegalPerson);

                var eventInDb = _unitOfWork.Events.GetEvent(viewModel.EventId);
                eventInDb.NumberOfGuests += viewModel.LegalPersonViewModel.NumberOfGuests;
                _unitOfWork.Events.Update(eventInDb);

                _unitOfWork.Complete();
                return RedirectToAction("Index", "Events");
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
        public IActionResult Delete(int id, string guesttype, int eventid)
        {
            if (guesttype == "privateguest")
            {
                var privateGuest = _unitOfWork.PrivateGuests.Find(id);
                if (privateGuest == null)
                {
                    return NotFound();
                }

                privateGuest.EventId = null;
                _unitOfWork.PrivateGuests.Update(privateGuest);

                var eventInDb = _unitOfWork.Events.Find(eventid);
                eventInDb.NumberOfGuests -= 1;
                _unitOfWork.Events.Update(eventInDb);

                _unitOfWork.Complete();

                return RedirectToAction("Index", "Events");
            }

            if (guesttype == "legalperson")
            {
                var legalPerson = _unitOfWork.LegalPersons.Find(id);
                if (legalPerson == null)
                {
                    return NotFound();
                }

                legalPerson.EventId = null;
                _unitOfWork.LegalPersons.Update(legalPerson);

                var eventInDb = _unitOfWork.Events.Find(eventid);
                var count = legalPerson.NumberOfGuests;

                eventInDb.NumberOfGuests -= count;
                _unitOfWork.Events.Update(eventInDb);

                _unitOfWork.Complete();

                return RedirectToAction("Index", "Events");
            }

            return RedirectToAction("Index", "Events");
        }
    }
}
