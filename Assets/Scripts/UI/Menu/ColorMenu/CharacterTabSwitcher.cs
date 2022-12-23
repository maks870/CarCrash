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
    }

    public void SetSavedCharacter(CharacterModelSO characterSO)
    {
        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].TabSwitcher = this;
            switchers[i].InitializeUI();

            if (switchers[i].FindStartCharacter(characterSO))
                switchers[i].SetCurrentCharacter(characterSO);

        }
    }
}
