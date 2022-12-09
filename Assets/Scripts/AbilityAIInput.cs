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

            if (abilityController.IsMineWarning)
                ShieldDesicion();

            MineDesicion();
            MissleDesicion(abilityController.Target);

        }
    }

    private int? FindAvailableAbilityIndex(AbilityType abilityType, bool isConsiderCooldown)//����� ������
    {
        int? abilityNumber = null;
        List<AbilitySO> abilities = abilityController.Abilities;

        for (int i = 0; i < abilities.Count; i++)
        {
            bool abilityCooldown = abilityCorutine[i] != null;
            bool cooldown�ondition = isConsiderCooldown ? !abilityCooldown : true;

            if (abilities[i].Type == abilityType && cooldown�ondition)
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

    private void MissleDesicion(GameObject target)//������ ��� ������
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

    private void MineDesicion()//������ ��� ����
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Mine, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);
    }

    private void ShieldDesicion()//������ ��� ����
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Shield, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);

    }

    public void SetAbilities(List<AbilitySO> abilities)//�����
    {
        for (int i = abilityCorutine.Count; i < abilities.Count; i++)
        {
            abilityCorutine.Add(null);
        }
    }

    IEnumerator AbilityUseTimer(int abilityNumber)//�����
    {
        yield return new WaitForSeconds(abilityUseTimer);
        abilityCorutine[abilityNumber] = null;
    }
}
