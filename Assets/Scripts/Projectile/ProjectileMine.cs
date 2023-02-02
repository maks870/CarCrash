using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMine : Projectile
{
    [SerializeField] private float blinkTime = 0.1f;
    [SerializeField] private float blinkDelay = 5f;
    [SerializeField] private GameObject mineLight;
    [HideInInspector] public List<AbilityController> warningCars = new List<AbilityController>();
    private bool lightOn = false;

    protected override void Start()
    {
        base.Start();
        rb.useGravity = true;
        SwitchLight();
    }

    private void SwitchLight()
    {
        //if (lightOn)
        //    meshRenderer.materials[1].color *= -10;
        //else
        //    meshRenderer.materials[1].color *= 0;
        mineLight.SetActive(lightOn);
        lightOn = !lightOn;
        StartCoroutine(SwitchTimer());
    }

    protected override void Destruct()
    {
        for (int i = 0; i < warningCars.Count; i++)
        {
            warningCars[i].IsMineWarning = false;
        }
        base.Destruct();
    }

    IEnumerator SwitchTimer()
    {
        if (lightOn)
            yield return new WaitForSeconds(blinkDelay);
        else
            yield return new WaitForSeconds(blinkTime);

        SwitchLight();
    }

}
