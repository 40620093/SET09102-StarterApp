using System;
using System.ComponentModel.DataAnnotations;

namespace StarterApp.Models
{
    
    ///<summary>
    /// This represents an item that a user can list for rent in the marketplace
    /// </summary>
    public class Item
    {
        public int Id {get; set; } //unique identifier for each item 

        [Required]
        [StringLength(100)]
        public string Title  { get; set; } = string.Empty; //title of the item (e.g. power drill)

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty; //description providing more detail about the item

        [Required]
        [StringLength(50)]
        public string Category {get; set; } = string.Empty; //the catgeory which the item belongs to (e.g. camping, tools)

        [Required]
        [StringLength(50)]
        public string Location {get; set; } = string.Empty; //the location where the item is available

        [Range(0.01, 10000)]
        public decimal DailyRate { get; set; } //cost to rent the item per day

        public bool IsAvailable {get; set; } = true; //shows if item is available to rent 

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow; //stores when item was created 

        public int OwnerUserId { get; set; } //links the item to the user who created it




    }
}