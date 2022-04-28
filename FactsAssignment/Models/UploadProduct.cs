using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FactsAssignment.Models
{
    public class UploadProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Brand { get; set; }
        //public string Image { get; set; }
        [Required]
        [Display(Name = "Upload File")]
        public IFormFile postedFile { get; set; }

        public string Color { get; set; }
    }

    //public class AllowedExtensionsAttribute : ValidationAttribute
    //{
    //    private readonly string[] _extensions;

    //    public AllowedExtensionsAttribute(string[] extensions)
    //    {
    //        _extensions = extensions;
    //    }


    //    public override bool IsValid(object value)
    //    {
    //        if (value is null)
    //            return true;

    //        var file = value as IFormFile;
    //        var extension = Path.GetExtension(file.FileName);

    //        if (!_extensions.Contains(extension.ToLower()))
    //            return false;

    //        return true;
    //    }
    //}

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            var extension = Path.GetExtension(file.FileName);
            if (file != null)
            {
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Your image's filetype is not valid.";
        }
    }
}
