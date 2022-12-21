using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//------- Created by  : Hamza Herbou
//------- Email       : hamza95herbou@gmail.com

namespace EasyUI.Tabs
{
    public enum TabsType
    {
        Horizontal,
        Vertical
    }
    public abstract class TabsUI : MonoBehaviour
    {

        [System.Serializable] public class TabsUIEvent : UnityEvent<int> { }

        [SerializeField] private Transform buttons;
        [SerializeField] private Transform contents;

        [Header("Tabs customization :")]
        //[SerializeField] private Color themeColor = Color.gray ;
        [SerializeField] private float tabSpacing = 2f;
        [Space]
        [Header("OnTabChange event :")]
        public TabsUIEvent OnTabChange;

        private TabButtonUI[] tabBtns;
        private GameObject[] tabContent;

#if UNITY_EDITOR
        private LayoutGroup layoutGroup;
#endif

        private Sprite tabSpriteActive, tabSpriteInactive;
        private int current, previous;

        private int tabBtnsNum, tabContentNum;


        private void Start()
        {
            GetTabBtns();
        }

        private void GetTabBtns()
        {
            //buttons = transform.GetChild(0);
            //contents = transform.GetChild(1);
            tabBtnsNum = buttons.childCount;
            tabContentNum = contents.childCount;

            if (tabBtnsNum != tabContentNum)
            {
                Debug.LogError("!!Number of <b>[Buttons]</b> is not the same as <b>[Contents]</b> ("
                + tabBtnsNum + " buttons & " + tabContentNum
                + " Contents)");
                return;
            }

            tabBtns = new TabButtonUI[tabBtnsNum];
            tabContent = new GameObject[tabBtnsNum];
            for (int i = 0; i < tabBtnsNum; i++)
            {
                tabBtns[i] = buttons.GetChild(i).GetComponent<TabButtonUI>();
                int i_copy = i;
                tabBtns[i].uiButton.onClick.RemoveAllListeners();
                tabBtns[i].uiButton.onClick.AddListener(() => OnTabButtonClicked(i_copy));

                tabContent[i] = contents.GetChild(i).gameObject;
                tabContent[i].SetActive(false);
            }

            previous = current = 0;

            tabSpriteActive = tabBtns[0].uiImage.sprite;
            tabSpriteInactive = tabBtns[1].uiImage.sprite;

            tabBtns[0].uiButton.interactable = false;
            tabContent[0].SetActive(true);
        }

        public void OnTabButtonClicked(int tabIndex)
        {
            if (current != tabIndex)
            {
                ScrollRect scrollRect = tabContent[current].GetComponent<ScrollRect>();
                scrollRect.horizontalNormalizedPosition = 0;

                if (OnTabChange != null)
                    OnTabChange.Invoke(tabIndex);

                previous = current;
                current = tabIndex;

                tabContent[previous].SetActive(false);
                tabContent[current].SetActive(true);

                tabBtns[previous].uiImage.sprite = tabSpriteInactive;
                tabBtns[current].uiImage.sprite = tabSpriteActive;

                tabBtns[previous].uiButton.interactable = true;
                tabBtns[current].uiButton.interactable = false;
            }
        }


#if UNITY_EDITOR
        //public void UpdateThemeColor (Color color) {
        //   tabBtns [ 0 ].uiImage.color = color ;
        //   Color colorDark = DarkenColor (color, 0.3f) ;
        //   for (int i = 1; i < tabBtnsNum; i++)
        //      tabBtns [ i ].uiImage.color = colorDark ;

        //   parentContent.GetComponent <Image> ().color = color ;
        //}

        private Color DarkenColor(Color color, float amount)
        {
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);
            v = Mathf.Max(0f, v - amount);
            return Color.HSVToRGB(h, s, v);
        }

        public void Validate(TabsType type)
        {
            buttons = transform.GetChild(0);
            //contents = transform.GetChild(1);
            tabBtnsNum = buttons.childCount;
            tabContentNum = contents.childCount;

            tabBtns = new TabButtonUI[tabBtnsNum];
            tabContent = new GameObject[tabBtnsNum];

            for (int i = 0; i < tabBtnsNum; i++)
            {
                tabBtns[i] = buttons.GetChild(i).GetComponent<TabButtonUI>();
                tabContent[i] = contents.GetChild(i).gameObject;
            }

            //  UpdateThemeColor (themeColor) ;

            if (layoutGroup == null)
                layoutGroup = buttons.GetComponent<LayoutGroup>();

            if (type == TabsType.Horizontal)
                ((HorizontalLayoutGroup)layoutGroup).spacing = tabSpacing;
            else if (type == TabsType.Vertical)
                ((VerticalLayoutGroup)layoutGroup).spacing = tabSpacing;

        }
#endif
    }
}
