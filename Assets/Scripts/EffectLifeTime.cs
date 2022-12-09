using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifeTime : MonoBehaviour
{
    [SerializeField] private float time;
    void Start()
    {
        StartCoroutine(TimerLife(time));
    }

    private IEnumerator TimerLife(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
