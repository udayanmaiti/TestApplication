using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TestApplication.Models;
using TestApplication.Services.IService;

namespace TestApplication.Services.Helpers
{
    public class FileHelper : IFileHelper
    {
        private string _fileLocation;
        private readonly object _lockObject = new object();

        public FileHelper(IOptions<FileLocation> options)
        {
            if (options is null)
            {
                throw new System.ArgumentNullException(nameof(options));
            }

            _fileLocation = Path.Combine(Directory.GetCurrentDirectory(), options.Value.FilePath);
        }

        public List<T> ReadFile<T>() where T : class
        {
            lock (_lockObject)
            {
                string data = File.ReadAllText(_fileLocation);
                if (!string.IsNullOrEmpty(data))
                {
                    return JsonConvert.DeserializeObject<List<T>>(data);
                }
                return null;
            }
        }

        public void WriteFile(string data)
        {
            lock (_lockObject)
            {
                File.WriteAllText(_fileLocation, data);
            }
        }
    }
}

