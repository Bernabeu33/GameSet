using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetVersionList 
{
    public List<AssetVersionItem> items;
    public AssetVersionList () {
        this.items = new List<AssetVersionItem> ();
    }
}
