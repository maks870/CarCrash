using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    public abstract void Use(NewCarController car);

    public abstract void Use(NewCarController car, NewCarController target);
}


