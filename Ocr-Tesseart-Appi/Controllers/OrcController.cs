using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocr_Tesseart_Appi.Interface;

namespace Ocr_Tesseart_Appi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrcController : ControllerBase
    {
        private readonly IOcrService _iOcrService;
        public OrcController(IOcrService iOcrService)
        {
            _iOcrService = iOcrService;
        }

        [HttpPost("extract")]
        public async Task<IActionResult> ExtractText(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not found.");

            using var stream = file.OpenReadStream();
            var text = await _iOcrService.ExtractTextAsync(stream);
            return Ok(new {text});
        }
    }
}
