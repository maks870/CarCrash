using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTabSwitcher : MonoBehaviour
{
    [SerializeField] private List<CharacterModelSwitcher> switchers = new List<CharacterModelSwitcher>();

    public void UpdateCharacterSwitchers()
    {
        for (int i = 0; i < switchers.Count; i++)
        {
            switchers[i].LoadCharactersSO();
        }
    }

}
