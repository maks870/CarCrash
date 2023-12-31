using System.Collections.Generic;
using UnityEngine;

public class CharacterTabSwitcher : MonoBehaviour
{
    [SerializeField] private Transform startCurrentCharacterTransform;
    [SerializeField] private List<CharacterModelSwitcher> switchers = new List<CharacterModelSwitcher>();
    [SerializeField] private GameObject selectFrame;
    private CharacterModelSwitcher currentSwitcher;

    public CharacterModelSwitcher CurrentSwitcher { get => currentSwitcher; set => currentSwitcher = value; }
    public bool HaveNewCollectibles
    {
        get
        {
            foreach (CharacterModelSwitcher switcher in switchers)
            {
                if (switcher.HaveNewCollectibles)
                    return true;
            }

            return false;
        }
    }

    public void SubscribeSwitchers()
    {
        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].LoadSOSubscribe();
        }

        UpdateCurrentCharacter(startCurrentCharacterTransform);
    }

    public void SetSavedCharacter(CharacterModelSO characterSO)
    {
        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].InitializeUI();

            if (switchers[i].FindStartCharacter(characterSO))
            {
                switchers[i].SetCurrentCharacter(characterSO);
            }

        }
    }

    public void UpdateCurrentCharacter(Transform currentCharacter)
    {
        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].CurrentCharacterTransform = currentCharacter;
        }
    }

    public void SelectButton(Transform buttonTransform)
    {
        selectFrame.transform.SetParent(buttonTransform);
        selectFrame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

}
