using Ocr_Tesseart_Appi.Interface;
using System.Data;
using Tesseract;

namespace Ocr_Tesseart_Appi
{
    public class OcrService : IOcrService
    {
        private readonly string _tessDataPath;
        public OcrService(IWebHostEnvironment env)
        {
            _tessDataPath = Path.Combine(env.ContentRootPath, "tessData");
        }

        public async Task<string> ExtractTextAsync(Stream imageStream)
        {
            var ms = new MemoryStream();
            await imageStream.CopyToAsync(ms);
            ms.Position = 0;

            using var img = Pix.LoadFromMemory(ms.ToArray());
            using var engine = new TesseractEngine(_tessDataPath, "eng+mya", EngineMode.Default);
            using var page = engine.Process(img);
            var rawText = page.GetText();

            var cleanedText = rawText
                .Replace("\n", "")
                .Replace("\"", "")
                .Replace("\r", "")
                .Trim();
            return cleanedText;
        }
    }
}
