using System;

namespace Jal.Bootstrapper.CastleWindsor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InstallerTagAttribute : Attribute
    {
    }
}
