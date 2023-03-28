using Newtonsoft.Json;
using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    internal class IOFont
    {
        private readonly string FilePath;
        public IOFont()
        {
            FilePath = $"{Environment.CurrentDirectory}\\Settings.json";

        }
        public FontSettings LoadSettings()
        {
            var fileExists = File.Exists(FilePath);
            if (!fileExists)
            {
                File.CreateText(FilePath).Dispose();
                return new FontSettings();

            }
            using (var reader = File.OpenText(FilePath))
            {
                string fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<FontSettings>(fileText);
            }
        }
        public void SaveSettings(object FontSettings)
        {
            using (StreamWriter writer = File.CreateText(FilePath))
            {
                string output = JsonConvert.SerializeObject(FontSettings);
                writer.Write(output);
            }
        }
    }
}
