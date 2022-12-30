using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(WeaponBase))]
public class Bow3 : MonoBehaviour, IWeaponSplit
{
    private WeaponBase WBase;

    private float maxChargeTime = 1;
    private float currentChargeTime;

    private void Awake()
    {
        WBase = GetComponent<WeaponBase>();
    }
    public void AttackStart()
    {
        //do nothing
    }
    public void AttackHold()
    {
        ChargeBow();
        //charge bow
    }
    public void AttackEnd()
    {
        WBase.ShootGun();
        currentChargeTime = 0f;
        //shoot bow
    }

    private void ChargeBow()
    {
        if (currentChargeTime < maxChargeTime)
        {
            WBase.ChargePercent = Mathf.Lerp(0, 1, currentChargeTime);
            currentChargeTime += Time.deltaTime;
        }
        WBase.CurrentProjectileVelocity = 50f + (100 * WBase.ChargePercent);
    }
}
