using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AspNetMvc.Controllers.Api
{
    [RoutePrefix("api/File")]
    public class FileController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post()
        {
            var request = HttpContext.Current.Request;

            var directory = HttpContext.Current.Server.MapPath("~/Files");
            var form = request.Form;
            var fileNames = new List<string>();
            var files = request.Files;
            var json = form.Get("json");
            var value = JsonConvert.DeserializeObject<dynamic>(json);

            Directory.CreateDirectory(directory);

            for (var i = 0; i < files.Count; i++)
            {
                var f = files[0];
                var path = Path.Combine(directory, f.FileName);
                f.SaveAs(path);
                fileNames.Add(f.FileName);
            }

            return Ok(new
            {
                Json = value,
                Files = fileNames,
            });
        }
    }
}
