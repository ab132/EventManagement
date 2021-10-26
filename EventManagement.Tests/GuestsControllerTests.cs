using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using EventManagement.Controllers;
using EventManagement.Models;
using EventManagement.Core;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Core.ViewModels;
using EventManagement.ViewModels;
using EventManagement.Core.Repositories;

namespace EventManagement.Tests
{
    public class GuestsControllerTests
    {
        [Fact]
        public void EditPrivateGuest_GET_ModelIsPrivateGuestObject()
        {
            // arrange
            var guestRep = new Mock<IPrivateGuestRepository>();
            guestRep.Setup(m => m.GetPrivateGuest(It.IsAny<int>()))
                .Returns(new PrivateGuest() { PaymentOption = PaymentOption.CashPayment});
            var unit = new Mock<IUnitOfWork>();
            unit.Setup(m => m.PrivateGuests).Returns(guestRep.Object);

            var controller = new GuestsController(unit.Object);

            // act
            var model = controller.EditPrivateGuest(3).ViewData.Model as PrivateGuestViewModel;

            // assert
            Assert.IsType<PrivateGuestViewModel>(model);
        }

        [Fact]
        public void EditLegalPerson_GET_ModelIsLegalPersonObject()
        {
            // arrange
            var legalRep = new Mock<ILegalPersonRepository>();
            legalRep.Setup(m => m.GetLegalPerson(It.IsAny<int>()))
                .Returns(new LegalPerson() { PaymentOption = PaymentOption.CashPayment });
            var unit = new Mock<IUnitOfWork>();
            unit.Setup(m => m.LegalPersons).Returns(legalRep.Object);

            var controller = new GuestsController(unit.Object);

            // act
            var model = controller.EditLegalPerson(1).ViewData.Model as LegalPersonViewModel;

            // assert
            Assert.IsType<LegalPersonViewModel>(model);
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // arrange
            var unit = new Mock<IUnitOfWork>();
            var controller = new GuestsController(unit.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditPrivateGuest_POST_ReturnsViewResultIfModelIsNotValid()
        {
            // arrange
            var unit = new Mock<IUnitOfWork>();
            var controller = new GuestsController(unit.Object);
            controller.ModelState.AddModelError("", "Test error message.");
            PrivateGuestViewModel vm = new PrivateGuestViewModel() { Id = 3, FirstName = "FirstName1", LastName = "LastName1", PersonalIdentificationCode = "12345673", PaymentOption = (PaymentOption)1, AdditionalInfo = "Additionalinfo3" };

            // act
            var result = controller.EditPrivateGuest(vm);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditLegalPerson_POST_ReturnsViewResultIfModelIsNotValid()
        {
            // arrange
            var unit = new Mock<IUnitOfWork>();
            var controller = new GuestsController(unit.Object);
            controller.ModelState.AddModelError("", "Test error message.");
            LegalPersonViewModel vm = new LegalPersonViewModel() { Id = 1, Name = "Company1", RegistryCode = "123456711", NumberOfGuests = 4, PaymentOption = (PaymentOption)1, AdditionalInfo = "Additionalinfo11" };

            // act
            var result = controller.EditLegalPerson(vm);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Add_ReturnsViewResultIfModelIsNotValid()
        {
            // arrange
            var unit = new Mock<IUnitOfWork>();
            var controller = new GuestsController(unit.Object);
            controller.ModelState.AddModelError("", "Test error message.");
            GuestViewModel vm = new GuestViewModel() { EventId = 1, PrivateGuestViewModel = null, LegalPersonViewModel = null, PaymentOptions = null, GuestType = null };

            // act
            var result = controller.Add(vm);

            // assert
            Assert.IsType<ViewResult>(result);
        }

    }
}
