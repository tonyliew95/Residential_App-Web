using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Nop.Core.Infrastructure;
using Nop.Plugin.Api.DTOs.Errors;
using Nop.Plugin.Api.Models;

namespace Nop.Plugin.Api.Attributes
{
    using System.IO;
    using System.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Nop.Plugin.Api.JSON.ActionResults;
    using Nop.Plugin.Api.JSON.Serializers;

    public class GetRequestsErrorInterceptorActionFilter : ActionFilterAttribute
    {
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public GetRequestsErrorInterceptorActionFilter()
        {
            _jsonFieldsSerializer = EngineContext.Current.Resolve<IJsonFieldsSerializer>();
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null && !actionExecutedContext.ExceptionHandled)
            {
                //WilliamHo 20181015 \/
                //var error = new KeyValuePair<string, List<string>>("internal_server_error", new List<string>() { "please, contact the store owner" });
                var error = new KeyValuePair<string, List<string>>("internal_server_error", new List<string>() { "please, contact our developer" });
                //WilliamHo 20181015 /\
                // add logging here to capture the error.
                string exceptionErrorMessage = actionExecutedContext.Exception.Message;
                actionExecutedContext.Exception = null;
                actionExecutedContext.ExceptionHandled = true;
                SetError(actionExecutedContext, error, exceptionErrorMessage);
            }
            else if (actionExecutedContext.HttpContext.Response != null && 
                (HttpStatusCode)actionExecutedContext.HttpContext.Response.StatusCode != HttpStatusCode.OK)
            {
                string responseBody = string.Empty;

                using (var streamReader = new StreamReader(actionExecutedContext.HttpContext.Response.Body))
                {
                    responseBody = streamReader.ReadToEnd();
                }

                // reset reader possition.
                actionExecutedContext.HttpContext.Response.Body.Position = 0;

                DefaultWeApiErrorsModel defaultWebApiErrorsModel = JsonConvert.DeserializeObject<DefaultWeApiErrorsModel>(responseBody);

                // If both are null this means that it is not the default web api error format, 
                // which means that it the error is formatted by our standard and we don't need to do anything.
                if (!string.IsNullOrEmpty(defaultWebApiErrorsModel.Message) &&
                    !string.IsNullOrEmpty(defaultWebApiErrorsModel.MessageDetail))
                {

                    var error = new KeyValuePair<string, List<string>>("lookup_error", new List<string>() { "not found" });

                    SetError(actionExecutedContext, error);
                }
            }

            base.OnActionExecuted(actionExecutedContext);
        }

        private void SetError(ActionExecutedContext actionExecutedContext, KeyValuePair<string, List<string>> error, string detailedError = null)
        {
            //var bindingError = new Dictionary<string, List<string>>();
            //bindingError.Add(error.Key, error.Value);
            //
            var errorsRootObject = new ErrorsResultObject();
            errorsRootObject.meta.code = 4999;
            errorsRootObject.meta.message = "Unhandled exception error. " + detailedError;
            
            string errorJson = _jsonFieldsSerializer.Serialize(errorsRootObject, null);

            actionExecutedContext.Result = new ErrorActionResult(errorJson, HttpStatusCode.OK);
        }
    }
}