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
    private AsyncOperationHandle<SceneInstance> handle;
    private bool unloaded = true;
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
        if (!unloaded) 
        {
            Addressables.UnloadSceneAsync(handle, true);
            unloaded = true;
        }
    }

    public static void SwitchScene(string sceneName)
    {
        instance.unloaded = false;
        instance.StartCoroutine(instance.SceneLoadComplete(sceneName));
    }

    private IEnumerator SceneLoadComplete(string sceneName)
    {
        instance.panel.SetActive(true);
        instance.audioMixer.SetFloat("Master", -80);
        instance.animator.SetTrigger("sceneClosing");
        yield return new WaitForEndOfFrame();

        instance.handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single, false);

        while (!endAnimation)
        {
            yield return null;
        }

        while (handle.Status != AsyncOperationStatus.Succeeded) 
        {
            yield return null;
        }

        handle.Result.ActivateAsync();
    }

    public void EndPreload()
    {
    }

    public void EndAnimation()
    {
        endAnimation = true;
    }
}