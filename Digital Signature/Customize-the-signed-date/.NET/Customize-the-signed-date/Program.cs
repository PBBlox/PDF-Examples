﻿using Syncfusion.Pdf;
using Syncfusion.Pdf.Security;
using Syncfusion.Drawing;

//Create a new PDF document instance.
PdfDocument document = new PdfDocument();
//Add a new page to the PDF document.
PdfPage page = document.Pages.Add();
//Create a digital signature and associate it with the added page.
PdfSignature signature = new PdfSignature(page, "Signature", new DateTime(2020, 12, 24, 10, 50, 10));
//Set the timestamp server for the digital signature.
signature.TimeStampServer = new TimeStampServer(new Uri("http://timestamp.digicert.com"));
//Set the signed name for the signature.
signature.SignedName = "Test";
//Define the bounds for the signature (position and size on the page).
signature.Bounds = new RectangleF(new PointF(0, 0), new SizeF(200, 100));
//Save the document to a file using a file stream.
using (FileStream fileStream = new FileStream(Path.GetFullPath(@"Output\Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
{
    document.Save(fileStream);
}
//Close the document to release resources.
document.Close(true);
