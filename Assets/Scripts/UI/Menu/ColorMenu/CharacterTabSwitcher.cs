using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTabSwitcher : MonoBehaviour
{

    [SerializeField] private List<CharacterModelSwitcher> switchers = new List<CharacterModelSwitcher>();
    [SerializeField] private CharacterModelSwitcher currentSwitcher;

    public CharacterModelSwitcher CurrentSwitcher { get => currentSwitcher; set => currentSwitcher = value; }


    private void Start()
    {
        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].TabSwitcher = this;
        }
        currentSwitcher = switchers[0];
    }

    public void UpdateCharacterSwitchers()
    {
        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].LoadCharactersSO();
        }
    }
}
