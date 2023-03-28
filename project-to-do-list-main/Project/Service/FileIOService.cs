using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace Project.Service
{
    class FileIOService
    {
        private readonly string FilePath;
        private readonly ICripter cripter;
        public FileIOService(string filePath)
        {
            FilePath = filePath;
            cripter = new EnDeCryption();
        }
        public BindingList<ToDoModel> LoadData()
        {
            var fileExists = File.Exists(FilePath);
            if(!fileExists)
            {
                File.CreateText(FilePath).Dispose();
                return new BindingList<ToDoModel>();

            }
            using (var reader = File.OpenText(FilePath))
            {
                string fileText = reader.ReadToEnd();
                fileText = cripter.Decryption(fileText);
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }
        public void SaveData(object todoDataList)
        {
            using (StreamWriter writer = File.CreateText(FilePath))
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                output = cripter.Encryption(output);
                writer.Write(output);
            }
        }

    }
}
