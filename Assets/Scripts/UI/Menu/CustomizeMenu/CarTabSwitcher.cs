using System.Collections.Generic;
using UnityEngine;

public class CarTabSwitcher : MonoBehaviour
{
    [SerializeField] private CarColorSwitcher carColorSwitcher;
    [SerializeField] private CarModelSwitcher carModelSwitcher;
    [SerializeField] private GameObject selectFrame;



    private ButtonCollectibleUI choosenButton;

    public bool HaveNewCollectibles => carModelSwitcher.HaveNewCollectibles;
    public CarColorSwitcher CarColorSwitcher { get => carColorSwitcher; set => carColorSwitcher = value; }
    public CarModelSwitcher CarModelSwitcher { get => carModelSwitcher; set => carModelSwitcher = value; }

    private void Awake()
    {
        List<CarColorSO> carColors = SOLoader.LoadSOByType<CarColorSO>();
        List<CarModelSO> carModels = SOLoader.LoadSOByType<CarModelSO>();

        carColorSwitcher.FillListBySO(carColors);
        carModelSwitcher.FillListBySO(carModels);
    }

    public void SwitchToCarColor()
    {
        carModelSwitcher.CarStatWindow.SetActive(false);
        carColorSwitcher.SelectCurrentButton();
    }

    public void SwitchToCarModel()
    {
        carColorSwitcher.PurchaseColor.HidePurchaseButton();
        carModelSwitcher.CarStatWindow.SetActive(true);
        carModelSwitcher.SelectCurrentButton();
    }

    public void SetSavedCar(CarColorSO carColor, CarModelSO carModel)
    {
        carColorSwitcher.InitializeUI();
        carModelSwitcher.InitializeUI();
        carModelSwitcher.SetCurrentModel(carModel);
        carColorSwitcher.SetCurrentColor(carColor);// устанавливаем цвет после установки модели из за рамки выбора
    }

    public void SelectButton(Transform buttonTransform)
    {
        selectFrame.transform.SetParent(buttonTransform);
        selectFrame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
