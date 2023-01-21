using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private AudioMixer audioMixer;
    private bool endAnimation = false;
    private Animator animator;
    private AsyncOperation sceneOperation;
    public static SceneTransition instance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
    }

    public static void SwitchScene(string sceneName)
    {
        instance.StartCoroutine(instance.StartLoadScene(sceneName));
    }

    public static void SwitchScene(int idScene)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(idScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SwitchScene(sceneName);
    }

    private IEnumerator StartLoadScene(string sceneName)
    {
        panel.SetActive(true);
        audioMixer.SetFloat("Master", -80);
        instance.animator.SetTrigger("sceneClosing");

        sceneOperation = SceneManager.LoadSceneAsync(sceneName);
        sceneOperation.allowSceneActivation = false;

        while (instance.sceneOperation.progress < 0.85f && !endAnimation)
        {
            //loadingImage.fillAmount = instance.sceneOperation.progress;
            yield return null;
        }

        sceneOperation.allowSceneActivation = true;
    }

    public void EndPreload()
    {
    }

    public void EndAnimation()
    {
        endAnimation = true;
    }
}