using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class AbilityController : MonoBehaviour
{
    private const float slowMultiplier = 0.9f;
    private const float slowTime = 1;

    [SerializeField] private float shieldLifeTime;
    [SerializeField] private Transform spawnPointForward;
    [SerializeField] private Transform spawnPointMiddle;
    [SerializeField] private Transform spawnPointBack;
    [SerializeField] private GameObject target;
    private List<AbilitySO> abilities = new List<AbilitySO>();
    private bool isDamaged;
    private bool isProtected;
    private Animator shieldAnimator;

    public bool IsDamaged => isDamaged;

    public List<AbilitySO> Abilities { get => abilities; }

    public delegate void AbilityListHandler(List<AbilitySO> abilitySO);
    public event AbilityListHandler RefreshAbilityEvent;

    public void AddAbility(AbilitySO ability)// ���������� �����������
    {
        abilities.Add(ability);
        if (RefreshAbilityEvent != null)
            RefreshAbilityEvent.Invoke(abilities);
    }

    public void UseAbility(int abilityPlace)// ������������� �����������
    {
        AbilitySO ability = abilities[abilityPlace];
        Vector3 spawnPoint = Vector3.zero;
        Debug.Log(target.name);
        AbilityController targetCar = target.GetComponentInChildren<AbilityController>();

        switch (ability.Type)
        {
            case AbilityType.Missle:
                spawnPoint = spawnPointForward.position;
                break;

            case AbilityType.Shield:
                targetCar = this;
                spawnPoint = spawnPointMiddle.position;
                break;

            case AbilityType.Mine:
                spawnPoint = spawnPointBack.position;
                break;
        }

        ability.Use(spawnPoint, targetCar);
        abilities.Remove(ability);
        RefreshAbilityEvent.Invoke(abilities);
    }

    public void TakeDamage()// ������ ��������� �����
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
        shieldAnimator.SetTrigger("");// TODO: ��������� ������� ��������� �������� ����
        Debug.Log("Shield Activated");
    }
    private void ShieldOff()
    {
        shieldAnimator.SetTrigger("");// TODO: ��������� ������ ���������� �������� ����
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
    IEnumerator DamagedTimer()// ������ ��������� �����
    {
        isDamaged = true;
        yield return new WaitForSeconds(slowTime);
        isDamaged = false;
    }
}
