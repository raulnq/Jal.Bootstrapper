using System;

namespace Jal.Bootstrapper.CastleWindsor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InstallerTagAttribute : Attribute
    {
        public string Tag { get; set; }

        public InstallerTagAttribute(string tag)
        {
            Tag = tag;
        }
    }
}
