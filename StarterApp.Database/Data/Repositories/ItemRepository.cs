using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Data;
using StarterApp.Database.Models;
using StarterApp.Models;

namespace StarterApp.Database.Data.Repositories;

//this handles database actions for Item records.
public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    //Constructor created this receives the database context by dependency injection
    public ItemRepository (AppDbContext context)
    {
        _context = context;
    }

    //Get all items 
    public async Task<List<Item>> GetAllItemsAsync()
    {
        return await _context.Items.ToListAsync();
    }

    //Get a individual item by ID
    public async Task<Item?> GetItemByIdAsync(int id)
    {
        return await _context.Items.FindAsync(id);
    }

    //Add a new Item
    public async Task AddItemAsync (Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
    }

    //update an existing item
    public async Task UpdateItemAsync (Item item)
    {
        //find the exisiting item in the database
        var exisitingItem = await _context.Items.FindAsync(item.Id);

        //if item cannot be found, stop
        if (exisitingItem == null)
        return;

        //update the editable fields
        exisitingItem.Title = item.Title;
        exisitingItem.Description = item.Description;
        exisitingItem.Category = item.Category;
        exisitingItem.Location = item.Location;
        exisitingItem.DailyRate = item.DailyRate;

        //save updated item
        await _context.SaveChangesAsync();
    }

    //Delete item by ID
    public async Task DeleteItemAsync (int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return;
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }
}