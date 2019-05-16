using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Web.Http.Tracing;
using DeltaXSetup.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net;
using DeltaXSetup.ErrorHelper;

namespace DeltaXSetup.ActionFilters
{
    /// <summary>  
    /// Action filter to handle for Global application errors.  
    /// </summary>  
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());

            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Error(context.Request, "Controller : " + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + 
                Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName, context.Exception);

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { 
                    Content = new StringContent(context.Exception.Message), ReasonPhrase = "ValidationException", };
                throw new HttpResponseException(resp);

            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else if (exceptionType == typeof(BaseException))
            {
                var webapiException = context.Exception as BaseException;
                if (webapiException != null)
                    throw new HttpResponseException(context.Request.CreateResponse(webapiException.HttpStatus, 
                        new { StatusCode = webapiException.ErrorCode, StatusMessage = webapiException.ErrorDescription, 
                            ReasonPhrase = webapiException.ReasonPhrase }));
            }
            else if (exceptionType == typeof(BusinessException))
            {
                var businessException = context.Exception as BusinessException;
                if (businessException != null)
                    throw new HttpResponseException(context.Request.CreateResponse(businessException.HttpStatus, 
                        new { StatusCode = businessException.ErrorCode, StatusMessage = businessException.ErrorDescription, 
                            ReasonPhrase = businessException.ReasonPhrase }));
            }
            else if (exceptionType == typeof(DataException))
            {
                var dataException = context.Exception as DataException;
                if (dataException != null)
                    throw new HttpResponseException(context.Request.CreateResponse(dataException.HttpStatus, 
                        new { StatusCode = dataException.ErrorCode, StatusMessage = dataException.ErrorDescription, 
                            ReasonPhrase = dataException.ReasonPhrase }));
            }
            else
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }
    }  
}