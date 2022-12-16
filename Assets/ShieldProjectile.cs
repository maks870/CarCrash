using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectile : MonoBehaviour
{
    private Animator animator;
    private bool isProtected;
    private MeshRenderer meshRenderer;
    [HideInInspector] public float lifeTime;

    public bool IsProtected => isProtected;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void Activate()
    {
        StartCoroutine(ShieldTimer(lifeTime));
        animator.SetBool(0, true);
    }
    public void Deactivate()
    {
        ShieldOff();
    }
    private void ShieldOff()
    {
        meshRenderer.enabled = false;
        isProtected = false;
        animator.SetBool(0, false);
    }

    private void ShieldOn()
    {
        meshRenderer.enabled = true;
        isProtected = true;
    }

    IEnumerator ShieldTimer(float time)
    {
        if (isProtected)
            ShieldOff();

        ShieldOn();
        yield return new WaitForSeconds(time);
        ShieldOff();
    }
}
