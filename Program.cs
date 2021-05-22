using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
                    // serialize is json string
                    var serialize = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true });
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
