using UnityEngine;

public class LvlMenu : MonoBehaviour
{
    [SerializeField] private GameObject RaceUI, Countdown, FinishCamera, LapsSelected;
    //also, it is used if we hit restart in the pause menu
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] SoundController soundController;
    [SerializeField] private bool isFreeplayMode = false;
    private void Start()
    {
        Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Restart()
    {   
        SceneTransition.ReloadScene();
    }
   
    public void Play()
    {
        RaceUI.SetActive(true); 

        if (!isFreeplayMode)
        {
            LapsSelected.SetActive(true); 
            FinishCamera.SetActive(false); 
            Countdown?.SetActive(true); 
        }

        SetPause(false);
    }

    public void Exit()
    {
        SetPause(false);
        SceneTransition.SwitchScene("General");
    }

    public void ExitInCity()
    {
        SetPause(false);
        SceneTransition.SwitchScene("SimpleTown");
    }

    public void Pause()
    {
        SetPause(true);
    }

    private void SetPause(bool paused)
    {
        if (paused)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            PauseMenu.SetActive(true); 
        }
        else
        {
            Cursor.visible = false;
            Time.timeScale = 1;
            PauseMenu.SetActive(false); 
        }
    }
}
