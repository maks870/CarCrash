using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition instance;
   // private Animator animator;
    private AsyncOperation sceneOperation;

    public static void SwitchScene(string sceneName)
    {
        instance.sceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.StartLoadScene();
    }

    private void Update()
    {
        if(sceneOperation!=null && sceneOperation.progress>0.8)
            instance.sceneOperation.allowSceneActivation = true;
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

      //  animator = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
    }

    public static void SwitchScene(int idScene)
    {
        instance.sceneOperation = SceneManager.LoadSceneAsync(idScene);
        instance.StartLoadScene();
    }

    private void StartLoadScene() 
    {
        instance.sceneOperation.allowSceneActivation = false;
        //   instance.animator.SetTrigger("sceneClosing");
    }
}
