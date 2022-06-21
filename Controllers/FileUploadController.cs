namespace SignalRAppAngular.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="FileUploadController" />.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileUploadController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public FileUploadController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The UploadFile.
        /// </summary>
        /// <param name="filesList">The filesList<see cref="List{IFormFile}"/>.</param>
        /// <returns>The <see cref="List{string}"/>.</returns>
        [HttpPost]
        [Route("upload")]
        public List<string> UploadFile(List<IFormFile> filesList)
        {
            List<string> pathArray = new List<string>();

            if (filesList == null || filesList.Count <= 0)
            {
                throw new Exception("FileNotFound");
            }

            foreach (var file in filesList)
            {
                if (file.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString("N");
                    var fileName = guid + "_" + file.FileName;
                    if (file.FileName.Length > 100)
                    {
                        fileName = guid + "_" + file.FileName.Substring(file.FileName.Length - 100);
                    }
                    var rootPath = Configuration.GetValue<string>("ROOT_PATH");
                    var basePath = Configuration.GetValue<string>("FILE_PATH");
                    var fullPath = Path.Combine(rootPath, basePath);
                    var pathBuilt = Path.GetFullPath(fullPath);
                    if (!Directory.Exists(pathBuilt))
                    {
                        Directory.CreateDirectory(pathBuilt);
                    }

                    var path = Path.Combine(pathBuilt, fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    string filePath = Path.Combine(rootPath, basePath, fileName);
                    pathArray.Add(filePath);
                }
            }
            return pathArray;
        }
    }
}
