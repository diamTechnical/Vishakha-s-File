using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Model.Mail
{
    public class MailSendModel:IDisposable
    {
        public List<string> to { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool isTemplate  { get; set; }
        public string templateName { get; set; }
        public string templateSource { get; set; }
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
        ~MailSendModel()
        {
            Dispose(false);
        }
    }
}
