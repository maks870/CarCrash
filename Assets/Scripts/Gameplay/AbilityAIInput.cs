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

    [SerializeField] private AbilityController abilityController;

    private int complexity;
    private float abilityUseTimer;
    private float missleDesicionDistance;
    private GameObject currentTarget;
    private CarAIControl carAIControl;
    private List<YieldInstruction> abilityCorutines = new List<YieldInstruction>();

    public int Complexity { set => complexity = value; }
    public CarAIControl CarAIControl { set => carAIControl = value; }

    public delegate void AbilityUseDecision(int numberAbility);

    private void Start()
    {
        abilityController.RefreshAbilityEvent += SetAbilities;
        abilityUseTimer = Mathf.Lerp(maxAbilityUseTimer, minAbilityUseTimer, (complexity / 100f));
        missleDesicionDistance = Mathf.Lerp(maxMissleDistance, minMissleDistance, (complexity / 100f));
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

    private int? FindAvailableAbilityIndex(AbilityType abilityType, bool isConsiderCooldown)
    {
        int? abilityNumber = null;
        List<AbilitySO> abilities = abilityController.Abilities;

        for (int i = 0; i < abilities.Count; i++)
        {
            bool abilityCooldown = abilityCorutines[i] != null;
            bool cooldownÑondition = isConsiderCooldown ? !abilityCooldown : true;

            if (abilities[i].Type == abilityType && cooldownÑondition)
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

    private void MissleDesicion(GameObject target)//Ëîãèêà äëÿ ðàêåòû
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

    private void MineDesicion()//Ëîãèêà äëÿ ìèíû
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Mine, true);

        if (abilityIndex == null)
            return;
    }

    private void ShieldDesicion()//Ëîãèêà äëÿ ùèòà
    {
        int? abilityIndex = FindAvailableAbilityIndex(AbilityType.Shield, true);

        if (abilityIndex == null)
            return;
    }

    public void SetAbilities(List<AbilitySO> abilities)
    {
        for (int i = abilityCorutines.Count; i < abilities.Count; i++)
        {
            abilityCorutines.Add(null);
        }
    }

    IEnumerator AbilityUseTimer(int abilityNumber)
    {
        yield return new WaitForSeconds(abilityUseTimer);

        if (abilityCorutines.Count > abilityNumber)
            abilityCorutines[abilityNumber] = null;
    }
}
