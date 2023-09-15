using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using Syncfusion.Drawing;

namespace TextExtractionSample {
    internal class Program {
        static void Main(string[] args) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");

            ExtractText();
            //Extract_Layout();
            //ExtractText_EntirePDF();
            //Extract_Bounds();

        }
        /// <summary>
        /// Extract text from a specific page
        /// </summary>
        static void ExtractText() {
            //Get stream from an existing PDF document. 
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Input.pdf"), FileMode.Open, FileAccess.Read);
            //Load the PDF document. 
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Load the first page. 
            PdfPageBase page = loadedDocument.Pages[0];
            //Extract text from first page. 
            string extractedText = page.ExtractText();
            //Save the text.  
            File.WriteAllText("Result.txt", extractedText);
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Layout-based text extraction
        /// </summary>
        static void Extract_Layout() {
            //Get stream from an existing PDF document. 
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Invoice.pdf"), FileMode.Open, FileAccess.Read);
            //Load the PDF document.
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Load first page.
            PdfPageBase page = loadedDocument.Pages[0];
            //Extract text from first page.
            string extractedTexts = page.ExtractText(true);
            //Save the text.
            File.WriteAllText("data.txt", extractedTexts);
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Extract all text from the entire PDF document
        /// </summary>
        static void ExtractText_EntirePDF() {
            //Get stream from an existing PDF document.
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Data.pdf"), FileMode.Open, FileAccess.Read);
            //Load the PDF document.
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            string extractedText = string.Empty;
            // Extract all the text from the PDF document pages.
            foreach (PdfLoadedPage loadedPage in loadedDocument.Pages) {
                extractedText += loadedPage.ExtractText();
            }
            //Save the text to file.
            File.WriteAllText("data.txt", extractedText);
            //Close the document.
            loadedDocument.Close(true);
        }
        /// <summary>
        /// Extract text from predefined bounds
        /// </summary>
        static void Extract_Bounds() {
            //Get stream from an existing PDF document. 
            FileStream docStream = new FileStream(Path.GetFullPath("../../../Invoice.pdf"), FileMode.Open, FileAccess.Read);
            //Load the PDF document.
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Get the first page of the loaded PDF document.
            PdfPageBase page = loadedDocument.Pages[0];
            //Create line collection. 
            var lineCollection = new TextLineCollection();
            //Extract text from the first page.
            page.ExtractText(out lineCollection);
            RectangleF textBounds = new RectangleF(474.96198f, 161.62997f, 50.040073f, 9);
            string invoiceNumber = "";
            //Get the text provided in the bounds.
            foreach (TextLine textLine in lineCollection.TextLine) {
                foreach (TextWord word in textLine.WordCollection) {
                    if (textBounds==word.Bounds) {
                        invoiceNumber = word.Text;
                        break;
                    }
                }
            }
            //Save the text.
            File.WriteAllText("data.txt", invoiceNumber);
            //Close the PDF document. 
            loadedDocument.Close(true);
        }       
    }
}