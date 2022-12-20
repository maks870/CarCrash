using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCollectibleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject closedImage;
    [SerializeField] private Button button;
    [SerializeField] private ÑollectibleSO collectibleSO;

    public Image Image { get => image; set => image = value; }
    public GameObject ClosedImage { get => closedImage; }
    public Button Button { get => button; set => button = value; }
    public ÑollectibleSO CollectibleSO { get => collectibleSO; set => collectibleSO = value; }


}
