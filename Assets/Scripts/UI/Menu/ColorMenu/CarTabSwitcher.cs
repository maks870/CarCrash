using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTabSwitcher : MonoBehaviour
{
    [SerializeField] private CarColorSwitcher carColorSwitcher;
    [SerializeField] private CarModelSwitcher carModelSwitcher;

    public CarColorSwitcher CarColorSwitcher { get => carColorSwitcher; set => carColorSwitcher = value; }
    public CarModelSwitcher CarModelSwitcher { get => carModelSwitcher; set => carModelSwitcher = value; }

    public void SwitchToCarColor()
    {
        carModelSwitcher.CarStatWindow.SetActive(false);
        carModelSwitcher.UpdateCarStatWindow((CarModelSO)carModelSwitcher.CurrentCarModel);
    }

    public void SwitchToCarModel()
    {
        carColorSwitcher.PurchaseColor.HidePurchaseButton();
        carModelSwitcher.CarStatWindow.SetActive(true);
    }

    public void UpdateCarSwitchers()
    {
        carColorSwitcher.LoadCarColorsSO();
        carModelSwitcher.LoadCarModelsSO();
    }

    public void SetSavedCar(CarColorSO carColor, CarModelSO carModel)
    {
        carColorSwitcher.SetCurrentColor(carColor);
        carModelSwitcher.SetCurrentModel(carModel);
    }
}
