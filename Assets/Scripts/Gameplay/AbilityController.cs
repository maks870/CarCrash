using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private float slowTime = 1;
    [SerializeField] private int maxAbilities;
    [SerializeField] private Transform spawnPointForward;
    [SerializeField] private Transform spawnPointMiddle;
    [SerializeField] private Transform spawnPointBack;
    [SerializeField] private ShieldProjectile shield;
    [SerializeField] private List<GameObject> possibleTargets = new List<GameObject>();
    [SerializeField] private List<AbilitySO> abilities = new List<AbilitySO>();

    private GameObject target;
    private bool isDamaged;
    private int haveTargetWeapon = 0;
    private int mineWarning = 0;
    private int missleWarning = 0;

    public bool HaveTargetWeapon
    {
        get
        {
            return haveTargetWeapon > 0 ? true : false;
        }
        set
        {
            if (value)
                haveTargetWeapon++;
            else
                haveTargetWeapon--;
        }
    }
    public bool IsMineWarning
    {
        get
        {
            return mineWarning > 0 ? true : false;
        }
        set
        {
            if (value)
                mineWarning++;
            else
                mineWarning--;
        }
    }
    public bool IsMissleWarning
    {
        get
        {
            return missleWarning > 0 ? true : false;
        }
        set
        {
            if (value)
                missleWarning++;
            else
                missleWarning--;
        }
    }

    public bool IsDamaged => isDamaged;
    public int MaxAbilities => maxAbilities;
    public GameObject Target => target;
    public ShieldProjectile Shield => shield;

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

        switch (ability.Type)
        {
            case AbilityType.Missle:
                spawnPoint = spawnPointForward.transform;
                break;

            case AbilityType.Shield:
                spawnPoint = spawnPointForward.transform;
                targetCar = this;
                break;

            case AbilityType.Mine:
                spawnPoint = spawnPointBack.transform;
                break;

            default:
                spawnPoint = spawnPointForward.transform;
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
            float newDistance = Vector3.Distance(possibleTargets[i].transform.position, transform.position);
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
            if (ability.Type == AbilityType.Missle)
                HaveTargetWeapon = true;

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

        if (ability.Type == AbilityType.Missle)
            HaveTargetWeapon = false;

        ability.Use(spawnPoint, targetCar);
        abilities.Remove(ability);
        RefreshAbilityEvent.Invoke(abilities);
    }

    public void TakeDamage()// Логика получения урона
    {
        if (shield.IsProtected)
        {
            shield.Deactivate();
        }
        else if (!isDamaged)
        {
            StartCoroutine(DamagedTimer());
        }
    }

    public void ActivateShield()
    {
        shield.Activate();
    }

    IEnumerator DamagedTimer()// Таймер получения урона
    {
        isDamaged = true;
        yield return new WaitForSeconds(slowTime);
        isDamaged = false;
    }
}
