using Newtonsoft.Json;
using PSI.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PSI.WebAPI.Controllers
{
    [EnableCorsAttribute("http://localhost:22205", "*", "*")]
    public class FilesController : ApiController
    {
        [HttpPost] 
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData.First());
            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            var returnData = "UpLoad" + originalFileName + "Successful";

            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData, uploadedFileInfo });
        }

        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Temporary/Uploads";
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
}