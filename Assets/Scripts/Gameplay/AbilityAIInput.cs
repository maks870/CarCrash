using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AbilityAIInput : MonoBehaviour
{
    const float maxAbilityUseTimer = 3;
    const float minAbilityUseTimer = 1;

    const float maxMissleDistance = 7;
    const float minMissleDistance = 4;




    [SerializeField] [Range(0, 100)] private int complexity;
    [SerializeField] private AbilityController abilityController;

    private float abilityUseTimer;
    private float missleDesicionDistance;
    private GameObject currentTarget;
    private CarAIControl carAIControl;
    private List<AbilityType> abilityUseType;
    private List<YieldInstruction> abilityCorutines = new List<YieldInstruction>();

    public CarAIControl CarAIControl { set => carAIControl = value; }

    public delegate void AbilityUseDecision(int numberAbility);
    public event AbilityUseDecision UseDecisionEvent;

    private void Start()
    {
        abilityController.RefreshAbilityEvent += SetAbilities;
        abilityUseTimer = Mathf.Lerp(3, 1, (complexity / 100f));
        missleDesicionDistance = Mathf.Lerp(8, 5, (complexity / 100f));
        Debug.Log("Ability use timer = " + abilityUseTimer);
        Debug.Log("millseDesicionDistance = " + missleDesicionDistance);
    }

    private void Update()
    {
        if (abilityController.Abilities.Count > 0)
        {
            if (abilityController.IsMineWarning || carAIControl.GoToAbility || abilityController.IsMissleWarning)
                ShieldDesicion();

            MineDesicion();
            MissleDesicion(abilityController.Target);

        }
    }

    private int? FindAvailableAbilityIndex(AbilityType abilityType, bool isConsiderCooldown)//новый методж
    {
        int? abilityNumber = null;
        List<AbilitySO> abilities = abilityController.Abilities;

        for (int i = 0; i < abilities.Count; i++)
        {
            bool abilityCooldown = abilityCorutines[i] != null;
            bool cooldownСondition = isConsiderCooldown ? !abilityCooldown : true;

            if (abilities[i].Type == abilityType && cooldownСondition)
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
            if (abilityCorutines[abilityNumber] != null)
            {
                Coroutine coroutine = (Coroutine)abilityCorutines[abilityNumber];
                StopCoroutine(coroutine);
            }

            abilityCorutines.RemoveAt(abilityNumber);
            abilityController.UseAbility(abilityNumber);
            return true;
        }
        else
        {
            Coroutine coroutine = StartCoroutine(AbilityUseTimer(abilityNumber));
            abilityCorutines[abilityNumber] = coroutine;
            return false;
        }
    }

    private void MissleDesicion(GameObject target)//Логика для ракеты
    {
        if (target == null)
        {
            currentTarget = null;
            return;
        }

        Vector3 targetDist = target.transform.position - transform.position;
        Vector3 forwardDist = transform.forward;

        float currentForwardDistance = Vector3.Dot(forwardDist, targetDist);

        if (currentForwardDistance < missleDesicionDistance)
            return;

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

    private void MineDesicion()//Логика для мины
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Mine, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);
    }

    private void ShieldDesicion()//Логика для щита
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Shield, true);

        if (abilityIndex == null)
            return;

        bool decision = FinalDecision((int)abilityIndex);

    }

    public void SetAbilities(List<AbilitySO> abilities)//Новый
    {
        for (int i = abilityCorutines.Count; i < abilities.Count; i++)
        {
            abilityCorutines.Add(null);
        }
    }

    IEnumerator AbilityUseTimer(int abilityNumber)//новый
    {
        yield return new WaitForSeconds(abilityUseTimer);

        if (abilityCorutines.Count > abilityNumber)
            abilityCorutines[abilityNumber] = null;
    }
}
