using System;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommand
    {
        public int PropertyId { get; set; }
        public DateTime ViewingAtDate { get; set; }
        public DateTime ViewingAtTime { get; set; }
    }
}