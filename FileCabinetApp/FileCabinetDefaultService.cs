﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetDefaultService : FileCabinetService
    {
        protected override void ValidateParameters(RecordData parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.firstName))
            {
                if (parameters.firstName is null)
                {
                    throw new ArgumentNullException($"{nameof(parameters.firstName)} cannot be null.");
                }

                if (parameters.firstName.Length < 2 || parameters.firstName.Length > 60)
                {
                    throw new ArgumentException($"{nameof(parameters.firstName.Length)} must be in range 2 to 60.");
                }

                throw new ArgumentException($"{nameof(parameters.firstName)} cannot be empty or whiteSpace.");
            }

            if (string.IsNullOrWhiteSpace(parameters.lastName))
            {
                if (parameters.lastName is null)
                {
                    throw new ArgumentNullException($"{nameof(parameters.lastName)} cannot be null.");
                }

                if (parameters.lastName.Length < 2 || parameters.lastName.Length > 60)
                {
                    throw new ArgumentException($"{nameof(parameters.lastName.Length)} must be in range 2 to 60.");
                }

                throw new ArgumentException($"{nameof(parameters.lastName)} cannot be empty or whiteSpace.");
            }

            if (parameters.dateOfBirth < new DateTime(1950, 1, 1) || parameters.dateOfBirth > DateTime.Now)
            {
                throw new ArgumentException($"{nameof(parameters.dateOfBirth)} is incorrect.");
            }
        }
    }
}
