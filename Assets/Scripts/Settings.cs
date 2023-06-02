using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings 
{
    //版本号
    public static string Version = "1.0.0";
    
    /// <summary>
    /// cdn下载地址
    /// </summary>
    public static string CDNPath = "http://192.168.17.69:9999";
    
    
    private static string _resVersion = "";
    /// <summary>
    /// 资源版本号
    /// </summary>
    public static string ResVersion {
        get
        {
            return _resVersion;
        }
        set
        {
            _resVersion = value;
        }
    }
}
