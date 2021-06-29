using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Collections.Specialized;
using SakaryaBel.Web.Models;
using SakaryaBel.Web.Identity;

namespace SakaryaBel.Web.Helpers
{
    public class Common
    {
        readonly BlogContext db = new BlogContext();

        //public static void CreatePdf()
        //{
        //    var directory = System.Web.HttpContext.Current.Server.MapPath("~/Files/");
        //    Guid fileName = Guid.NewGuid();

        //    string filePath = Path.Combine(directory, "Temp");
        //    if (!System.IO.Directory.Exists(filePath))
        //        Directory.CreateDirectory(filePath);

        //    filePath = Path.Combine(directory, "Temp", Convert.ToString(fileName));
        //    if (!System.IO.Directory.Exists(filePath))
        //        Directory.CreateDirectory(filePath);

        //    filePath = Path.Combine(filePath, "frtPdf.pdf");

        //    try
        //    {
        //        Document pdfDoc = new Document(PageSize.A4, 25, 10, 25, 10);
        //        MemoryStream myMemoryStream = new MemoryStream();
        //        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, myMemoryStream);
        //        pdfDoc.Open();

        //        //--------------------------------------------------------------------------------------------
        //        Paragraph Text = new Paragraph("This is test file");
        //        pdfDoc.Add(Text);

        //        //---------------------------------------------------------------------------------------------------------
        //        pdfWriter.CloseStream = false;
        //        pdfDoc.Close();

        //        using (FileStream fs = System.IO.File.Create(filePath))
        //        {
        //            myMemoryStream.WriteTo(fs);
        //        }

        //        myMemoryStream.Close();
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        //public static void CreatePdf2()
        //{
        //    var directory = System.Web.HttpContext.Current.Server.MapPath("~/Files/");
        //    Guid fileName = Guid.NewGuid();

        //    string filePath = Path.Combine(directory, "Temp", Convert.ToString(fileName));
        //    if (!System.IO.Directory.Exists(filePath))
        //        Directory.CreateDirectory(filePath);

        //    filePath = Path.Combine(filePath, "frtPdf.pdf");

        //    Document itextDoc = new Document();
        //    MemoryStream myMemoryStream = new MemoryStream();
        //    PdfWriter pdfDoc = PdfWriter.GetInstance(itextDoc, myMemoryStream);

        //    System.Net.WebClient webClient = new System.Net.WebClient();
        //    //passing url of local web page to read its html content
        //    Stream responseData = webClient.OpenRead("http://www.haber7.com/");
        //    //converting stream into stream reader object
        //    StreamReader inputstream = new StreamReader(responseData);
        //    //If you want to read text from other source like plain text file or user input, ignore all above lines
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter writer = new HtmlTextWriter(sw);
        //    writer.Write(inputstream.ReadToEnd());
        //    //comment above line and uncomment below line if you wish to convert text file to pdf
        //    //writer.Write(File.ReadAllText(@"E:\MyFolder\filename.txt"));
        //    StringReader sr = new StringReader(sw.ToString());

        //    //Parse into IElement
        //    List<IElement> elements = HTMLWorker.ParseToList(sr, null);
        //    //Open the Document
        //    itextDoc.Open();
        //    //loop through all elements
        //    foreach (IElement el in elements)
        //    {
        //        //add individual element to Document
        //        itextDoc.Add(el);
        //    }
        //    //Close the document
        //    itextDoc.Close();

        //    using (FileStream fs = System.IO.File.Create(filePath))
        //    {
        //        myMemoryStream.WriteTo(fs);
        //    }

        //    myMemoryStream.Close();
        //}

        //public static void SendMail(MessagePackage messagePackage, string path = "")
        //{
        //    if (messagePackage.MailTemplateType.HasValue)
        //    {
        //        MailTemplate mailTemplate = Db.MailTemplate.Where(m => m.Type == (int)messagePackage.MailTemplateType).FirstOrDefault();
        //        if (mailTemplate == null) return;
        //        messagePackage.Body = mailTemplate.ContentBody;
        //    }

        //    List<GeneralSettings> MailSettings = Db.GeneralSettings.Where(m => m.Category == "SMTP_Settings").ToList();

        //    if (MailSettings == null || MailSettings.Count() == 0)
        //        return;
        //    else
        //        messagePackage.MailSettings = MailSettings;

        //    if (messagePackage.From == null)
        //    {
        //        string fromEmail, fromDisplayName;
        //        if (MailSettings.FirstOrDefault(s => s.Code == "SMTP_From_Email") != null)
        //            fromEmail = MailSettings.FirstOrDefault(s => s.Code == "SMTP_From_Email").Text;
        //        else
        //            fromEmail = "mail2@sakarya.edu.tr"; //burada web config den deger okunabilir.

        //        if (MailSettings.FirstOrDefault(s => s.Code == "SMTP_From_Name") != null)
        //            fromDisplayName = MailSettings.FirstOrDefault(s => s.Code == "SMTP_From_Name").Text;
        //        else
        //            fromDisplayName = "Uzem"; //burada web config den deger okunabilir.

        //        messagePackage.From = new UserAddress(fromEmail, fromDisplayName);
        //    }

        //    foreach (var item in messagePackage.ParamsToBeChanged)
        //        messagePackage.Body = messagePackage.Body.Replace(item.Key, item.Value);
        //    EmailServiceCall.SendMail(messagePackage, path);
        //}

        public static Select2PagedResult ConvertToSelect2Format(List<selectList> selectList, int totalRoles)
        {
            Select2PagedResult jsonRoles = new Select2PagedResult();
            jsonRoles.Results = new List<Select2Result>();

            foreach (selectList a in selectList)
            {
                jsonRoles.Results.Add(new Select2Result { id = a.Id.ToString(), text = a.Text });
            }

            jsonRoles.Total = totalRoles;
            return jsonRoles;
        }

    }
}