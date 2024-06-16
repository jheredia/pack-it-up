using System;
using System.Collections.Generic;
using UnityEngine;

public partial class ResourceManager : Singleton<ResourceManager>
{
    private AssetBundle mainAssetBundle = null;
    private Dictionary<string, string> assetPathsInAssetBundle = new Dictionary<string, string>();

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        path = path.ToLower();
        T result = null;
        if (Utils.IsUseLocalAssetBundle() && IsAssetPathExistInAssetBundle(path))
        {
            result = mainAssetBundle.LoadAsset<T>(assetPathsInAssetBundle[path]);
        }
        else
        {
            result = Resources.Load<T>(path);
        }

        return result;
    }
    public void SetAssetBundle(AssetBundle ab)
    {
        mainAssetBundle = ab;
        string assetPath = null;
        string[] assets = ab.GetAllAssetNames();
        for (int i = 0; i < assets.Length; i++)
        {
            assetPath = assets[i];
            assetPath = assetPath.Replace("assets/resources/", "");
            assetPath = assetPath.Substring(0, assetPath.IndexOf('.'));
            assetPathsInAssetBundle[assetPath] = assets[i];
        }
    }

    private bool IsAssetPathExistInAssetBundle(string path)
    {
        return mainAssetBundle != null && assetPathsInAssetBundle.ContainsKey(path.ToLower());
    }

    protected override void OnInit()
    {
        base.OnInit();
    }
}
