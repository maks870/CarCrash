using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TESTSCRIPT : MonoBehaviour
{
    List<CollectibleSO> colList = new List<CollectibleSO>();
    public Text TEXT;
    private void OnEnable()
    {

    }

    void Start()
    {
        colList = SOLoader.LoadAllCollectibles();
        foreach (CollectibleSO col in colList)
        {
            TEXT.text += $"\n{col.name}";
        }

    }

}
