using System;

namespace StarterApp.Models;

public class RentalRequest // represents a rental request made by a user for an item 
{
    public int Id { get; set; } //unique identifier for each rental request 

    public int ItemId { get; set; } //Id of the item being requested

    public string ItemTitle { get; set; } = string.Empty; //title of the item

    public int OwnerUserId { get; set; } //Id of the user who owns the item

    public string RequesterUserId { get; set; } = string.Empty; //Id of the user requesting to rent the item

    public DateTime RequestedAtUtc { get; set; } = DateTime.UtcNow; //date and time the request was made (stored in utc)

    public string Status { get; set; } = "Pending"; //current status of the request (e.g. Pending, Approved, Rejected)

}