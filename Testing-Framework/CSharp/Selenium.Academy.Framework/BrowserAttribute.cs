using System;

namespace Ensek.Testing.Framework
{
    [AttributeUsage(validOn: AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class BrowserAttribute : Attribute
    {
        public string Browser { get; set; }
        public string Version { get; set; }
        public bool IsRemote { get; set; }
        public string Url { get; set; }
    }
}
