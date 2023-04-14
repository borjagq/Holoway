using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class INIFile
{
    
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
    private string path = "";
    public INIFile()
    {

    }
    public INIFile(string filePath)
    {
        this.path = filePath;
    }
    public void LoadFromFile(string filePath)
    {
        this.path = filePath;
    }
    public void IniWriteValue(string Section, string Key, string Value)
    {
        WritePrivateProfileString(Section, Key, Value, this.path);
    }
    public string IniReadValue(string Section, string Key)
    {
        const int TOTAL_SIZE = 4 * 1024;
        StringBuilder temp = new StringBuilder(TOTAL_SIZE);
        GetPrivateProfileString(Section, Key, "", temp, TOTAL_SIZE, this.path);
        return temp.ToString();

    }
}
