using System.Collections.Generic;
using API.Common.Models;

namespace API.Common
{
    public static class Repository
    {
        public static List<CountryModel> Countries { get; set; } = new List<CountryModel>();
    }
}