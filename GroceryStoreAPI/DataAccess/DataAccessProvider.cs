using GroceryStoreAPI.Model;
using System.Threading;

namespace GroceryStoreAPI.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        public void AddConsumerRecord(Consumer consumer)
        {
            _context.consumers.Add(consumer);
            _context.SaveChanges();
        }

        public void UpdateConsumerRecord(Consumer consumer)
        {
            _context.consumers.Update(consumer);
            _context.SaveChanges();
        }

        public void DeleteConsumerRecord(int id)
        {
            var entity = _context.consumers.FirstOrDefault(t => t.id == id);
            _context.consumers.Remove(entity);
            _context.SaveChanges();
        }

        public Consumer GetConsumerSingleRecord(int id)
        {
            return _context.consumers.FirstOrDefault(t => t.id == id);
        }

        public List<Consumer> GetConsumerSearch(string task_name, string tag, DateTime due_date, string status)
        {
            return _context.consumers.Where(p => p.task_record.task_name == task_name
                                                        && p.task_record.tags.tagName == tag
                                                        && p.task_record.due_date == due_date
                                                        && p.task_record.status == status).ToList();
        }

        public List<Consumer> GetConsumerRecords()
        {
            return _context.consumers.ToList();
        }
    }
}
