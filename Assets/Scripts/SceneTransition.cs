using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject panel;
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

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        animator = GetComponent<Animator>();
    }

    private void StartLoadScene()
    {
        panel.SetActive(true);
        instance.animator.SetTrigger("sceneClosing");
    }

    private void EndLoadScene() 
    {
        instance.sceneOperation.allowSceneActivation = true;
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
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