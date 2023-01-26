using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldProjectile : MonoBehaviour
{
    private Animator animator;
    private int isProtected = 0;
    private MeshRenderer meshRenderer;
    private float lifeTime;

    public float Lifetime { set => lifeTime = value; }
    public bool IsProtected
    {
        get
        {
            return isProtected > 0;
        }
        set
        {
            if (IsProtected)
                isProtected++;
            else if (isProtected > 0)
                isProtected--;
        }
    }

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
        IsProtected = false;
        if (isProtected == 0)
        {
            meshRenderer.enabled = false;
            animator.SetBool(0, false);
        }
    }

    private void ShieldOn()
    {
        meshRenderer.enabled = true;
        IsProtected = true;
    }

    IEnumerator ShieldTimer(float time)
    {
        ShieldOn();
        yield return new WaitForSeconds(time);
        ShieldOff();
    }
}
