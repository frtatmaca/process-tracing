using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.Office.Core;
//using Microsoft.Office.Interop.PowerPoint;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity;
using System.IO;
//using Microsoft.Office.Interop.Word;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SakaryaBel.Web.Models
{
    public class ContentHelper
    {
        public static bool ValidUrl(string url)
        {
            //string regular = @"^(ht|f|sf)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";
            //string myString = m.Url.Trim();
            //if (!Regex.IsMatch(myString, regular))
            //{
            //    ModelState.AddModelError("Url", "Url alanı geçersiz.");
            //}

            if (url != null && (url.StartsWith("http") || url.StartsWith("https")))
            {
                return true;

            }
            return false;
        }

        public static LinkModel LinkEmbed(string url)
        {
            if (url.IndexOf("youtube") != -1)
            {
                string urlPattern = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";
                string urlReplace = "http://www.youtube.com/embed/$1";
                var rgx = new Regex(urlPattern);
                var urlResult = rgx.Replace(url, urlReplace);
                //return urlResult;
                return new LinkModel() { url = urlResult, width = 855, height = 510, Icon = "fa-youtube-play" };
            }
            else if (url.IndexOf("vimeo") != -1)
            {
                var tu = url.Length;
                var abc = url.LastIndexOf("/") + 1;

                var result = url.Substring(abc, (tu - abc));

                var urlResult = "https://player.vimeo.com/video/" + result;
                //return urlResult;
                return new LinkModel() { url = urlResult, width = 960, height = 540, Icon = "fa-vimeo-square" };
            }
            return new LinkModel() { url = url, width = 640, height = 480, Icon = "fa-external-link" };
        }

        public static dynamic LinkPreview(string url)
        {
            if (url.IndexOf("youtube") != -1)
            {
                string urlPattern = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";
                string urlReplace = "http://www.youtube.com/embed/$1";
                var rgx = new Regex(urlPattern);
                var urlResult = rgx.Replace(url, urlReplace);
                return new { urls = urlResult, estatus = "ok" };
            }
            else if (url.IndexOf("vimeo") != -1)
            {
                var tu = url.Length;
                var abc = url.LastIndexOf("/") + 1;
                var result = url.Substring(abc, (tu - abc));
                var urlResult = "https://player.vimeo.com/video/" + result;
                return new { urls = urlResult, estatus = "ok" };
            }
            return new { urls = url, estatus = "error" };
        }

        public void ConvertPowerPoint(int sourceid, string sourcefileurl)
        {
            //ApplicationClass pptApplication = new ApplicationClass();
            //Presentation pptPresentation = pptApplication.Presentations.Open("http://localhost:51529/" + sourcefileurl, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);

            //var slidescount = pptPresentation.Slides.Count;

            //for (int i = 1; i < slidescount + 1; i++)
            //{
            //    //pptPresentation.Slides[i].Export("E:\\LMS2\\Source\\trunk\\Web\\LMS.Content\\Uploads\\53314\\ppt\\" + pptPresentation.Slides[i].Name + ".png", "png", 1280, 720);
            //    pptPresentation.Slides[i].Export("E:\\LMS2\\Source\\trunk\\Web\\LMS.Content\\Uploads\\53314\\" + pptPresentation.Slides[i].Name + ".png", "png", 1280, 720);
            //}
        }

        public static string FileTypeControl(string type)
        {

            List<string> genelTipler = new List<string>();
            genelTipler.Add("application/msword");
            genelTipler.Add("application/vnd.ms-word.document.macroEnabled.12");
            genelTipler.Add("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            genelTipler.Add("application/vnd.ms-excel");
            genelTipler.Add("application/vnd.ms-excel.sheet.binary.macroEnabled.12");
            genelTipler.Add("application/vnd.ms-excel.sheet.macroEnabled.12");
            genelTipler.Add("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            genelTipler.Add("application/vnd.ms-powerpoint");
            genelTipler.Add("application/vnd.ms-powerpoint.presentation.macroEnabled.12");
            genelTipler.Add("application/vnd.openxmlformats-officedocument.presentationml.presentation");
            genelTipler.Add("application/pdf");
            genelTipler.Add("application/x-zip-compressed");
            genelTipler.Add("application/x-compress");
            genelTipler.Add("application/octet-stream");

            if (type == "video/mp4")
            {
                type = "1"; // mp4
            }
            else if (type == "audio/mp3")
            {
                type = "2"; // mp3
            }
            else if (genelTipler.Contains(type))
            {
                if (type == "application/vnd.openxmlformats-officedocument.presentationml.presentation")
                {
                    type = "3.1"; // powerpoint
                }
                else
                {
                    type = "3"; // dosya
                }
            }
            else if (type == "image/jpeg" || type == "image/png")
            {
                type = "4"; // image
            }
            else
            {
                type = "5";
            }
            return type;
        }
        public static int ControlFileType(string type)
        {
            int ctype = 0;
            switch (type)
            {
                case "video/mp4": ctype = Convert.ToInt32(ContentTypes.video); break;
                case "audio/mp3": ctype = Convert.ToInt32(ContentTypes.Audio); break;
                case "image/jpeg": ctype = Convert.ToInt32(ContentTypes.Image); break;
                case "image/png": ctype = Convert.ToInt32(ContentTypes.Image); break;
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": ctype = Convert.ToInt32(ContentTypes.Word); break;
                case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": ctype = Convert.ToInt32(ContentTypes.Excell); break;
                case "application/vnd.openxmlformats-officedocument.presentationml.presentation": ctype = Convert.ToInt32(ContentTypes.PowerPoint); break;
                case "application/vnd.ms-powerpoint": ctype = Convert.ToInt32(ContentTypes.PowerPoint); break;
                case "application/pdf": ctype = Convert.ToInt32(ContentTypes.Pdf); break;
                case "application/octet-stream": ctype = Convert.ToInt32(ContentTypes.Zip); break;
                case "application/x-zip-compressed": ctype = Convert.ToInt32(ContentTypes.Zip); break;
                //default: ctype = Convert.ToInt32(ContentTypes.Zip); break;
            }
            return ctype;
        }

    }


}