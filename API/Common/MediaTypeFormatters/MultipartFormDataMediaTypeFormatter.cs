using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Common.MediaTypeFormatters
{
    public class MultipartFormDataMediaTypeFormatter : MediaTypeFormatter
    {
        public MultipartFormDataMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }

        // Can deserialize only the types that implements IFormFile.
        public override bool CanReadType(Type type)
        {
            return type == typeof(IFormFile);
        }

        public override bool CanWriteType(Type type) => false;

        // Process of deserializing
        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var formFile = new FormFile();

            var multipartProvider = await content.ReadAsMultipartAsync();

            if (multipartProvider.Contents.Count == 0) throw new Exception("File not provided.");

            var httpContent = multipartProvider.Contents[0];

            formFile.ContentDisposition = httpContent.Headers.ContentDisposition;
            formFile.ContentType = httpContent.Headers.ContentType?.ToString();
            formFile.FileName = formFile.ContentDisposition.FileName;
            formFile.Length = httpContent.Headers.ContentLength == null ? 0 : (int)httpContent.Headers.ContentLength;
            formFile.Name = formFile.ContentDisposition.Name;
            formFile.Stream = await httpContent.ReadAsStreamAsync();

            return formFile;
        }
    }
}