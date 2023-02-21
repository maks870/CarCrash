using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopInput : BaseInput
{
    [SerializeField] private GameObject imageAbilityPref;

    public override event PressButtonAbility PressButtonAbilityEvent;

    public override void SetAbilities(List<AbilitySO> abilities)
    {
        ClearButton();

        for (int i = 0; i < abilities.Count; i++) 
        {
           GameObject imageAbility  = Instantiate(imageAbilityPref, container);
            if (i == 0) 
                imageAbility.GetComponentInChildren<Text>().text = "E";
            //if(i == 1)
            //    imageAbility.GetComponentInChildren<Text>().text = "X";
            //if (i == 2)
            //    imageAbility.GetComponentInChildren<Text>().text = "C";
            imageAbility.GetComponent<Image>().sprite = abilities[i].Icon;
        }
    }

    private void Update()
    {
        verticalAxis = Input.GetAxis("VerticalP2") + Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("HorizontalP2") + Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            handbrake = 1;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            handbrake = 0;
        }

       if (Input.GetKeyDown(KeyCode.E))
        {
            PressButtonAbilityEvent.Invoke(0);
        }
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    PressButtonAbilityEvent.Invoke(1);
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    PressButtonAbilityEvent.Invoke(2);
        //}
    }
}
