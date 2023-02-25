using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Button buttonSound;
    [SerializeField] private Sprite buttonOnSprite;
    [SerializeField] private Sprite buttonOffSprite;
    private float oldVolume = 4;

    private void Start()
    {
        music.Play();
        EnableSoundEffect(true);
        //audioMixer.GetFloat("Effect", out oldVolume);
    }

    private void SoundChange(bool on)
    {
        if (on)
        {
            buttonSound.image.sprite = buttonOnSprite;
            EnableSoundEffect(true);
            audioMixer.SetFloat("Master", -10);
        }
        else
        {
            buttonSound.image.sprite = buttonOffSprite;
            EnableSoundEffect(false);
            audioMixer.SetFloat("Master", -80);
        }
        buttonSound.onClick.RemoveAllListeners();
        buttonSound.onClick.AddListener(() => SoundChange(!on));

        YandexGame.savesData.sound = on;
        YandexGame.SaveProgress();

    }

    public void Initialize()
    {
        SoundChange(YandexGame.savesData.sound);
    }

    public void EnableSoundEffect(bool enable) 
    {
        if (!enable)
            audioMixer.SetFloat("Effect", -80);
        else
            audioMixer.SetFloat("Effect", oldVolume);
    }
}
