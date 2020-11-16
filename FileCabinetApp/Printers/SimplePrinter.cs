﻿using System;
using System.Collections.Generic;
using FileCabinetApp.Service;

namespace FileCabinetApp.Printers
{
    public class SimplePrinter
    {
        public void Print(IEnumerable<FileCabinetRecord> records)
        {
            foreach (var record in records)
            {
                Console.WriteLine($"#{record.Id}: {record.FirstName} {record.LastName}; Date of birth: {record.DateOfBirth.ToLongDateString()}" +
                    $" Experience: {record.Experience} years, Balance: {record.Balance}, EnglishLevel: {record.EnglishLevel}.");
            }
        }
    }
}