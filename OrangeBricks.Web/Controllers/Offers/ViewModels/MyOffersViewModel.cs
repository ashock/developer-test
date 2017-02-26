using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class MyOffersViewModel
    {
        public MyOffersViewModel()
        {
            Properties = new List<OffersOnPropertyViewModel>();
        }

        public List<OffersOnPropertyViewModel> Properties { get; private set; }
    }
}