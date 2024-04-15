using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Model.Mail
{
    public class MailSendReceivedModel:IDisposable
    {
        public bool isSuccess   { get; set; }
        public string message { get; set; }
        public HttpStatusCode  statusCode { get; set; }
        public HttpContent responseBody { get; set; }
        public  HttpResponseHeaders responseHeaders { get; set; }
        public string email { get; set; }
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
        ~MailSendReceivedModel()
        {
            Dispose(false);
        }
    }
}
