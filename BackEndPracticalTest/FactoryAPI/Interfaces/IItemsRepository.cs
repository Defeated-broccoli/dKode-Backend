using FactoryAPI.Models;

namespace FactoryAPI.Interfaces
{
    public interface IItemsRepository
    {
        Task<Item> GetItemById(int id);
        Task<List<Item>> GetAllItems();
        Task<bool> Add(Item item);
        Task<bool> Update(Item item);
        Task<bool> Delete(int id);
        Task<bool> Save();
    }
}
