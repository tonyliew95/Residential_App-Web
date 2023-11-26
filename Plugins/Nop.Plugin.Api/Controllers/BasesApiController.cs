using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.DTOs.Errors;
using Nop.Plugin.Api.JSON.ActionResults;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Nop.Plugin.Api.Controllers
{
    using Nop.Plugin.Api.JSON.Serializers;
    using Nop.Plugin.Api.Models;

    public class BasesApiController : Controller
    {
        protected readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public BasesApiController(IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }

        //protected IActionResult Error(HttpStatusCode statusCode = (HttpStatusCode)422, string propertyKey = "", string errorMessage = "")
        //{
            //var errors = new Dictionary<string, List<string>>();

            //if (!string.IsNullOrEmpty(errorMessage) && !string.IsNullOrEmpty(propertyKey))
            //{
                //var errorsList = new List<string>() {errorMessage};
                //errors.Add(propertyKey, errorsList);
            //}
            
            //foreach (var item in ModelState)
            //{
                //var errorMessages = item.Value.Errors.Select(x => x.ErrorMessage);

                //List<string> validErrorMessages = new List<string>();

                //if (errorMessages != null)
                //{
                    //validErrorMessages.AddRange(errorMessages.Where(message => !string.IsNullOrEmpty(message)));
                //}

                //if (validErrorMessages.Count > 0)
                //{
                    //if (errors.ContainsKey(item.Key))
                    //{
                        //errors[item.Key].AddRange(validErrorMessages);
                    //}
                    //else
                    //{
                        //errors.Add(item.Key, validErrorMessages.ToList());
                    //}
                //}
            //}

            //var errorsRootObject = new ErrorsRootObject()
            //{
                //Errors = errors
            //};

            //var errorsJson = _jsonFieldsSerializer.Serialize(errorsRootObject, null);

            //return new ErrorActionResult(errorsJson, statusCode);
        //}

        // erictan - 20180822 
        protected IActionResult ErrorResult(HttpStatusCode statusCode = (HttpStatusCode)422, ErrorsResultObject ErrResultObject = null)
        {
            var errorsJson = _jsonFieldsSerializer.Serialize(ErrResultObject);

            return new ErrorActionResult(errorsJson, statusCode);
        }

    }
}