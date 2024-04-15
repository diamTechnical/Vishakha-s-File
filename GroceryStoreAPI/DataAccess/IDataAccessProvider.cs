using GroceryStoreAPI.Model;

namespace GroceryStoreAPI.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddConsumerRecord(Consumer Consumer);
        void UpdateConsumerRecord(Consumer Consumer);
        void DeleteConsumerRecord(int id);
        Consumer GetConsumerSingleRecord(int id);
        List<Consumer> GetConsumerSearch(string task_name,string tag, DateTime due_date, string status);
        List<Consumer> GetConsumerRecords();
    }
}
