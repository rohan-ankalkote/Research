using System;

namespace API.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class EntitlementAttribute : Attribute
    {
        public string ComponentCode { get; set; }
        public string ActionCode { get; set; }
        public bool OpenResource { get; set; }
    }
}