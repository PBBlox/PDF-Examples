﻿// See https://aka.ms/new-console-template for more information

using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;


//Get the stream from the document.
FileStream documentStream = new FileStream(Path.GetFullPath(@"Data/Input.pdf"), FileMode.Open, FileAccess.Read);

//Load an existing PDF document.
PdfLoadedDocument document = new PdfLoadedDocument(documentStream);

//Get the page of the existing PDF document.
PdfLoadedPage loadedPage = document.Pages[0] as PdfLoadedPage;

//Create a new PDF signature without PdfCertificate instance.
PdfSignature signature = new PdfSignature(document, loadedPage, null, "Signature1");

//Hook up the ComputeHash event.
signature.ComputeHash += Signature_ComputeHash;

//Save the document into stream
MemoryStream stream = new MemoryStream();
document.Save(stream);
//Close the document
document.Close(true);


//Load an existing PDF stream..
PdfLoadedDocument loadedDocument = new PdfLoadedDocument(stream);

//Gets the first signature field of the PDF document
PdfLoadedSignatureField signatureField = loadedDocument.Form.Fields[0] as PdfLoadedSignatureField;
PdfSignature pdfSignature = signatureField.Signature;

//Create X509Certificate2 from your certificate to create a long-term validity.
X509Certificate2 x509 = new X509Certificate2(Path.GetFullPath(@"Data/PDF.pfx"), "syncfusion");

//Create LTV with your public certificates.
pdfSignature.CreateLongTermValidity(new List<X509Certificate2> { x509 });

//Create file stream.
using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"Output/Output.pdf"), FileMode.Create, FileAccess.ReadWrite))
{
    //Save the PDF document to file stream.
    loadedDocument.Save(outputFileStream);
}

//Close the document.
loadedDocument.Close(true);


void Signature_ComputeHash(object sender, PdfSignatureEventArgs ars)
{
    //Get the document bytes
    byte[] documentBytes = ars.Data;

    SignedCms signedCms = new SignedCms(new ContentInfo(documentBytes), detached: true);

    //Compute the signature using the specified digital ID file and the password
    X509Certificate2 certificate = new X509Certificate2(Path.GetFullPath(@"Data/PDF.pfx"), "syncfusion");
    CmsSigner cmsSigner = new CmsSigner(certificate);

    //Set the digest algorithm SHA256
    cmsSigner.DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1");

    signedCms.ComputeSignature(cmsSigner);

    //Embed the encoded digital signature to the PDF document
    ars.SignedData = signedCms.Encode();
}