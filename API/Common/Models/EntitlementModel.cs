using System.Collections.Generic;

namespace API.Common.Models
{
    public class EntitlementModel
    {
        public string ComponentCode { get; set; }
        public List<string> ActionCodes { get; set; }
    }
}