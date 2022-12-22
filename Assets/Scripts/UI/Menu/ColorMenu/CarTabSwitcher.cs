using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTabSwitcher : MonoBehaviour
{
    [SerializeField] private CarColorSwitcher carColorSwitcher;
    [SerializeField] private CarModelSwitcher carModelSwitcher;

    public CarColorSwitcher CarColorSwitcher { get => carColorSwitcher; set => carColorSwitcher = value; }
    public CarModelSwitcher CarModelSwitcher { get => carModelSwitcher; set => carModelSwitcher = value; }

    public void CarColorSwitch()
    {

    }

    public void CarModelSwitch()
    { 
    
    }
    public void UpdateCarSwitchers()
    {
        carColorSwitcher.LoadCarColorsSO();
        carModelSwitcher.LoadCarModelsSO();
    }
}
