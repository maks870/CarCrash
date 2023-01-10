using UnityEngine;

public class RaceFinish : MonoBehaviour
{
    [SerializeField] private AwardPresenter awardPresenter;
    [SerializeField] private GameObject FinishCam;
    [SerializeField] private GameObject ViewModes;
    [SerializeField] private GameObject PosDisplay, PauseButton, Panel1, Panel2;
    [SerializeField] private GameObject FinishPanelWin, FinishPanelLose;

    public void Finish()
    {
        awardPresenter.GetAward();
        FinishCam.SetActive(true);
        PauseButton.SetActive(false);  
        Panel1.SetActive(false);     
        Panel2.SetActive(false);
        ViewModes.SetActive(false);

        //if you win (you finish 1st position)
        if (ChkManager.posMax == 1)
        {
            FinishPanelWin.SetActive(true);//win panel turns on
            FinishPanelLose.SetActive(false);//lose panel turns off
        }
        //you lose (not 1st position)
        else
        {
            FinishPanelWin.SetActive(false);//win panel turns off
            FinishPanelLose.SetActive(true);//lose panel turns on
            AudioListener.volume = 0f;//audio turns off
            Time.timeScale = 0;//time stops
        }
    }
}