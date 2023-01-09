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
        List<T> scriptableObjects = new List<T>();

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            T[] objects = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(T)).Cast<T>().ToArray();
            scriptableObjects.AddRange(objects);
        }

        return scriptableObjects;
    }

    public static CollectibleSO LoadCollectibleByName(string name)
    {
        string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
        DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
        List<CollectibleSO> sctriptableObjects = new List<CollectibleSO>();
        CollectibleSO scriptableObj;

        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            CollectibleSO[] objects = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(CollectibleSO)).Cast<CollectibleSO>().ToArray();
            sctriptableObjects.AddRange(objects);
        }

        scriptableObj = sctriptableObjects[0];

        foreach (CollectibleSO obj in sctriptableObjects)
        {
            if (obj.Name == name)
                scriptableObj = obj;
        }

        return scriptableObj;
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
