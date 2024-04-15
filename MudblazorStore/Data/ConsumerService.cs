using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.DataAccess;
using GroceryStoreAPI.Model;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.Icons;

namespace MudblazorStore.Data
{
    public class ConsumerService
    {
        private readonly ILogger<ConsumerService> _logger;
        private readonly IDataAccessProvider _dataAccessProvider;
        public ConsumerService(ILogger<ConsumerService> logger, IDataAccessProvider dataAccessProvider)
        {
            _logger = logger;
            _dataAccessProvider = dataAccessProvider;
        }
        public IEnumerable<Consumer> GetConsumer()
        {

            return _dataAccessProvider.GetConsumerRecords();
        }
        public void DeleteConsumer(int id)
        {
            _dataAccessProvider.DeleteConsumerRecord(id);
          
        }

        public Consumer GetConsumerById(int id)
        {
            var consumer = _dataAccessProvider.GetConsumerSingleRecord(id);
           
            return consumer;
        }

        public void SaveConsumer(Consumer consumer)
        {
            _dataAccessProvider.AddConsumerRecord(consumer);
           
        }
    }
}
