using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class BookViewingCommandHandlerTest
    {
        private BookViewingCommandHandler _handler;
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
            _context.Properties.Returns(Substitute.For<IDbSet<Models.Property>>());
            _context.PropertyViewings.Returns(Substitute.For<IDbSet<Models.PropertyViewing>>());
            _handler = new BookViewingCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAddProperty()
        {
            var properties = new List<Models.Property>{
                new Models.Property{ Id = 1, PropertyType = "House", StreetName = "Smith Street", Description = "", IsListedForSale = true },
                new Models.Property{ Id = 2, PropertyType = "House", StreetName = "Jones Street", Description = "", IsListedForSale = true}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            _context.Properties.Find(1).Returns(properties[0]);

            // Arrange
            var command = new BookViewingCommand()
            {
                PropertyId = 1,
                ViewingAtDate = new System.DateTime(2017, 02, 02),
                ViewingAtTime = new System.DateTime(2017, 02, 02, 12, 59, 0)
            };

            // Act
            _handler.Handle(command, "buyer@purple.com");

            // Assert
            _context.PropertyViewings.Received(1);
            _context.Received(1).SaveChanges();
        }
    }
}
