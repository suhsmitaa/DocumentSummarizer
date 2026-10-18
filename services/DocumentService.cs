using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Text;

namespace DocumentSummarizer.Services;

public class DocumentService
{
    public async Task<string> ExtractTextFromPdfAsync(Stream pdfStream)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            await pdfStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            if (memoryStream.Length == 0)
                return string.Empty;

            var sb = new StringBuilder();

            using var document = PdfDocument.Open(memoryStream);

            foreach (Page page in document.GetPages())
            {
                var text = page.Text?.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    sb.AppendLine(text);
                    sb.AppendLine();
                }
            }

            return sb.ToString().Trim();
        }
        catch (Exception ex)
        {
            return $"ERROR_EXTRACTING_PDF: {ex.Message}";
        }
    }

    public string ExtractTextFromPlain(string text) => text?.Trim() ?? string.Empty;
}