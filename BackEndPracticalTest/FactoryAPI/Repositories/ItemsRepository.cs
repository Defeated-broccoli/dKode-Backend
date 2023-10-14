using FactoryAPI.Helpers;
using FactoryAPI.Interfaces;
using FactoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryAPI.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly AppDbContext _context;

        public ItemsRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<bool> Add(Item item)
        {
            await _context.Items.AddAsync(item);
            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.ID == id);
            _context.Items.Remove(item);
            return await Save();
        }

        public async Task<List<Item>> GetAllItems()
        {
            var items = await _context.Items.ToListAsync();

            return items;
        }

        public async Task<Item> GetItemById(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.ID == id);

            return item;
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Item item)
        {
            _context.Items.Update(item);
            return await Save();
        }
    }
}
