using System;
using System.Collections.Generic;
using System.IO;

#nullable enable

namespace HardenWindowsSecurity
{
    public static class HardeningRegistryKeys
    {
        // Define a public class to store the structure of the new CSV data
        public sealed class CsvRecord
        {
            public required string Category { get; set; }       // Column for category
            public required string Path { get; set; }           // Column for registry path
            public required string Key { get; set; }            // Column for registry key
            public required string Value { get; set; }          // Column for the expected value
            public required string Type { get; set; }           // Column for the type of the registry value
            public required string Action { get; set; }         // Column for the action to be taken
            public string? Comment { get; set; }                // Column for comments
        }

        // Define a public method to parse the CSV file and save the records to RegistryCSVItems
        public static void ReadCsv()
        {
            // Ensure GlobalVars.path is not null
            string basePath = GlobalVars.path ?? throw new ArgumentNullException(nameof(GlobalVars.path), "GlobalVars.path cannot be null.");

            // Ensure RegistryCSVItems is initialized
            List<CsvRecord> registryCSVItems = GlobalVars.RegistryCSVItems ?? throw new InvalidOperationException("RegistryCSVItems is not initialized.");

            // Define the path to the CSV file
            string path = Path.Combine(basePath, "Resources", "Registry.csv");

            // Open the file and read the contents
            using StreamReader reader = new(path);

            // Read the header line
            string? header = reader.ReadLine();

            // Return if the header is null
            if (header is null)
            {
                return;
            };

            // Read the rest of the file line by line
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();

                // Skip if the line is null
                if (line is null)
                {
                    continue;
                }

                // Split the line by commas to get the values, that's the CSV's delimiter
                string[] values = line.Split(',');

                // Check if the number of values is 7
                if (values.Length == 7)
                {
                    // Add a new CsvRecord to the list
                    registryCSVItems.Add(new CsvRecord
                    {
                        Category = values[0],
                        Path = values[1],
                        Key = values[2],
                        Value = values[3],
                        Type = values[4],
                        Action = values[5],
                        Comment = values[6]
                    });
                }
            }
        }
    }
}
