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
    private AsyncOperation sceneOperation;
    public static SceneTransition instance;


    private void Update()
    {
        if (sceneOperation != null)
        {
            loadingImage.fillAmount = instance.sceneOperation.progress;

            if (instance.sceneOperation.progress >= 0.85 && endAnimation) 
            {
                EndLoadScene();
            }  
        }          
    }

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

    private void StartLoadScene()
    {
        panel.SetActive(true);
        audioMixer.SetFloat("Master", -80);
        instance.animator.SetTrigger("sceneClosing");
    }

    private void EndLoadScene() 
    {
        instance.sceneOperation.allowSceneActivation = true;
        
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panel.SetActive(false);
        endAnimation = false;
    }

    public static void SwitchScene(string sceneName)
    {
        instance.StartLoadScene();
        instance.sceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.sceneOperation.allowSceneActivation = false;
    }

    public static void SwitchScene(int idScene)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(idScene);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        SwitchScene(sceneName);
    }

    public void EndPreload()
    {
    }

    public void EndAnimation() 
    {
        endAnimation = true;
    }
}