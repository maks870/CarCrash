using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShield : Projectile
{
    protected override void Start()
    {
        base.Start();
        rb.useGravity = false;
        Debug.Log("Activate shield spawn effect"); 
/*        animator.SetTrigger("");*/ //TODO: Назначить тригер эффекта включения щита
    }
    public void EndAnimation()//TODO: Вызвать по окончании анимации
    {
        Destroy(gameObject);
    }
}
