﻿// See https://aka.ms/new-console-template for more information

using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

//Create a new document with PDF/A-2b standard.
PdfDocument document = new PdfDocument(PdfConformanceLevel.Pdf_A2B);

//Add a page to the document.
PdfPage page = document.Pages.Add();

//Create PDF graphics for the page.
PdfGraphics graphics = page.Graphics;

//Load the TrueType font from the local file.
FileStream fontStream = new FileStream(Path.GetFullPath("../../../Arial.ttf"), FileMode.Open, FileAccess.Read);

//Create the PDF font file. 
PdfFont font = new PdfTrueTypeFont(fontStream, 14);

//Draw the text.
graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));

//Create file stream.
using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
{
    //Save the PDF document to file stream.
    document.Save(outputFileStream);
}

//Close the document.
document.Close(true);
