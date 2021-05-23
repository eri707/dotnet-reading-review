using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace dotnet_file_reading_review
{
    class Program
    {
        static void Main(string[] args)
        {
            //error handling
            var fileName = args[0];
            if (!fileName.Contains("."))
            {
                Console.WriteLine("Please specify file extension.");
                return;
            }
            if (!File.Exists($"./{fileName}"))
            {
                Console.WriteLine("This file doesn't exist.");
                return;
            }
            try
            {
                using (StreamReader r = new StreamReader($"./{fileName}"))
                {
                    var jsonString = r.ReadToEnd();
                    // cars is a list of objects
                    var cars = JsonSerializer.Deserialize<List<Car>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    //Dictionary stores key-value pairs(empty object)
                    var carHash = new Dictionary<string, int>();
                    foreach(var i in cars)
                    {
                        if (carHash.ContainsKey(i.Make))
                        {
                            carHash[i.Make]++;
                        }
                        else 
                        {
                            carHash[i.Make] = 1;
                        }
                    }// serialize is json string(to print it out)
                     var serialize = JsonSerializer.Serialize(carHash, new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(serialize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
