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
        public ViewResult Index()
        {
            return View();
        }

        // GET: GuestsController/Details/5
        public IActionResult PrivateGuestDetails(int privateGuestId)
        {
            var privateGuestInDb = _unitOfWork.PrivateGuests.GetPrivateGuest(privateGuestId);

            PrivateGuestViewModel privateGuestViewModel = new PrivateGuestViewModel();
            privateGuestViewModel.Id = privateGuestInDb.Id;
            privateGuestViewModel.FirstName = privateGuestInDb.FirstName;
            privateGuestViewModel.LastName = privateGuestInDb.LastName;
            privateGuestViewModel.PersonalIdentificationCode = privateGuestInDb.PersonalIdentificationCode;
            privateGuestViewModel.PaymentOption = (PaymentOption)privateGuestInDb.PaymentOption;
            privateGuestViewModel.AdditionalInfo = privateGuestInDb.AdditionalInfo;
            privateGuestViewModel.EventId = privateGuestInDb.EventId;
            privateGuestViewModel.EventViewModel.EventName = privateGuestInDb.Event.EventName;

            return View(privateGuestViewModel);
        }

        // GET: GuestsController/Details/5
        public IActionResult LegalPersonDetails(int legalPersonId)
        {
            var legalPersonInDb = _unitOfWork.LegalPersons.GetLegalPerson(legalPersonId);

            LegalPersonViewModel legalPersonViewModel = new LegalPersonViewModel();
            legalPersonViewModel.Id = legalPersonInDb.Id;
            legalPersonViewModel.Name = legalPersonInDb.Name;
            legalPersonViewModel.RegistryCode = legalPersonInDb.RegistryCode;
            legalPersonViewModel.NumberOfGuests = legalPersonInDb.NumberOfGuests;
            legalPersonViewModel.PaymentOption = (PaymentOption)legalPersonInDb.PaymentOption;
            legalPersonViewModel.EventId = legalPersonInDb.EventId;
            legalPersonViewModel.EventViewModel.EventName = legalPersonInDb.Event.EventName;


            return View(legalPersonViewModel);

        }

        public IActionResult List(int eventId)
        {
            JoinTable joinTable = new JoinTable();

            joinTable.PrivateGuests = _unitOfWork.PrivateGuests.GetPrivateGuests(eventId);
            joinTable.LegalPersons = _unitOfWork.LegalPersons.GetLegalPersons(eventId);

            joinTable.EventId = eventId;

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
                newPrivateGuest.PaymentOption = (PaymentOption)viewModel.PrivateGuestViewModel.PaymentOption;
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
                newLegalPerson.PaymentOption = (PaymentOption)viewModel.LegalPersonViewModel.PaymentOption;
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
        [HttpGet]
        public ViewResult EditPrivateGuest(int privateGuestId)
        {
            List<string> paymentOptionsList = new List<string>();

            var cashPaymentString = nameof(PaymentOption.CashPayment);
            var creditCardPaymentString = nameof(PaymentOption.CreditCardPayment);

            paymentOptionsList.Add(cashPaymentString);
            paymentOptionsList.Add(creditCardPaymentString);

            ViewBag.PaymentOptions = paymentOptionsList;

            var privateGuest = _unitOfWork.PrivateGuests.GetPrivateGuest(privateGuestId);

            PrivateGuestViewModel viewModel = new PrivateGuestViewModel();

            viewModel.Id = privateGuest.Id;
            viewModel.FirstName = privateGuest.FirstName;
            viewModel.LastName = privateGuest.LastName;
            viewModel.PersonalIdentificationCode = privateGuest.PersonalIdentificationCode;
            viewModel.PaymentOption = (PaymentOption)privateGuest.PaymentOption;
            viewModel.AdditionalInfo = privateGuest.AdditionalInfo;
            viewModel.EventId = privateGuest.EventId;

            return View(viewModel);
        }

        // GET: GuestsController/Edit/5
        public ViewResult EditLegalPerson(int legalPersonId)
        {
            List<PaymentOption> paymentOptionsList = new List<PaymentOption>();

            var cashPaymentString = PaymentOption.CashPayment;
            var creditCardPaymentString = PaymentOption.CreditCardPayment;

            paymentOptionsList.Add(cashPaymentString);
            paymentOptionsList.Add(creditCardPaymentString);

            ViewBag.PaymentOptions = paymentOptionsList;

            var legalPerson = _unitOfWork.LegalPersons.GetLegalPerson(legalPersonId);

            LegalPersonViewModel viewModel = new LegalPersonViewModel();

            viewModel.Id = legalPerson.Id;
            viewModel.Name = legalPerson.Name;
            viewModel.RegistryCode = legalPerson.RegistryCode;
            viewModel.NumberOfGuests = legalPerson.NumberOfGuests;
            viewModel.PaymentOption = (PaymentOption)legalPerson.PaymentOption;
            viewModel.AdditionalInfo = legalPerson.AdditionalInfo;
            viewModel.EventId = legalPerson.EventId;

            return View(viewModel);
        }

        // POST: GuestsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPrivateGuest(PrivateGuestViewModel privateGuestViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(privateGuestViewModel);
            }

            var privateGuest = _unitOfWork.PrivateGuests.GetPrivateGuest(privateGuestViewModel.Id);

            if (privateGuest == null)
                return NotFound();

            privateGuest.Id = privateGuestViewModel.Id;
            privateGuest.FirstName = privateGuestViewModel.FirstName;
            privateGuest.LastName = privateGuestViewModel.LastName;
            privateGuest.PersonalIdentificationCode = privateGuestViewModel.PersonalIdentificationCode;
            privateGuest.PaymentOption = (PaymentOption)privateGuestViewModel.PaymentOption;
            privateGuest.AdditionalInfo = privateGuestViewModel.AdditionalInfo;
            privateGuest.EventId = privateGuestViewModel.EventId;

            _unitOfWork.PrivateGuests.Update(privateGuest);
            _unitOfWork.Complete();

            return RedirectToAction("Index", "Events");
        }

        // POST: GuestsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditLegalPerson(LegalPersonViewModel legalPersonViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(legalPersonViewModel);
            }

            var legalPerson = _unitOfWork.LegalPersons.GetLegalPerson(legalPersonViewModel.Id);

            if (legalPerson == null)
                return NotFound();

            legalPerson.Id = legalPersonViewModel.Id;
            legalPerson.Name = legalPersonViewModel.Name;
            legalPerson.RegistryCode = legalPersonViewModel.RegistryCode;
            legalPerson.NumberOfGuests = legalPersonViewModel.NumberOfGuests;
            legalPerson.PaymentOption = (PaymentOption)legalPersonViewModel.PaymentOption;
            legalPerson.AdditionalInfo = legalPersonViewModel.AdditionalInfo;
            legalPerson.EventId = legalPersonViewModel.EventId;

            _unitOfWork.LegalPersons.Update(legalPerson);
            _unitOfWork.Complete();

            return RedirectToAction("Index", "Events");
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
