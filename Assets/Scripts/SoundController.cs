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

    private void SoundChange(bool on)
    {
        if (on)
        {
            buttonSound.image.sprite = buttonOnSprite;
            audioMixer.SetFloat("Master", 0);
        }
        else
        {
            buttonSound.image.sprite = buttonOffSprite;
            audioMixer.SetFloat("Master", -80);
        }

        buttonSound.onClick.AddListener(() => SoundChange(!on));       
        YandexGame.savesData.sound = on;
        YandexGame.SaveProgress();
    }

    public void Initialize()
    {
        SoundChange(YandexGame.savesData.sound);
    }
}
