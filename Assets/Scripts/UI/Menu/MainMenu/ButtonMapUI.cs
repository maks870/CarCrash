using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMapUI : MonoBehaviour
{
    [SerializeField] private Text fastestTime;
    [SerializeField] private GameObject bestTimeObj;
    [SerializeField] private Text bestPlace;
    [SerializeField] private Image image;
    [SerializeField] private Image closedImage;
    [SerializeField] private Image cupImage;
    [SerializeField] private Button button;
    [SerializeField] private List<Sprite> cupSprites = new List<Sprite>();
    private MapSO mapSO;

    public Text FastestTime { get => fastestTime; }
    public Text BestPlace { get => bestPlace; }
    public Image Image { get => image; }
    public Image ClosedImage { get => closedImage; }
    public Image CupImage { get => cupImage; }
    public Button Button { get => button; }
    public List<Sprite> CupSprites { get => cupSprites; }
    public MapSO MapSO { get => mapSO; set => mapSO = value; }
    public GameObject BestTimeObj { get => bestTimeObj; }
}
