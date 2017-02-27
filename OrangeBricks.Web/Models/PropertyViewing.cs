using System;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Models
{
    public class PropertyViewing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public string BuyerUserId { get; set; }
        [Required]
        public DateTime ViewingAt { get; set; }
        public ViewingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}