using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Utils
{
    public class JsonUtils
    {
        private static readonly string BaseFilePath = @"D:\MarsAdvancedTaskPart2\TestData\";


        public static List<T> ReadJsonData<T>(string jsonDataFile)
        {
            string jsonFilePath = Path.Combine(BaseFilePath, jsonDataFile);
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"File not found: {jsonFilePath}");
            }
            string jsonData = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
    }
}
