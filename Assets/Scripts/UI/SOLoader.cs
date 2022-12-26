using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SOLoader
{
    public static List<T> LoadSOByType<T>()
    {
        string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
        DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
        List<T> sctriptableObjects = new List<T>();

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            T[] objects = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(T)).Cast<T>().ToArray();
            sctriptableObjects.AddRange(objects);
        }

        return sctriptableObjects;
    }

    public static List<CollectibleSO> LoadAllCollectibles()
    {
        string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
        DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
        List<CollectibleSO> collectible = new List<CollectibleSO>();

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            CollectibleSO[] objects = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(CollectibleSO)).Cast<CollectibleSO>().ToArray();
            collectible.AddRange(objects);
        }

        return collectible;
    }
}
