using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SimpltyTravelBLL;
using Models;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace SimplyTravelAPI.Controllers
{
    [RoutePrefix("api/upload")]

    public class UploadController : ApiController
    {
       

        [AcceptVerbs("GET", "POST")]
        [Route("uploadfile")]
        [HttpPost]
        public async Task<HttpResponseMessage> uploadfile()
        {

                // Check if the request contains multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = HttpContext.Current.Server.MapPath("~/App_Data");
                var provider = new MultipartFormDataStreamProvider(root);
            string returnValue="";
                try
                {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                    // This illustrates how to get the file names.
                    foreach (MultipartFileData file in provider.FileData)
                {
                    
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                        Trace.WriteLine("Server file path: " + file.LocalFileName);
                    returnValue = file.LocalFileName;
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, returnValue);
                }
                catch (System.Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                }
            }




        }
}

