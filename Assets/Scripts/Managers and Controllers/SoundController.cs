using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Button buttonSound;
    [SerializeField] private Sprite buttonOnSprite;
    [SerializeField] private Sprite buttonOffSprite;

    public AudioMixer AudioMixer { get => audioMixer;}

    private void SoundChange(bool on)
    {
        if (on)
        {
            buttonSound.image.sprite = buttonOnSprite;
            AudioMixer.SetFloat("Master", -10);
        }
        else
        {
            buttonSound.image.sprite = buttonOffSprite;
            AudioMixer.SetFloat("Master", -80);
        }
        buttonSound.onClick.RemoveAllListeners();
        buttonSound.onClick.AddListener(() => SoundChange(!on));

        YandexGame.savesData.sound = on;
        YandexGame.SaveProgress();

    }

    //public void SoundDistortion(bool on)
    //{
    //    if (on)
    //        distortionSnapshot.TransitionTo(0.3f);
    //    else
    //        normalSnapshot.TransitionTo(0.3f);
    //}

    public void Initialize()
    {
        SoundChange(YandexGame.savesData.sound);
    }
}
