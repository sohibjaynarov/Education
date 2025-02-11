﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSizeKB;
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSizeInKB)
        {
            _maxFileSize = maxFileSizeInKB;
            _maxFileSizeKB = _maxFileSize * 1024;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSizeKB)
                {
                    return new ValidationResult($"The file size exceeds the limit allowed { _maxFileSize} KB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
