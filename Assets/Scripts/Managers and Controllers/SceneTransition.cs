using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private AudioMixer audioMixer;
    private AsyncOperationHandle<SceneInstance> previousHandle;
    private AsyncOperationHandle<SceneInstance> currentHandlehandle;
    private bool endAnimation = false;
    private Animator animator;
    public static SceneTransition instance;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(this.gameObject);
        animator = GetComponent<Animator>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panel.SetActive(false);
        endAnimation = false;


        if (previousHandle.IsValid())
            Addressables.UnloadSceneAsync(previousHandle, true);

    }


    public static void SwitchScene(string sceneName)
    {
        SOLoader.instance.Clear();
        instance.StartCoroutine(instance.SceneLoadComplete(sceneName));
    }

    private IEnumerator SceneLoadComplete(string sceneName)
    {
        instance.panel.SetActive(true);
        instance.audioMixer.SetFloat("Master", -80);
        instance.animator.SetTrigger("sceneClosing");

        if (previousHandle.IsValid())
            previousHandle = currentHandlehandle;

        yield return new WaitForEndOfFrame();

        instance.currentHandlehandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single, false);

        while (!endAnimation)
        {
            yield return null;
        }

        while (currentHandlehandle.Status != AsyncOperationStatus.Succeeded)
        {
            yield return null;
        }

        currentHandlehandle.Result.ActivateAsync();
    }

    public void EndPreload()
    {
    }

    public void EndAnimation()
    {
        endAnimation = true;
    }
}