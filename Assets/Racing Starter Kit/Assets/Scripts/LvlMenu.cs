using UnityEngine;
using UnityEngine.SceneManagement;
//this script is used in the Play button from the first menu and the Continue button when you finish the race
public class LvlMenu : MonoBehaviour
{
    [SerializeField] private GameObject RaceUI, Countdown, FinishCamera, Checkpoints, LapsSelected;
    //also, it is used if we hit restart in the pause menu
    [SerializeField] private GameObject PauseMenu;

    private void Start()
    {
        Play();
    }
    public void Restart()
    {   //void restart is used in continue buttons when we finish the race and in return button of the pause menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//so it will restart the game's scene
    }
    //if we hit play in the menu at the start of the scene we will use the Play void:
    public void Play()
    {   //all the racing stuff turns on
        RaceUI.SetActive(true); //racing UI
        Countdown.SetActive(true);  //countdown UI (3,2,1,go)
        Checkpoints.SetActive(true); //racetrack checkpoints
        LapsSelected.SetActive(true); //turn on the lap requirement race-UI text
        FinishCamera.SetActive(false); //and the camera goes off too, to use the one in the player car
        SetPause(false);
    }

    public void Exit()
    {
        SetPause(false);
        SceneTransition.SwitchScene(0);//so it will restart the game's scene
    }

    public void Pause() 
    {
        SetPause(true);
    }

    private void SetPause(bool paused) 
    {
        if (paused)
        {
            //pausing will turn off audio and stop time
            AudioListener.volume = 0f;
            Time.timeScale = 0;
            PauseMenu.SetActive(true); //show the pause menu (to resume or restart race)
        }
        else
        {
            //unpausing reactivates audio and resumes normal time
            AudioListener.volume = 1f;
            Time.timeScale = 1;
            PauseMenu.SetActive(false); //turn off the pause menu
        }
    }
}
