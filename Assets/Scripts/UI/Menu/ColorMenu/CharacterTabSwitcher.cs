using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTabSwitcher : MonoBehaviour
{
    [SerializeField] private List<CharacterModelSwitcher> switchers = new List<CharacterModelSwitcher>();
    private CharacterModelSwitcher currentSwitcher;

    public CharacterModelSwitcher CurrentSwitcher { get => currentSwitcher; set => currentSwitcher = value; }

    private void Awake()
    {
        List<CharacterModelSO> characters = CollectibleLoader.LoadCollectiblesByType<CharacterModelSO>();

        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].FillListBySO(characters);
        }
    }

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
