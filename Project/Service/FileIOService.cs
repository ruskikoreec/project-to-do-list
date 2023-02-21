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
        public FileIOService(string filePath)
        {
            FilePath = filePath;
        }
        public BindingList<ToDoModel> LoadData()
        {
            return null;
        }
        public void SaveData(BindingList<ToDoModel> todoDataList)
        {
            using(StreamWriter writer = File.CreateText(FilePath))
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                writer.Write(output);
            }
        }

    }
}
