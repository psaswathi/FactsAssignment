using FactsAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
//using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
//using System.IO;
//using System.Linq;
using System.Text.Json;
//using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace FactsAssignment.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DisplayProducts()
        {
            List<Product> productList = new List<Product>();
            using (StreamReader r = new StreamReader("jsonInput.json"))
            {
                string json = r.ReadToEnd();
                productList = JsonConvert.DeserializeObject<List<Product>>(json);
            }
            return View(productList);
        }

        public IActionResult UploadProduct()
        {
            return View();
        }
        public IActionResult AddToCart()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadProduct(UploadProduct uploadProduct)
        {
            try
            {
                List<Product> productList = new List<Product>();
                using (StreamReader r = new StreamReader("jsonInput.json"))
                {
                    string json = r.ReadToEnd();
                    productList = JsonConvert.DeserializeObject<List<Product>>(json);
                }
                string imageColor = FindImageColor(uploadProduct.postedFile);
                productList.Add(new Product
                {
                    Name = uploadProduct.Name,
                    Price = uploadProduct.Price,
                    Currency = uploadProduct.Currency,
                    Brand = uploadProduct.Brand,
                    Image = uploadProduct.postedFile.FileName,
                    Color = imageColor
                });

                using (StreamWriter sw = new StreamWriter("jsonInput.json"))
                {
                    string jsonProduct = JsonConvert.SerializeObject(productList);
                    sw.WriteLine(jsonProduct);
                }
                return RedirectToAction(nameof(DisplayProducts));
            }
            catch
            {
                return View();
            }
        }

        public static string FindImageColor(IFormFile file)
        {
            Color imageColor = new Color();
            try
            {
                string imageBase64 = string.Empty;
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        imageBase64 = Convert.ToBase64String(fileBytes);
                    }
                }
                Bitmap signatureBM = null;
                byte[] byteBuffer = Convert.FromBase64String(imageBase64);
                using (MemoryStream memoryStream = new MemoryStream(byteBuffer))
                {
                    memoryStream.Position = 0;
                    signatureBM = (Bitmap)Bitmap.FromStream(memoryStream);

                    memoryStream.Close();
                    byteBuffer = null;
                };

                imageColor = signatureBM.GetPixel(signatureBM.Width - 1, signatureBM.Height - 1);

            }
            catch (Exception)
            {
                
            }
            return "#" + imageColor.R.ToString("X2") + imageColor.G.ToString("X2") + imageColor.B.ToString("X2"); 
        }
    }
}
