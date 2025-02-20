﻿// See https://aka.ms/new-console-template for more information

using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;

//Initialize HTML converter with WebKit rendering engine.
HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();

//Create the Webkit settings. 
WebKitConverterSettings webKitSettings = new WebKitConverterSettings();

//Disable JavaScript; By default - true.
webKitSettings.EnableJavaScript = false;

//Assign WebKit settings to HTML converter.
htmlConverter.ConverterSettings = webKitSettings;

//Convert URL to PDF.
PdfDocument document = htmlConverter.Convert("https://www.google.com");

//Create file stream.
using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"../../../Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
{
    //Save the PDF document to file stream.
    document.Save(outputFileStream);
}

//Close the document.
document.Close(true);