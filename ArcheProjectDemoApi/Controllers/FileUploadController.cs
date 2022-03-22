using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ArcheProjectDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost(nameof(UploadFile))]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                var path = "D:/FileFolder/";
                if(file.Length>0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullpath = Path.Combine(path, fileName);
                    using (var stream=new FileStream(fullpath,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            //if(files.count==0)
            //{
            //    return BadRequest();
            //}
            //string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadFiles");

        }
    }
}
