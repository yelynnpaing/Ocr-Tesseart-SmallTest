namespace Ocr_Tesseart_Appi.Interface
{
    public interface IOcrService
    {
        Task<string> ExtractTextAsync(Stream imageStream);
    }
}
