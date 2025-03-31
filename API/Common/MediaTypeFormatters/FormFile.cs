using System.IO;
using System.Net.Http.Headers;

namespace API.Common.MediaTypeFormatters
{
    public class FormFile : IFormFile
    {
        public ContentDispositionHeaderValue ContentDisposition { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public int Length { get; set; }
        public string Name { get; set; }
        public Stream OpenReadStream() => Stream;

        public Stream Stream { get; set; }
    }
}