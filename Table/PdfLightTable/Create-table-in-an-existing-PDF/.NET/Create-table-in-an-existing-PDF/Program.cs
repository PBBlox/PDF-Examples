﻿// See https://aka.ms/new-console-template for more information

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Tables;

//Get stream from an existing PDF document. 
FileStream docStream = new FileStream(Path.GetFullPath("../../../Input.pdf"), FileMode.Open, FileAccess.Read);

//Load the PDF document.
PdfLoadedDocument document = new PdfLoadedDocument(docStream);

//Get first page from document.
PdfLoadedPage page = document.Pages[0] as PdfLoadedPage;

//Create PDF graphics for the page.
PdfGraphics graphics = page.Graphics;

// Create a PdfLightTable.
PdfLightTable pdfLightTable = new PdfLightTable();

//Add values to list.
List<object> data = new List<object>();
object row = new { Name = "abc", Age = "21", Sex = "Male" };

data.Add(row);

//Add list to IEnumerable
IEnumerable<object> table = data;

//Assign data source.
pdfLightTable.DataSource = table;

//Show header of the table.
pdfLightTable.Style.ShowHeader = true;

//Draw PdfLightTable.
pdfLightTable.Draw(graphics, new Syncfusion.Drawing.PointF(0, 40));

//Create file stream.
using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
{
    //Save the PDF document to file stream.
    document.Save(outputFileStream);
}

//Close the document.
document.Close(true);

