using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AbilityController : MonoBehaviour
{
    private const float slowMultiplier = 0.9f;
    private const float slowTime = 1;

    [SerializeField] private float shieldLifeTime;
    [SerializeField] private int maxAbilities;
    [SerializeField] private Transform spawnPointForward;
    [SerializeField] private Transform spawnPointMiddle;
    [SerializeField] private Transform spawnPointBack;
    [SerializeField] private GameObject target;
    [SerializeField] private List<GameObject> possibleTargets = new List<GameObject>();
    private List<AbilitySO> abilities = new List<AbilitySO>();
    private bool isDamaged;
    private bool isProtected;
    private Animator shieldAnimator;

    public bool IsDamaged => isDamaged;
    public int MaxAbilities => maxAbilities;
    public GameObject Target { get => target; set => target = value; }

    public List<AbilitySO> Abilities => abilities;

    public delegate void AbilityListHandler(List<AbilitySO> abilitySO);
    public event AbilityListHandler RefreshAbilityEvent;

    private void Update()
    {
        FindNearstTarget();
    }

    private void GetInfoForAbility(AbilitySO ability, out Transform spawnPoint, out AbilityController targetCar)
    {
        targetCar = target != null
            ? target.GetComponentInChildren<AbilityController>()
            : null;

        spawnPoint = spawnPointForward.transform;

        switch (ability.Type)
        {
            case AbilityType.Missle:
                spawnPoint = spawnPointForward.transform;
                break;

            case AbilityType.Shield:
                targetCar = this;
                spawnPoint = spawnPointMiddle.transform;
                break;

            case AbilityType.Mine:
                spawnPoint = spawnPointBack.transform;
                break;
        }
    }

    private void FindNearstTarget()
    {
        if (possibleTargets.Count == 0)
        {
            target = null;
            return;
        }

        target = possibleTargets[0];

        for (int i = 0; i < possibleTargets.Count; i++)
        {
            float newDistance = Vector3.Distance(possibleTargets[0].transform.position, transform.position);
            float currentDistance = Vector3.Distance(target.transform.position, transform.position);

            if (newDistance < currentDistance)
                target = possibleTargets[i];
        }
    }

    public void AddTarget(GameObject target)
    {
        possibleTargets.Add(target);
        FindNearstTarget();
    }

    public void RemoveTarget(GameObject target)
    {
        possibleTargets.Remove(target);
        FindNearstTarget();
    }

    public void AddAbility(AbilitySO ability)// Добавление способности
    {
        if (abilities.Count < maxAbilities)
        {
            abilities.Add(ability);
            if (RefreshAbilityEvent != null)
                RefreshAbilityEvent.Invoke(abilities);
        }
    }

    public void UseAbility(int abilityPlace)// Использование способности
    {
        if (abilities.Count == 0 || abilityPlace >= abilities.Count)
            return;

        Transform spawnPoint;
        AbilityController targetCar;
        AbilitySO ability = abilities[abilityPlace];

        GetInfoForAbility(abilities[abilityPlace], out spawnPoint, out targetCar);

        ability.Use(spawnPoint, targetCar);
        abilities.Remove(ability);
        RefreshAbilityEvent.Invoke(abilities);
    }

    public void TakeDamage()// Логика получения урона
    {
        if (isProtected)
        {
            ShieldOff();
            return;
        }
        if (!isDamaged)
            StartCoroutine(DamagedTimer());
    }

    private void ShieldOn()
    {
        isProtected = true;
        /*        shieldAnimator.SetTrigger("");*/// TODO: Назначить триггер включения анимации щита
        Debug.Log("Shield Activated");
    }
    private void ShieldOff()
    {
        /*        shieldAnimator.SetTrigger("");*/// TODO: Назначить тригер отключения анимации щита
        isProtected = false;
        Debug.Log("Shield Deactivated");
    }

    public void ActivateShield()
    {
        StartCoroutine(ShieldTimer());
    }
    IEnumerator ShieldTimer()
    {
        ShieldOn();
        yield return new WaitForSeconds(shieldLifeTime);
        ShieldOff();
    }
    IEnumerator DamagedTimer()// Таймер получения урона
    {
        isDamaged = true;
        yield return new WaitForSeconds(slowTime);
        isDamaged = false;
    }
}
