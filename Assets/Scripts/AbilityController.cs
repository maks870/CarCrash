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
    private List<AbilitySO> abilities = new List<AbilitySO>();
    private bool isDamaged;
    private bool isProtected;
    private Animator shieldAnimator;

    public bool IsDamaged => isDamaged;
    public int MaxAbilities => maxAbilities;

    public List<AbilitySO> Abilities => abilities;

    public delegate void AbilityListHandler(List<AbilitySO> abilitySO);
    public event AbilityListHandler RefreshAbilityEvent;

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
        if (abilities.Count == 0)
            return;

        if (abilityPlace >= abilities.Count)
            return;

        AbilitySO ability = abilities[abilityPlace];
        Transform spawnPoint = spawnPointForward.transform;
        AbilityController targetCar;
        if (target != null)
            targetCar = target.GetComponentInChildren<AbilityController>();
        else
            targetCar = null;

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
