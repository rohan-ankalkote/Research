using System.IO;
using System.Net.Http.Headers;

namespace API.Common.MediaTypeFormatters
{
    public interface IFormFile
    {
        ContentDispositionHeaderValue ContentDisposition { get; set; }
        string ContentType { get; set; }
        string FileName { get; set; }
        int Length { get; set; }
        string Name { get; set; }

        Stream OpenReadStream();
    }
}