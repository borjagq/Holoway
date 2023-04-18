using System.Collections;
using System.Collections.Generic;
public class GlobalGameStrings
{
    public static string VERSION_MAJOR = "0";
    public static string VERSION_MINOR = "1";
    public static string RELEASE_TYPE = "a";

    public static string VOICE_APP_ID_STRING = "818c72249b1849e298987f76b6ade2b5";                                          //Used to connect to the Agora application
    
    public static string GetBuildInfo()
    {
        return (GlobalGameStrings.VERSION_MAJOR + "." + GlobalGameStrings.VERSION_MINOR + GlobalGameStrings.RELEASE_TYPE);
    }
    
}
