using System.Collections;
using UnityEngine;

public class ShieldProjectile : MonoBehaviour
{
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
            if (value)
                isProtected++;
            else if (isProtected > 0)
                isProtected--;
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Activate()
    {
        StartCoroutine(ShieldTimer(lifeTime));
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
