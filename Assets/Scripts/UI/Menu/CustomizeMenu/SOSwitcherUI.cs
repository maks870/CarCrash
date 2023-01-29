using System.Collections.Generic;
using UnityEngine;

public class SOSwitcherUI : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private List<ButtonCollectibleUI> buttons = new List<ButtonCollectibleUI>();
    [SerializeField] private List<CollectibleSO> carModelsSO = new List<CollectibleSO>();
}
