using System;
using System.Collections.Generic;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(BookViewingCommand command, string buyerUserId)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var viewing = new PropertyViewing
            {
                BuyerUserId = buyerUserId,
                Status = ViewingStatus.Pending,
                ViewingAt = new DateTime(command.ViewingAtDate.Year, command.ViewingAtDate.Month, command.ViewingAtDate.Day, command.ViewingAtTime.Hour, command.ViewingAtDate.Minute,0),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            if (property.Viewings == null)
            {
                property.Viewings = new List<PropertyViewing>();
            }
                
            property.Viewings.Add(viewing);
            
            _context.SaveChanges();
        }
    }
}