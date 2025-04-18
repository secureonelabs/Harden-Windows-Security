// MIT License
//
// Copyright (c) 2023-Present - Violet Hansen - (aka HotCakeX on GitHub) - Email Address: spynetgirl@outlook.com
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// See here for more information: https://github.com/HotCakeX/Harden-Windows-Security/blob/main/LICENSE
//

using System;
using System.Globalization;
using System.IO;

namespace HardenWindowsSecurity;

internal static class ProcessMitigationsParser
{
	// a class to store the structure of the new CSV data
	internal sealed class ProcessMitigationsRecords
	{
		internal string? ProgramName { get; set; }    // Column for program name
		internal string? Mitigation { get; set; }     // Column for mitigation
		internal string? Action { get; set; }         // Column for action
		internal bool RemovalAllowed { get; set; } // Column for removal allowed
		internal string? Comment { get; set; }        // Column for comments
	}

	// a method to parse the CSV file and save the records to RegistryCSVItems
	internal static void ReadCsv()
	{

		// Initializing the path variable for the CSV file
		string path;

		// Define the path to the CSV file
		path = Path.Combine(GlobalVars.path, "Resources", "ProcessMitigations.csv");

		// Open the file and read the contents
		using StreamReader reader = new(path);

		// Read the header line
		string? header = reader.ReadLine();

		// Return if the header is null
		if (header is null) return;

		// Read the rest of the file line by line
		while (!reader.EndOfStream)
		{
			string? line = reader.ReadLine();

			// Skip if the line is null
			if (line is null) continue;

			// Split the line by commas to get the values, that's the CSV's delimiter
			string[] values = line.Split(',');

			// Check if the number of values is 5
			if (values.Length == 5)
			{
				// Add a new ProcessMitigationsRecords to the list
				GlobalVars.ProcessMitigations.Add(new ProcessMitigationsRecords
				{
					ProgramName = values[0],
					Mitigation = values[1],
					Action = values[2],
					RemovalAllowed = Convert.ToBoolean(values[3], CultureInfo.InvariantCulture),
					Comment = values[4]
				});
			}
		}
	}
}
