using StarterApp.Models;
using StarterApp.Database.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq;

namespace StarterApp.Database.Data.Repositories
{
    //this class implements the IRentalRequestRepository interface
    public class RentalRequestRepository : IRentalRequestRepository
    {
        private readonly AppDbContext _context; //Db context

        //constructor - Dependency injection
        public RentalRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        //add a new rental request to the database
        public async Task AddRequestAsync(RentalRequest request)
        {
        

            _context.RentalRequests.Add(request);
            await _context.SaveChangesAsync();

        }

        //get all requests for a specific item (used by item owner)
        public async Task<List<RentalRequest>> GetRequestsForItemAsync(int itemId)
        {
            return await _context.RentalRequests
            .Where(r => r.ItemId == itemId)
            .ToListAsync();
        }

        //get all requests made by a specific user
        public async Task<List<RentalRequest>> GetRequestsForUserAsync(string userId)
        {
            return await _context.RentalRequests
            .Where(r=> r.RequesterUserId == userId)
            .ToListAsync();
        }

        //update an existing request e.g. approve/reject
        public async Task UpdateRequestAsync(RentalRequest request)
        {
            _context.RentalRequests.Update(request);
            await _context.SaveChangesAsync();
        }

    }
}
