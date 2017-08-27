using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoDigest.Importers
{
    using CryptoDigest.Models;
    using CsvHelper;
    using CsvHelper.Configuration;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;

    public abstract class WebCsvImporter<T, TMapping> : IImporter<T> where T : class, ICurrencyHistoryItem where TMapping : CsvClassMap<T>
    {
        private readonly string cachedFileName;

        protected WebCsvImporter()
        {
            Directory.CreateDirectory("Temp");
            this.cachedFileName = $"Temp\\{DateTime.Now:yyyyMMdd}_{this.GetType().Name}.csv";
        }

        public abstract string FileLocation { get; set; }

        public IEnumerable<T> Import()
        {
            string content;
            if (!File.Exists(this.cachedFileName))
            {
                content = this.DownloadFileContent().Result;
                File.WriteAllText(this.cachedFileName, content);
            }
            else
            {
                content = File.ReadAllText(this.cachedFileName);
            }

            using (TextReader reader = new StringReader(content))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap(typeof(TMapping));
                return csv.GetRecords<T>().ToList();
            }
        }

        private async Task<string> DownloadFileContent()
        {
            var webRequest = WebRequest.Create(this.FileLocation);

            using (var response = await webRequest.GetResponseAsync())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                return reader.ReadToEnd();
            }
        }
    }
}