using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheadLightsSwitcher : MonoBehaviour
{
    [SerializeField] private int matIndexInRenderer;
    [SerializeField] private Material redMat;
    [SerializeField] private Material yellowMat;
    [SerializeField] private Material greenMat;

    private int counter = 0;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SwitchLight()
    {
        switch (counter)
        {
            case 0:
                meshRenderer.materials[matIndexInRenderer].color = redMat.color;
                break;
            case 1:
                meshRenderer.materials[matIndexInRenderer].color = yellowMat.color;
                break;
            case 2:
                meshRenderer.materials[matIndexInRenderer].color = greenMat.color;
                break;
        }
        counter++;
    }
}
