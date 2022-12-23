using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DynamicCollectibleLoaderUI
{
    public static List<T> LoadCollectiblesByType<T>()
    {
        string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
        DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
        List<T> collectible = new List<T>();

        foreach (FileInfo file in dirInfo.GetFiles())
        {
            T[] objects = Resources.LoadAll(file.FullName, typeof(T)).Cast<T>().ToArray();
            collectible.AddRange(objects);
        }

        return collectible;
    }

    public static List<CollectibleSO> LoadAllCollectibles()
    {
        string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
        DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
        List<CollectibleSO> collectible = new List<CollectibleSO>();

        foreach (FileInfo file in dirInfo.GetFiles())
        {
            CollectibleSO[] objects = Resources.LoadAll(file.FullName, typeof(CollectibleSO)) as CollectibleSO[];
            collectible.AddRange(objects);
        }
        return collectible;
    }
}
