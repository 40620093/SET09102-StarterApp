using StarterApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarterApp.Database.Data.Repositories{

public interface IRentalRequestRepository
{
    Task AddRequestAsync(RentalRequest request); //adds a new rental request to the database

    Task<List<RentalRequest>> GetRequestsForItemAsync(int itemId); //gets all requests for a specific item (used by the item owner)

    Task<List<RentalRequest>> GetRequestsForUserAsync(string userId); //gets all requests made by a specific user

    Task UpdateRequestAsync(RentalRequest request); //updates an existing request e.g. approve/reject
}
}
    
