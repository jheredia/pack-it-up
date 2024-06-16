using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Utils
{
    public static string GetPlatform()
    {
        string platform = "iOS";
#if UNITY_IOS
      platform = "iOS";
#elif UNITY_ANDROID
      platform = "Android";
#elif UNITY_WEBGL
    platform = "WebGL";
#endif

        return platform;
    }

    public static bool IsUseLocalAssetBundle()
    {
        bool value = false;
#if LOCAL_STREAMING_ASSETBUNDLE
    value = true;
#endif

        return value;
    }

    public static bool IsUseLocalBlueprint()
    {
        bool value = false;
#if LOCAL_BLUEPRINT
    value = true;
#endif

        return value;
    }

    public static bool IsUnityWebGL()
    {
        bool value = false;
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            value = true;
        }

        return value;
    }

    public static bool IsUnityIOS()
    {
        bool value = false;
#if UNITY_IOS
    value = true;
#endif

        return value;
    }

    public static bool IsUnityAndroid()
    {
        bool value = false;
#if UNITY_ANDROID
    value = true;
#endif

        return value;
    }

    public static string GetPersistentAssetsPath()
    {
        return Path.Combine(Application.persistentDataPath, "Contents");
    }

    public static void WriteData(string filePath, byte[] data)
    {
        File.WriteAllBytes(filePath, data);
    }

    public static string GetLocalGamePath()
    {
        string path = GetPersistentAssetsPath();
        CreateFolder(path);

        return path + Path.DirectorySeparatorChar;
    }

    public static void CreateFolder(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}


public static class GameObjectExtensions
{
    public static void DestroyChildren(this GameObject t)
    {
        t.transform.Cast<Transform>().ToList().ForEach(c => UnityEngine.Object.Destroy(c.gameObject));
    }

    public static void DestroyChildrenImmediate(this GameObject t)
    {
        t.transform.Cast<Transform>().ToList().ForEach(c => UnityEngine.Object.DestroyImmediate(c.gameObject));
    }
}