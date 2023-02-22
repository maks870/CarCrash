using UnityEngine;

public class LvlMenu : MonoBehaviour
{
    [SerializeField] private GameObject RaceUI, Countdown, FinishCamera, LapsSelected;
    //also, it is used if we hit restart in the pause menu
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private MissionManager missionManager;
    [SerializeField] SoundController soundController;
    [SerializeField] private bool isFreeplayMode = false;
    private float oldVolume;

    private bool endRace = false;
    public bool EndRace { get => endRace; set => endRace = value; }

    private void Start()
    {
        Play();
        soundController.AudioMixer.GetFloat("Effect", out oldVolume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !EndRace)
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
            soundController.AudioMixer.SetFloat("Effect", -80);

            if (missionManager != null && !missionManager.IsActiveDialogue)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
        else
        {
            soundController.AudioMixer.SetFloat("Effect", oldVolume);

            if (missionManager != null && !missionManager.IsActiveDialogue)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            Time.timeScale = 1;
            PauseMenu.SetActive(false);
        }
    }
}
