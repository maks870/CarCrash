using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbilityController))]
public class AbilityAIInput : MonoBehaviour
{
    const float maxAbilityUseTimer = 3;
    const float minAbilityUseTimer = 1;


    [SerializeField] [Range(0, 100)] private int complexity;
    private float abilityUseTimer;
    private List<bool> abilityOnCooldown;
    private List<AbilityType> abilityUseType;
    private List<YieldInstruction> abilityCorutine;
    private GameObject currentTarget;
    private AbilityController abilityController;



    public delegate void AbilityUseDecision(int numberAbility);
    public event AbilityUseDecision UseDecisionEvent;

    private void Start()
    {
        abilityUseTimer = Mathf.Lerp(3, 1, complexity);
    }

    private void Update()
    {

    }

    private int? FindAvailableAbilityIndex(AbilityType abilityType, bool isConsiderCooldown)
    {
        bool cooldown—ondition;
        int? abilityNumber = null;

        for (int i = 0; i < abilityUseType.Count; i++)
        {
            cooldown—ondition = isConsiderCooldown ? !abilityOnCooldown[i] : true;

            if (abilityUseType[i] == abilityType && cooldown—ondition)
            {
                abilityNumber = i;
                return abilityNumber;
            }
        }
        return abilityNumber;
    }

    private bool DecisionPossibility()
    {
        int r = Random.Range(0, 100);
        return r < complexity
            ? true
            : false;
    }

    private bool FinalDecision(int abilityNumber)
    {
        if (DecisionPossibility())
        {
            if (abilityCorutine[abilityNumber] != null)
            {
                Coroutine coroutine = (Coroutine)abilityCorutine[abilityNumber];
                StopCoroutine(coroutine);
            }

            abilityUseType.RemoveAt(abilityNumber);
            abilityOnCooldown.RemoveAt(abilityNumber);
            abilityCorutine.RemoveAt(abilityNumber);

            UseDecisionEvent.Invoke(abilityNumber);
            return true;
        }
        else
        {
            Coroutine coroutine = StartCoroutine(AbilityUseTimer(abilityNumber));
            abilityCorutine[abilityNumber] = coroutine;
            return false;
        }
    }

    public void MissleDesicion(GameObject target)//ÀÓ„ËÍ‡ ‰Îˇ ‡ÍÂÚ˚
    {
        if (target == null)
        {
            currentTarget = null;
            return;
        }

        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Missle, false);

        if (abilityIndex == null)
            return;

        if (target == currentTarget)
        {
            abilityIndex = FindAvailableAbilityIndex(AbilityType.Missle, true);
            if (abilityIndex == null)
                return;
        }

        bool decision = FinalDecision((int)abilityIndex);

        if (decision)
            currentTarget = target;

    }

    public void MineDesicion()//ÀÓ„ËÍ‡ ‰Îˇ ÏËÌ˚
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Mine, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);
    }

    public void ShieldDesicion()//ÀÓ„ËÍ‡ ‰Îˇ ˘ËÚ‡
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Shield, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);

    }

    public void SetAbilities(List<AbilitySO> abilities)
    {
        for (int i = abilityUseType.Count; i < abilities.Count; i++)
        {
            abilityUseType.Add(abilities[i].Type);
            abilityOnCooldown.Add(false);
            abilityCorutine.Add(null);
        }
    }

    IEnumerator AbilityUseTimer(int abilityNumber)
    {
        abilityOnCooldown[abilityNumber] = true;
        yield return new WaitForSeconds(abilityUseTimer);
        abilityOnCooldown[abilityNumber] = false;
    }
}
