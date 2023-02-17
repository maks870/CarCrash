using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class Scenereloader : MonoBehaviour
{
    public void ReloadScene()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

        while (!sceneHandle.IsDone)
        {
            yield return null;
        }

        SceneInstance sceneInstance = sceneHandle.Result;
        sceneInstance.ActivateAsync();
    }
}
