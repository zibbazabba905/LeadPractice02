using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(WeaponBase))]
public class Gun3 : MonoBehaviour, IWeaponSplit
{
    private WeaponBase WBase;
    private void Awake()
    {
        WBase = GetComponent<WeaponBase>();
    }
    public void AttackStart()
    {
        //shoot gun
        WBase.ShootGun();
    }
    public void AttackHold()
    {
        //Aim Thing
    }
    public void AttackEnd()
    {
        //DoReloadEventually
    }
}
