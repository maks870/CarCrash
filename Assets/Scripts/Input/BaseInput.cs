using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    float horizontalAxis;
    float verticalAxis;

    protected abstract int PressFire();
}
