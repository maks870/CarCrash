using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInput : BaseInput
{
    [SerializeField] private GameObject buttonAbilityPref;

    public override float HorizontalAxis { get => horizontalAxis; }
    public override float VerticalAxis { get => verticalAxis; }

    public override event PressButtonAbility PressButtonAbilityEvent;

    public override void SetAbilities(List<AbilitySO> abilities)
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            GameObject buttonAbilityObj = Instantiate(buttonAbilityPref, container);
            Button buttonAbility = buttonAbilityObj.GetComponent<Button>();
            buttonAbility.image.sprite = abilities[i].Icon;
            buttonAbility.onClick.AddListener(() => PressButtonAbilityEvent.Invoke(i));
        }
    }

    private void Update()
    {
        //verticalAxis = Input.GetAxis("Vertical");
        //horizontalAxis = Input.GetAxis("Horizontal");  
    }
}
