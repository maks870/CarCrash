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
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
        animator = GetComponent<Animator>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panel.SetActive(false);
        endAnimation = false;
    }


    public static void SwitchScene(string sceneName)
    {
        instance.StartCoroutine(instance.SceneLoad(sceneName, LoadSceneMode.Single));
    }

    private IEnumerator SceneLoad(string sceneName, LoadSceneMode loadSceneMode)
    {
        instance.panel.SetActive(true);
        instance.audioMixer.SetFloat("Master", -80);
        instance.animator.SetTrigger("sceneClosing");

        yield return new WaitForEndOfFrame();

        while (!SOLoader.instance.IsResourcesLoaded)
        {
            yield return null;
        }

        SOLoader.instance.Clear();
        instance.currentHandlehandle = Addressables.LoadSceneAsync(sceneName, loadSceneMode, false);

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

    public static void ReloadScene()
    {
        SwitchScene(SceneManager.GetActiveScene().name);
    }

    public void EndPreload()
    {
    }

    public void EndAnimation()
    {
        endAnimation = true;
    }
}