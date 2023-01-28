using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject panel;
    [SerializeField] private AudioMixer audioMixer;
    private bool endAnimation = false;
    private Animator animator;
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

        AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(sceneName);
        sceneOperation.allowSceneActivation = false;

        while (sceneOperation.progress < 90 && !endAnimation)
        {
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