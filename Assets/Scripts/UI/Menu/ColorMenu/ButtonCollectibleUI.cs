using UnityEngine;
using UnityEngine.UI;

public class ButtonCollectibleUI : MonoBehaviour//типа структура
{
    [SerializeField] private Image image;
    [SerializeField] private GameObject closedImage;
    [SerializeField] private Button button;
    private CollectibleSO collectibleSO;

    public Image Image { get => image; set => image = value; }
    public GameObject ClosedImage { get => closedImage; }
    public Button Button { get => button; set => button = value; }
    public CollectibleSO CollectibleSO { get => collectibleSO; set => collectibleSO = value; }


}
