using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMapUI : MonoBehaviour
{
    [SerializeField] private Text numberLvl;
    [SerializeField] private Text fastestTime;
    [SerializeField] private Image image;
    [SerializeField] private Image closedImage;
    [SerializeField] private Image cupImage;
    [SerializeField] private Button button;
    [SerializeField] private List<Sprite> cupSprites = new List<Sprite>();
    private MapSO mapSO;

    public Text NumberLvl { get => numberLvl; }
    public Text FastestTime { get => fastestTime; set => fastestTime = value; }
    public Image Image { get => image; }
    public Image ClosedImage { get => closedImage; }
    public Image CupImage { get => cupImage; }
    public Button Button { get => button; set => button = value; }
    public List<Sprite> CupSprites { get => cupSprites; }
    public MapSO MapSO { get => mapSO; set => mapSO = value; }
}
