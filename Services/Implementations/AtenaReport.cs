using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Atena.Services.Interfaces;
using DataBaseContext;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;


namespace Atena.Services.Implementations;


public class AtlasFontResolverUbuntu : IFontResolver
{
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            return new FontResolverInfo("Arial");
        }

        public byte[] GetFont(string faceName)
        {
            return File.ReadAllBytes("/usr/share/fonts/truetype/dejavu/DejaVuSans.ttf");
        }
}

public class AtenaReport : IAtenaReport
{
    private readonly CaadiDbContext _context;
    public AtenaReport(CaadiDbContext context)
    {
        _context = context;

        GlobalFontSettings.FontResolver = new AtlasFontResolverUbuntu();
    }

    public List<Document> getArraysToPdsFromData(string periodText, string groupName, Dictionary<string, List<Tuple<string, string>>> monthsNuaHours)
    {
        List<Document> result = new List<Document>();
        foreach (var monthNuaHours in monthsNuaHours)
        {
            result.Add(
                createArrayFromData(periodText, groupName, monthNuaHours.Key, monthNuaHours.Value)
            );
        }

        return result;
    }

    public void savePdfs(string periodText, string groupName, List<Document> pdfs)
    {
        // 2. Create a MigraDoc renderer
        foreach (var pdf in pdfs)
        {
            var now = DateTime.Now;
             // Get the user's home directory
            string userHomeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Construct the path to the Desktop directory
            string desktopDirectory = Path.Combine(userHomeDirectory, "Desktop");

            // Combine the Desktop directory with the desired file name
            string filePath = Path.Combine(desktopDirectory, $"CaadiAtena-{now:MM}-{now:dd}-{now.Year}/{periodText}/{groupName}/");

            Directory.CreateDirectory(filePath);
        
            PdfDocumentRenderer renderer = new PdfDocumentRenderer();
            renderer.Document = pdf;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(filePath+ pdf.Comment + ".pdf");
        }
    }


    public Document createArrayFromData(string periodText,string groupName, string month, List<Tuple<string, string>> nuasHours)
    {

        var doc = new Document();

        var section = doc.AddSection();

        if (Convert.ToInt16(month) > 0)
            doc.Comment = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(month));
        else
            doc.Comment = "Final";

        PageSetup pageSetup = doc.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;


        var availableWidht = (pageSetup.PageWidth - pageSetup.LeftMargin - pageSetup.RightMargin)/4;

         section
        .AddParagraph($"[{periodText}]  -  [{groupName}]  -  [{doc.Comment}]")
        .Format.Alignment = ParagraphAlignment.Center;
    


        Table table = section.AddTable();
        table.Borders.Visible = true;

        var c1 = table.AddColumn();
        var c2 = table.AddColumn();
        var c3 = table.AddColumn();
        var c4 = table.AddColumn();


        c1.Width = availableWidht - Unit.FromCentimeter(3);
        c2.Width = availableWidht;
        c3.Width = availableWidht + Unit.FromCentimeter(3);
        c4.Width = availableWidht ;
    

        var headerRow = table.AddRow();
        headerRow.Format.Font.Bold = true;
        headerRow.Shading.Color = Color.FromRgb(200, 200, 200);
        headerRow.HeadingFormat = true;

        headerRow[0].AddParagraph("#").Format.Alignment = ParagraphAlignment.Center;
        headerRow[1].AddParagraph("NUA").Format.Alignment = ParagraphAlignment.Center;
        headerRow[2].AddParagraph("Name").Format.Alignment = ParagraphAlignment.Center;
        headerRow[3].AddParagraph("Total hours").Format.Alignment = ParagraphAlignment.Center;


        for (int i = 0; i < nuasHours.Count; i++)
        {
            Row tableRow = table.AddRow();
            string nua = nuasHours[i].Item1;
            string hours = nuasHours[i].Item2;
            string name = _context.Students
                                      .Where(s => s.Nua == nua)
                                      .Select(s => $"{s.Name} {s.FirstLastName} {s.SecondLastName}")
                                      .FirstOrDefault();

            tableRow.Cells[0].AddParagraph((i+1).ToString()).Format.Alignment = ParagraphAlignment.Center;
            tableRow.Cells[1].AddParagraph(nua).Format.Alignment = ParagraphAlignment.Center;
            tableRow.Cells[2].AddParagraph(name).Format.Alignment = ParagraphAlignment.Center;
            tableRow.Cells[3].AddParagraph(hours).Format.Alignment = ParagraphAlignment.Center;
        }

        return doc;
    }
}
