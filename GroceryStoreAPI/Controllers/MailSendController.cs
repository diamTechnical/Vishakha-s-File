using GroceryStoreAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendGrid;
using GroceryStoreAPI.Model.Mail;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/mailsend")]
    [ApiController]
    public class MailSendController : Controller
    {
        private readonly ILogger<MailSendController> logger;
        private readonly IDataAccessProvider _dataAccessProvider;
        private const string errMsg = "Send Grid Key Retrieval Failed From Control Lab, Response Payload is Empty.";
        private const string errMsgPayLoad = "Payload Not Contain From Mail List.";

        public MailSendController(ILogger<MailSendController> _logger, IDataAccessProvider dataAccessProvider)
        {
            logger = _logger;
            _dataAccessProvider = dataAccessProvider;
        }
        [HttpPost("sendmail")]
        public async Task<ActionResult<List<MailSendReceivedModel>>> SendMail(MailSendModel sendModel)
        {
            List<MailSendReceivedModel> objRes = new List<MailSendReceivedModel>();
            try
            {
                logger.LogInformation($"SendMail START");
                if (sendModel.to.Any())
                {
                    List<GetSendGridIntegrationModel> objModel = new List<GetSendGridIntegrationModel>();
                    objModel.Add(new GetSendGridIntegrationModel()
                    {
                        endpointUrl = "EndpointUrl",
                        id = 1,
                        isDefault = 1,
                        publicKey = "PublicKey",
                        senderEmail = "SenderEmail",
                        senderName = "SenderName",
                        thridPartyName = "ThirdPartyName",
                        version = "Version"
                    });
                    var responsePayLoad = objModel;
                    if (responsePayLoad != null)
                    {
                        var objKey = responsePayLoad.FirstOrDefault(p => !string.IsNullOrEmpty(p.publicKey));
                        if (objKey != null)
                        {
                            var client = new SendGridClient(objKey.publicKey);
                            foreach (var obj in sendModel.to)
                            {
                                try
                                {
                                    var objTemp = GenerateMessage(objKey.senderEmail, obj, sendModel.subject, sendModel.body, sendModel.body);
                                    var response = await client.SendEmailAsync(objTemp);

                                    MailSendReceivedModel objRes1 = new MailSendReceivedModel()
                                    {
                                        isSuccess = response.IsSuccessStatusCode,
                                        message = "Ok",
                                        statusCode = response.StatusCode,
                                        email = obj,
                                        responseBody = response.Body,
                                        responseHeaders = response.Headers
                                    };
                                    objRes.Add(objRes1);
                                }
                                catch (Exception ex)
                                {
                                    logger.LogError($"Mail Send Fail For {objKey.senderEmail},{obj},{sendModel.subject},{sendModel.body},{sendModel.body}");
                                    logger.LogInformation($"SendMail Error Message {ex.Message}");
                                    logger.LogInformation($"SendMail Error StackTrace {ex.StackTrace}");
                                    continue;
                                }
                            }
                            return Ok(objRes);
                        }
                        else
                        {
                            logger.LogError($"SendMail RESPONSEPAYLOAD PUBLICKEY Is IsNullOrEmpty");
                            logger.LogInformation($"SendMail END");
                            MailSendReceivedModel objErr1 = new MailSendReceivedModel() { isSuccess = false, message = errMsg };
                            objRes.Add(objErr1);
                            return BadRequest(objRes);
                        }
                    }
                    else
                    {
                        logger.LogError($"SendMail RESPONSEPAYLOAD Is Null");
                        logger.LogInformation($"SendMail END");
                        MailSendReceivedModel objErr2 = new MailSendReceivedModel() { isSuccess = false, message = errMsg };
                        objRes.Add(objErr2);
                        return BadRequest(objRes);
                    }
                }
                logger.LogError($"SendMail:: Payload Not Contain From Mail List ");
                MailSendReceivedModel objErr3 = new MailSendReceivedModel() { isSuccess = false, message = errMsgPayLoad };
                objRes.Add(objErr3);
                return BadRequest(objRes);

            }
            catch (Exception ex)
            {
                logger.LogError($"Erro In SendMail-->Post-->Error" + ex.Message);
                logger.LogError($"Erro In SendMail-->Post-->StackTrace" + ex.StackTrace);
                throw new ApplicationException(ex.Message, ex);
            }
        }


        private SendGridMessage GenerateMessage(string From, string To, string Subject, string PlainTextContent, string HtmlContent)
        {
            try
            {
                logger.LogInformation("SendGridMessage GenerateMessage Start");
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress(From, From),
                    Subject = Subject,
                    PlainTextContent = PlainTextContent,
                    HtmlContent = HtmlContent
                };
                msg.AddTo(new EmailAddress(To, To));
                logger.LogInformation("SendGridMessage GenerateMessage End");
                logger.LogInformation($"GenerateMessage {Convert.ToString(JsonConvert.SerializeObject(msg))}");
                return msg;
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro In GenerateMessage-->Post-->Error" + ex.Message);
                logger.LogError($"Erro In GenerateMessage-->Post-->StackTrace" + ex.StackTrace);
                throw new ApplicationException(ex.Message, ex);
            }
        }

    }
}
