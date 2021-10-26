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

namespace EventManagement.Tests
{
    public class EventsControllerTests
    {
        [Fact]
        public void Index_ReturnsARedirectActionResult()
        {
            // arrange
            var unit = new Mock<IUnitOfWork>();
            var controller = new EventsController(unit.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Index_RedirectToListActionMethod()
        {
            // arrange
            var unit = new Mock<IUnitOfWork>();
            var controller = new EventsController(unit.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void Create_ModelIsAEventViewModelObject()
        { 
            // arrange
            var unit = new Mock<IUnitOfWork>();

            var controller = new EventsController(unit.Object);
            
            // act
            var model = controller.Create().ViewData.Model as EventViewModel;

            // assert
            Assert.IsType<EventViewModel>(model);
        }

        [Fact]
        public void Create_ReturnsViewResultIfModelIsNotValid()
        {
            // arrange
            var unit = new Mock<IUnitOfWork>();
            var controller = new EventsController(unit.Object);
            controller.ModelState.AddModelError("", "Test error message.");
            EventViewModel vm = new EventViewModel() { Id = 1, EventName = null, Date = null, Venue = null, NumberOfGuests = null, AdditionalInfo = "Additional info 1" };

            // act
            var result = controller.Create(vm);

            // assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
