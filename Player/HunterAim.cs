using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//ADD this is where to add the different scope zooms for now
//FIX convert this BACK to "states" I guess
public class HunterAim : MonoBehaviour
{
    public Camera FpsCam;
    public GameObject CenterX;
    public GameObject ScopeView;
    public GameObject Ironsight;

    private MouseLook MouseLook;
    private SensitivityLevels SensitivityLevels;
    private WeaponHandler WeaponHandler;
    private PlayerInput PInput;

    public enum AimState { Down, Hip, ADS }
    public AimState CurrentAimState { get; set; } = AimState.Down;

    public bool UseGunslinger { get; set; } = false;

    public bool CanShoot;

    public bool BeenSprinting;

    void Awake()
    {
        MouseLook = GetComponent<MouseLook>();
        SensitivityLevels = GetComponent<SensitivityLevels>();
        WeaponHandler = GetComponent<WeaponHandler>();
        PInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (PInput.GetSprintInput())
            BeenSprinting = true;

        if (!GameManager.Instance.Paused)

            if (UseGunslinger)
                GunslingerControl();
            else
                HunterControl();
    }

    //FIX move inputs to player inputs
    private void HunterControl()
    {
        if (!Input.GetMouseButton(1)) //right click
        {
            DownView();
            CurrentAimState = AimState.Down;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!BeenSprinting)
            {
                ADSView();
                CurrentAimState = AimState.ADS;
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
                BeenSprinting = false;
        }

        HipView();
        CurrentAimState = AimState.Hip;
    }

    public void GunslingerControl()
    {
        if (Input.GetMouseButton(1)) //right click
        {
            ADSView();
            CurrentAimState = AimState.ADS;
            return;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            DownView();
            CurrentAimState = AimState.Down;
            return;
        }

        HipView();
        CurrentAimState = AimState.Hip;
    }

    private void DownView()
    {
        FpsCam.fieldOfView = Camera.HorizontalToVerticalFieldOfView(90f, 1.777f); ;
        MouseLook.Sens = SensitivityLevels.GetDownSens();//SensitivityLevels.DownSens;
        CanShoot = false;
        ScopeView.SetActive(false);
        Ironsight.SetActive(false);
        CenterX.SetActive(false);
    }
    private void HipView()
    {
        FpsCam.fieldOfView = Camera.HorizontalToVerticalFieldOfView(75f, 1.777f);
        MouseLook.Sens = SensitivityLevels.GetHipSens();//SensitivityLevels.HipSens;
        CanShoot = true;
        ScopeView.SetActive(false);
        Ironsight.SetActive(false);
        CenterX.SetActive(true);
    }
    private void ADSView()
    {
        FpsCam.fieldOfView = Camera.HorizontalToVerticalFieldOfView(WeaponHandler.UseScope ? 10f : 50f, 1.777f);
        MouseLook.Sens = SensitivityLevels.GetADSSens(); // SensitivityLevels.ADSSens;
        CanShoot = true;

        if (WeaponHandler.UseScope)
        {
            ScopeView.SetActive(true);
            CenterX.SetActive(false);
            Ironsight.SetActive(false);
        }
        else
        {
            CenterX.SetActive(false);
            Ironsight.SetActive(true);
        }
    }
    public bool CanShootCheck()
    {
        return CanShoot;
    }
}

//fov notes
//down is 90
//hip is 75
//aim is 50
//sniper scope is 10
//sens

//down 2    1.6
//hip .72   1.3
//aim .44   0.65
