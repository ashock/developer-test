using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class MyOffersViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MyOffersViewModel Build(string buyerUserId)
        {
            var result = new MyOffersViewModel();

            var offers =
                (from o in _context.Offers.AsNoTracking<Offer>()
                 where o.BuyerUserId == buyerUserId
                 group o by o.PropertyId into propertyGroup
                 orderby propertyGroup.Key
                 select propertyGroup).ToDictionary<int, Offer>();
            
            foreach (var propertyGroup in offers)
            {
                var property = (from p in _context.Properties
                                where p.Id == propertyGroup.Key
                                select p).FirstOrDefault();

                if (property != null)
                {
                    var offersOnProperty = new OffersOnPropertyViewModel
                    {
                        PropertyId = property.Id,
                        PropertyType = property.PropertyType,
                        StreetName = property.StreetName,
                        NumberOfBedrooms = property.NumberOfBedrooms,
                        HasOffers = true,
                        Offers = propertyGroup.Value.Select(x => new OfferViewModel
                        {
                            Id = x.Id,
                            Amount = x.Amount,
                            BuyerUserId = x.BuyerUserId,
                            CreatedAt = x.CreatedAt,
                            IsPending = x.Status == OfferStatus.Pending,
                            Status = x.Status.ToString()
                        })
                    };
                    result.Properties.Add(offersOnProperty);
                }
            }

            return result;
        }
    }
}