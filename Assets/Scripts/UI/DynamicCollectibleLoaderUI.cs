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

        foreach (DirectoryInfo file in dirInfo.GetDirectories())
        {
            T[] objects = Resources.LoadAll(file.FullName, typeof(T)).Cast<T>().ToArray();

            //if (objects.Length > 0)
            //    Debug.Log("чето скачали");

            collectible.AddRange(objects);
        }

        return collectible;
    }

    public static List<CollectibleSO> LoadAllCollectibles()
    {
        string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
        DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
        List<CollectibleSO> collectible = new List<CollectibleSO>();

        foreach (DirectoryInfo file in dirInfo.GetDirectories())
        {
            CollectibleSO[] objects = Resources.LoadAll(file.FullName, typeof(CollectibleSO)) as CollectibleSO[];
            collectible.AddRange(objects);
        }
        return collectible;
    }
}
