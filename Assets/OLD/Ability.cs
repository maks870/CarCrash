using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public GameObject pref;
    public void Use(GameObject gameObject) 
    {
        Instantiate(pref, gameObject.transform);
        Debug.Log("Use Ability");
    }
}
