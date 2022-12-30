using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviour
{
    public string ID;
    public string Name;
    public GameObject Owner;
    public float DefaultVelocity;
    public float ShotDelay;
    public bool IsBow;

    public Texture IronsightsImage;
    public GameObject Projectile;

    private GameObject ProjectileSpawn;
    public Text ChargeIndicatorFix;

    public float ChargePercent { get; set; }
    public float CurrentProjectileVelocity { get; set; }
    public float LastAttackTime { get; set; }
    public float ShotDelayEndTime { get; set; }
    private bool lateInputHappened;

    private IWeaponSplit WeaponMain;
    private WeaponHandler WH;

    private void Awake()
    {
        //"weapon" currently on hud instead of 3d game object
        Owner = transform.parent.gameObject;// GetComponentInParent<GameObject>();// GameObject.Find("Player");
        ProjectileSpawn = Owner.GetComponent<Hud>().BulletSpawn;
        WeaponMain = GetComponent<IWeaponSplit>();
        WH = Owner.GetComponent<WeaponHandler>();
        ChargeIndicatorFix = GameObject.Find("ChargeFix").GetComponent<Text>();
        LastAttackTime = -1;
        CurrentProjectileVelocity = DefaultVelocity;
    }

    private void Update()
    {
        //set charge indicator
        if (IsBow)
            ChargeIndicatorFix.text = (ChargePercent == 0) ? " " : Mathf.Round(ChargePercent * 100).ToString();
        else
            ChargeIndicatorFix.text = !RunningDelayTimer() ? " " : Mathf.Round(DelayPercent() * 100).ToString();

        //check for delay
        if (RunningDelayTimer() || !WH.CanShoot())
        {
            return;//if Delay or can't shoot, do nothing
        }
        //check inputs
        if (InputStart())
        {
            //do input start
            WeaponMain.AttackStart();
        }
        if (InputEnd())
        {
            //do input end, Reset ShotDelay
            WeaponMain.AttackEnd();
            ShotDelayEndTime = Time.time + ShotDelay;
        }
        if (InputContinue())
        {
            //do input continue
            WeaponMain.AttackHold();
        }

        lateInputHappened = WH.GetAttackInput();
    }

    private bool RunningDelayTimer()
    {
        return (ShotDelayEndTime > Time.time);
    }
    private float DelayPercent()
    {
        return (ShotDelayEndTime - Time.time);

    }

    private bool InputStart()
    {
        return InputContinue() && !lateInputHappened;
    }
    private bool InputEnd()
    {
        return !InputContinue() && lateInputHappened;
    }
    private bool InputContinue()
    {
        return (WH.GetAttackInput());
    }
    public void ShootGun()
    {
        Vector3 StartPoint = ProjectileSpawn.transform.position;
        Vector3 ShotDirection = WH.CastHitPoint() - StartPoint;
        //Instantiate bullet
        GameObject currentBullet = Instantiate(Projectile, StartPoint, Quaternion.identity);
        //rotate bullet to shoot direction (do with instantiate instead?)
        currentBullet.transform.forward = ShotDirection.normalized;
        //set future point for raycasting Nope, tell bullet its speed let it do that shit itself;
        currentBullet.GetComponent<Projectile3>().BulletVelocity = CurrentProjectileVelocity;
    }
}
