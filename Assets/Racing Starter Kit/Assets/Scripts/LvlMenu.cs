using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
//this script is used in the Play button from the first menu and the Continue button when you finish the race
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
    public void Restart()
    {   //void restart is used in continue buttons when we finish the race and in return button of the pause menu
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//so it will restart the game's scene
    }
    //if we hit play in the menu at the start of the scene we will use the Play void:
    public void Play()
    {
        RaceUI.SetActive(true); //racing UI

        if (!isFreeplayMode)
        {
            //all the racing stuff turns on
            LapsSelected.SetActive(true); //turn on the lap requirement race-UI text
            FinishCamera.SetActive(false); //and the camera goes off too, to use the one in the player car
            Countdown?.SetActive(true);  //countdown UI (3,2,1,go)
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
            //soundController.SoundDistortion(true);
            Time.timeScale = 0;
            PauseMenu.SetActive(true); //show the pause menu (to resume or restart race)
        }
        else
        {
            //soundController.SoundDistortion(false);
            //unpausing reactivates audio and resumes normal time
            Time.timeScale = 1;
            PauseMenu.SetActive(false); //turn off the pause menu
        }
    }
}
