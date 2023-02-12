using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SOLoader
{
    public static void LoadAllSO(AssetLabelReference assetLabel)
    {
        //List<T> loadedSO = new List<T>();
        //Type t = typeof(T);

        //T[] scriptableObjects = Resources.LoadAll("ScriptableObjects/" + t.Name, t).Cast<T>().ToArray();
        //ScriptableObject[] scriptableObjects =
        Addressables.LoadAssetsAsync<ScriptableObject>(assetLabel, (scriptableObjects) => { QEw(scriptableObjects); });

        //loadedSO.AddRange(scriptableObjects);

    }

    private static void QEw(ScriptableObject scriptableObject) 
    {

    }

    //public static T LoadSO<T>(string name) where T : ScriptableObject
    //{
    //    foreach (T scriptableObject in LoadAllSO<T>())
    //    {
    //        if (scriptableObject.name == name)
    //            return scriptableObject;
    //    }

    //    return null;
    //}



    //public static List<T> LoadSOByType<T>()
    //{
    //    string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
    //    DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
    //    List<T> scriptableObjects = new List<T>();

    //    foreach (DirectoryInfo dir in dirInfo.GetDirectories())
    //    {
    //        T[] objects = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(T)).Cast<T>().ToArray();
    //        scriptableObjects.AddRange(objects);
    //    }

    //    return scriptableObjects;
    //}

    //public static MapSO LoadMapByName(string name)
    //{
    //    string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
    //    DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
    //    List<MapSO> sctriptableObjects = new List<MapSO>();
    //    MapSO scriptableObj;

    //    foreach (DirectoryInfo dir in dirInfo.GetDirectories())
    //    {
    //        MapSO[] maps = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(MapSO)).Cast<MapSO>().ToArray();
    //        sctriptableObjects.AddRange(maps);
    //    }

    //    scriptableObj = sctriptableObjects[0];

    //    foreach (MapSO maps in sctriptableObjects)
    //    {
    //        if (maps.Name == name)
    //            scriptableObj = maps;
    //    }

    //    return scriptableObj;
    //}

    //public static T LoadCollectibleByName<T>(string name) where T : CollectibleSO
    //{
    //    string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
    //    DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
    //    List<T> sctriptableObjects = new List<T>();
    //    T scriptableObj;

    //    foreach (DirectoryInfo dir in dirInfo.GetDirectories())
    //    {
    //        T[] objects = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(T)).Cast<T>().ToArray();
    //        sctriptableObjects.AddRange(objects);
    //    }

    //    scriptableObj = sctriptableObjects[0];

    //    foreach (T obj in sctriptableObjects)
    //    {
    //        if (obj.Name == name)
    //            scriptableObj = obj;
    //    }

    //    return scriptableObj;
    //}

    //public static List<CollectibleSO> LoadAllCollectibles()
    //{
    //    string resourcesPath = Application.dataPath + "/Resources/ScriptableObjects";
    //    DirectoryInfo dirInfo = new DirectoryInfo(resourcesPath);
    //    List<CollectibleSO> collectible = new List<CollectibleSO>();

    //    foreach (DirectoryInfo dir in dirInfo.GetDirectories())
    //    {
    //        CollectibleSO[] objects = Resources.LoadAll("ScriptableObjects/" + dir.Name, typeof(CollectibleSO)).Cast<CollectibleSO>().ToArray();
    //        collectible.AddRange(objects);
    //    }

    //    return collectible;
    //}
}
