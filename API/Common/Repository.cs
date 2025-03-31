using System.Collections.Generic;
using API.Common.Models;

namespace API.Common
{
    public static class Repository
    {
        public static List<CountryModel> Countries { get; set; } = new List<CountryModel>();
        public static List<UserModel> Users { get; set; } = new List<UserModel>();
    }
}