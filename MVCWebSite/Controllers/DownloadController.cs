using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebSite.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Report(string PatientId, string ReportType)
        {
            string fullName = Server.MapPath("/Thesis/Proposal 1.0.docx");

            byte[] fileBytes = GetFile(fullName);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "ProposalReport.docx");
        }

        byte[] GetFile(string fullFileName)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(fullFileName);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(fullFileName);
            return data;
        }
    }
}