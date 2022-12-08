using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAIInput : MonoBehaviour
{
    const float maxAbilityUseTimer = 3;
    const float minAbilityUseTimer = 1;


    [SerializeField] [Range(0, 100)] private int complexity;
    [SerializeField] private AbilityController abilityController;

    private float abilityUseTimer;
    //private List<bool> abilityOnCooldown;
    private List<AbilityType> abilityUseType;
    private List<YieldInstruction> abilityCorutine = new List<YieldInstruction>();
    private GameObject currentTarget;



    public delegate void AbilityUseDecision(int numberAbility);
    public event AbilityUseDecision UseDecisionEvent;

    private void Start()
    {
        abilityController.RefreshAbilityEvent += SetAbilities;
        abilityUseTimer = Mathf.Lerp(3, 1, complexity);
    }

    private void Update()
    {
        if (abilityController.Abilities.Count > 0)
        {
            if (abilityController.IsMissleWarning)
                ShieldDesicion();

            MissleDesicion(abilityController.Target);
            MineDesicion();
        }
    }

    private int? FindAvailableAbilityIndex(AbilityType abilityType, bool isConsiderCooldown)//íîâûé ìåòîäæ
    {
        int? abilityNumber = null;
        List<AbilitySO> abilities = abilityController.Abilities;

        for (int i = 0; i < abilities.Count; i++)
        {
            bool abilityCooldown = abilityCorutine[i] != null;
            bool cooldownÑondition = isConsiderCooldown ? !abilityCooldown : true;

            if (abilities[i].Type == abilityType && cooldownÑondition)
            {
                abilityNumber = i;
                return abilityNumber;
            }
        }
        return abilityNumber;
    }
    //private int? FindAvailableAbilityIndex(AbilityType abilityType, bool isConsiderCooldown)
    //{
    //    bool cooldownÑondition;
    //    int? abilityNumber = null;

    //    for (int i = 0; i < abilityUseType.Count; i++)
    //    {
    //        cooldownÑondition = isConsiderCooldown ? !abilityOnCooldown[i] : true;

    //        if (abilityUseType[i] == abilityType && cooldownÑondition)
    //        {
    //            abilityNumber = i;
    //            return abilityNumber;
    //        }
    //    }
    //    return abilityNumber;
    //}

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

            abilityCorutine.RemoveAt(abilityNumber);
            abilityController.UseAbility(abilityNumber);
            return true;
        }
        else
        {
            Coroutine coroutine = StartCoroutine(AbilityUseTimer(abilityNumber));
            abilityCorutine[abilityNumber] = coroutine;
            return false;
        }
    }
    //private bool FinalDecision(int abilityNumber)
    //{
    //    if (DecisionPossibility())
    //    {
    //        if (abilityCorutine[abilityNumber] != null)
    //        {
    //            Coroutine coroutine = (Coroutine)abilityCorutine[abilityNumber];
    //            StopCoroutine(coroutine);
    //        }

    //        abilityUseType.RemoveAt(abilityNumber);
    //        abilityOnCooldown.RemoveAt(abilityNumber);
    //        abilityCorutine.RemoveAt(abilityNumber);

    //        abilityController.UseAbility(abilityNumber);
    //        return true;
    //    }
    //    else
    //    {
    //        Coroutine coroutine = StartCoroutine(AbilityUseTimer(abilityNumber));
    //        abilityCorutine[abilityNumber] = coroutine;
    //        return false;
    //    }
    //}

    private void MissleDesicion(GameObject target)//Ëîãèêà äëÿ ğàêåòû
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

    private void MineDesicion()//Ëîãèêà äëÿ ìèíû
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Mine, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);
    }

    private void ShieldDesicion()//Ëîãèêà äëÿ ùèòà
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Shield, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);

    }

    public void SetAbilities(List<AbilitySO> abilities)//Íîâûé
    {
        for (int i = abilityCorutine.Count; i < abilities.Count; i++)
        {
            abilityCorutine.Add(null);
        }
    }

    //public void SetAbilities(List<AbilitySO> abilities)
    //{
    //    for (int i = abilityUseType.Count; i < abilities.Count; i++)
    //    {
    //        abilityUseType.Add(abilities[i].Type);
    //        abilityOnCooldown.Add(false);
    //        abilityCorutine.Add(null);
    //    }
    //}

    IEnumerator AbilityUseTimer(int abilityNumber)//íîâûé
    {
        yield return new WaitForSeconds(abilityUseTimer);
        abilityCorutine[abilityNumber] = null;
    }

    //IEnumerator AbilityUseTimer(int abilityNumber)
    //{
    //    abilityOnCooldown[abilityNumber] = true;
    //    yield return new WaitForSeconds(abilityUseTimer);
    //    abilityOnCooldown[abilityNumber] = false;
    //}
}
