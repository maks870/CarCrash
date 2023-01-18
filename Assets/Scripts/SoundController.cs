using UnityEngine;
using UnityEngine.Audio;
using YG;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SoundChange(bool on)
    {
        int soundInt;

        if (on)
            soundInt = 0;
        else
            soundInt = -80;

        audioMixer.SetFloat("Master", soundInt);
        YandexGame.savesData.sound = on;
        YandexGame.SaveProgress();
    }

    public void Initialize()
    {
        SoundChange(YandexGame.savesData.sound);
    }
}
