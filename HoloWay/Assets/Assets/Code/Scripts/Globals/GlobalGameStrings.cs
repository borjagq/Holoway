using System.Collections;
using System.Collections.Generic;
public class GlobalGameStrings
{
    public static string VERSION_MAJOR = "0";
    public static string VERSION_MINOR = "1";
    public static string RELEASE_TYPE = "a";
    public static string GetBuildInfo()
    {
        return (GlobalGameStrings.VERSION_MAJOR + "." + GlobalGameStrings.VERSION_MINOR + GlobalGameStrings.RELEASE_TYPE);
    }
}
