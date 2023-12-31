using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public delegate void AddressableHandler();
public delegate void AddressableSOHandler(ScriptableObject obj);
public class SOLoader : MonoBehaviour
{
    public static SOLoader instance;

    public event AddressableHandler EndLoadingEvent;
    public event AddressableSOHandler OnLoadingEvent;
    private bool isResourcesLoaded = false;
    private Dictionary<string, AsyncOperationHandle> SOHandleDictionary = new Dictionary<string, AsyncOperationHandle>();
    private Dictionary<string, AsyncOperationHandle> uniqueHandleDictionary = new Dictionary<string, AsyncOperationHandle>();
    private Dictionary<string, Action<AsyncOperationHandle>> uniqueHandleActions = new Dictionary<string, Action<AsyncOperationHandle>>();

    public bool IsResourcesLoaded => isResourcesLoaded;

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        EndLoadingEvent += () => isResourcesLoaded = true;

#if !UNITY_EDITOR
        EndLoadingEvent += () => EndLoad();
#endif
        DontDestroyOnLoad(gameObject);
    }

    private void CheckEndLoading()
    {
        if (SOHandleDictionary.Count != 4)
            return;

        EndLoadingEvent?.Invoke();
    }

    [DllImport("__Internal")]
    private static extern void EndLoad();

    public void LoadAll()
    {
        if (!isResourcesLoaded)
        {
            StartLoadAllSO<CharacterModelSO>();
            StartLoadAllSO<CarColorSO>();
            StartLoadAllSO<CarModelSO>();
            StartLoadAllSO<MapSO>();
        }
        else
        {
            EndLoadingEvent?.Invoke();
        }
    }

    public void Clear()
    {
        if (OnLoadingEvent != null)
        {
            foreach (Delegate d in OnLoadingEvent.GetInvocationList())
            {
                OnLoadingEvent -= (AddressableSOHandler)d;
            }
        }

        if (EndLoadingEvent != null)
        {
            foreach (Delegate d in EndLoadingEvent.GetInvocationList())
            {
                EndLoadingEvent -= (AddressableHandler)d;
            }
        }

        foreach (var handle in uniqueHandleDictionary)
        {
            Addressables.Release(handle.Value);
        }

        uniqueHandleDictionary.Clear();
        uniqueHandleActions.Clear();
    }

    private void StartLoadAllSO<T>() where T : ScriptableObject
    {
        string assetLabel = typeof(T).Name;
        AsyncOperationHandle handle = Addressables.LoadAssetsAsync<T>(assetLabel, scriptableObject =>
        {
            OnLoadingEvent?.Invoke(scriptableObject);
        });

        handle.Completed += (operation) =>
        {
            SOHandleDictionary.Add(assetLabel, handle);
            CheckEndLoading();
        };
    }

    public List<T> GetSOList<T>()
    {
        string assetLabel = typeof(T).Name;
        AsyncOperationHandle handle;
        SOHandleDictionary.TryGetValue(assetLabel, out handle);
        return (List<T>)handle.Convert<IList<T>>().Result;
    }

    public void LoadAllSO<T>(Action<List<T>> action) where T : ScriptableObject // Legacy
    {
        AsyncOperationHandle handle;
        string assetLabel = typeof(T).Name;
        List<T> obj = new List<T>();
        handle = Addressables.LoadAssetsAsync<T>(assetLabel, scriptableObject => obj.Add(scriptableObject));
        handle.Completed += (operation) =>
        {
            action.Invoke(obj);
        };
    }

    public void LoadSO<T>(string name, Action<T> action) where T : ScriptableObject// Legacy
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(name);

        handle.Completed += (operation) =>
        {
            action.Invoke(handle.Result);
        };
    }

    //public static void LoadSO<T>(string name, Action<T> action) where T : ScriptableObject
    //{
    //    AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(name);

    //    handle.Completed += (operation) =>
    //    {
    //        action.Invoke(handle.Result);
    //        //Addressables.Release(handle);
    //    };
    //}

    public void LoadAssetReference<T>(AssetReference assetReference, Action<T> action) where T : UnityEngine.Object
    {
        T obj = default(T);
        AsyncOperationHandle handle;
        Action<AsyncOperationHandle> handleCompletedActions;
        bool isLoadedAssetReference = uniqueHandleDictionary.TryGetValue(assetReference.RuntimeKey.ToString(), out handle);
        if (isLoadedAssetReference)
        {
            if (handle.IsDone)
            {
                obj = handle.Convert<T>().Result;
                action.Invoke(obj);
            }
            else
            {
                uniqueHandleActions.TryGetValue(assetReference.RuntimeKey.ToString(), out handleCompletedActions);
                handleCompletedActions += (handle) =>
                {
                    action.Invoke(handle.Convert<T>().Result);
                };
            }
        }
        else
        {
            AsyncOperationHandle<T> newHandle = Addressables.LoadAssetAsync<T>(assetReference);
            uniqueHandleDictionary.Add(assetReference.RuntimeKey.ToString(), newHandle);

            uniqueHandleActions.Add(assetReference.RuntimeKey.ToString(), (handle) =>
            {
                action.Invoke(handle.Convert<T>().Result);
            });

            uniqueHandleActions.TryGetValue(assetReference.RuntimeKey.ToString(), out handleCompletedActions);

            newHandle.Completed += (operation) =>
            {
                handleCompletedActions.Invoke(operation);
            };
        }
    }

    public void LoadAsset<T>(string assetName, Action<T> action) //Legacy
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetName);

        handle.Completed += (operation) =>
        {
            action.Invoke(handle.Result);
        };
    }

    //public List<T> LoadAllSO<T>() where T : ScriptableObject
    //{
    //    string assetLabel = typeof(T).Name;
    //    List<T> obj = new List<T>();
    //    AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(assetLabel, scriptableObject => obj.Add(scriptableObject));
    //    handle.WaitForCompletion();
    //    Addressables.Release(handle);
    //    return obj;
    //}

    //public static T LoadSO<T>(string name) where T : ScriptableObject
    //{
    //    AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(name);
    //    handle.WaitForCompletion();
    //    T obj = handle.Result;
    //    Addressables.Release(handle);
    //    return obj;
    //}

    //public static T LoadAsset<T>(AssetReference assetReference) where T : ScriptableObject
    //{
    //    AsyncOperationHandle<T> handle = assetReference.LoadAssetAsync<T>();
    //    handle.WaitForCompletion();
    //    T obj = handle.Result;
    //    assetReference.ReleaseAsset();
    //    return obj;
    //}

    //public static void ReleaseSO<T>(List<T> assetList)
    //{
    //    for (int i=0; i< assetList.Count; i++)
    //    { 
    //    assetList[i].
    //    }
    //}

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
