using System;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class BookViewingViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public BookViewingViewModel Build(int propertyId, DateTime viewingAt)
        {
            var property = _context.Properties.Find(propertyId);

            return new BookViewingViewModel
            {
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                ViewingAtDate = viewingAt,
                ViewingAtTime = viewingAt
            };
        }
    }
}