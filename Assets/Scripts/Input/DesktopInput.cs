using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopInput : BaseInput
{
    [SerializeField] private GameObject imageAbilityPref;
    public override float HorizontalAxis { get => horizontalAxis; }
    public override float VerticalAxis { get => verticalAxis; }

    public override event PressButtonAbility PressButtonAbilityEvent;

    public override void SetAbilities(List<AbilitySO> abilities)
    {
        ClearButton();

        for (int i = 0; i < abilities.Count; i++) 
        {
           GameObject imageAbility  = Instantiate(imageAbilityPref, container);
            if (i == 0) 
                imageAbility.GetComponentInChildren<Text>().text = "Z";
            if(i == 1)
                imageAbility.GetComponentInChildren<Text>().text = "X";
            if (i == 2)
                imageAbility.GetComponentInChildren<Text>().text = "C";
            imageAbility.GetComponent<Image>().sprite = abilities[i].Icon;
        }
    }

    private void Update()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PressButtonAbilityEvent.Invoke(0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PressButtonAbilityEvent.Invoke(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            PressButtonAbilityEvent.Invoke(2);
        }
    }
}
