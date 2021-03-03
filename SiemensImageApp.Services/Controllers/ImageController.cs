using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiemensImageApp.DataAccessLayer;
using System.IO;

namespace SiemensImageApp.Services.Controllers
{
    //[Route("api/[controller]")]
    //[Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        public ImageController()
        {

        }

        [HttpGet]
        [Route("api/[controller]")]
        public JsonResult GET()
        {
            SiemensImageAppRepository dal = new SiemensImageAppRepository();
            //bool connected = dal.TestConnection();
            string imageNames = dal.GetAllImages();
            //return Json("testing connection:"+ connected);
            return Json(imageNames);

        }

        [HttpPost]
        [Route("api/[controller]")]
        public JsonResult POST(IFormFile file, string newName)
        {
            bool status = false;
            DateTime postedTime = DateTime.Now;
            string name = Path.GetFileName(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            //long size = file.Length;
            Stream stream = file.OpenReadStream();
            BinaryReader binaryReader = new BinaryReader(stream);
            byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".gif" || extension.ToLower() == ".png" || extension.ToLower() == ".bmp")
            {
                SiemensImageAppRepository dal = new SiemensImageAppRepository();
                status = dal.InsertImage(newName, postedTime, bytes);
            }
            else
            {

            }
            if (status)
            {
                return Json(file.FileName + " added successfull");
            }
            else
            {
                return Json("Errorin uploading file " + file.FileName);
            }

        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public JsonResult ASC()
        {
            SiemensImageAppRepository dal = new SiemensImageAppRepository();
            string imageNames = dal.GetAllImagesASC();
            return Json(imageNames);
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public JsonResult DES()
        {
            SiemensImageAppRepository dal = new SiemensImageAppRepository();
            string imageNames = dal.GetAllImagesDES();
            return Json(imageNames);
        }

    }
}
