using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private PlayerInput PlayerInput;
    private HunterAim HunterAim;
    private Hud Hud;
    public GameObject WeaponSpot;

    public GameObject BulletObject;
    public GameObject ArrowObject;

    public GameObject[] Weapons;
    public GameObject CurrentWeapon { get; set; }
    public int CurrentWeaponIndex { get; set; }
    public WeaponBase CurrentBase;

    public bool UseScope { get; set; }


    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        Hud = GetComponent<Hud>();
        HunterAim = GetComponent<HunterAim>();
        SetGun(0);
    }

    public void SetGun(int index)
    {
        if (CurrentWeapon != null)
            Destroy(CurrentWeapon.gameObject);
        CurrentWeapon = Instantiate(Weapons[index],transform);
        CurrentWeaponIndex = index;        
        CurrentBase = CurrentWeapon.GetComponent<WeaponBase>();
        InitWeapon(CurrentBase);
    }

    private void InitWeapon(WeaponBase newWeapon)
    {
        Hud.IronsightsHudElement.texture = newWeapon.IronsightsImage;
    }

    public Vector3 CastHitPoint()
    {
        Ray ray = Hud.FPSCam.ViewportPointToRay(Hud.AimPointCalc());
        RaycastHit hit;

        //check for hit
        return (Physics.Raycast(ray, out hit)) ? hit.point : ray.GetPoint(300);
    }
    public void SetWeaponVelocity(float value)
    {
        CurrentBase.CurrentProjectileVelocity = value;
    }

    public bool GetAttackInput()
    {
        return PlayerInput.GetAttackInputHeld();
    }

    public bool CanShoot()
    {
        return HunterAim.CanShootCheck();
    }
}
