using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Model.Mail
{
    public class GetSendGridIntegrationModel:IDisposable
    {
        public int id { get; set; }
        public string thridPartyName { get; set; }
        public string endpointUrl { get; set; }
        public string publicKey { get; set; }
        public string version { get; set; }
        public string senderEmail { get; set; }
        public string senderName { get; set; }
        public int isDefault { get; set; }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
        ~GetSendGridIntegrationModel()
        {
            Dispose(false);
        }
    }
}
