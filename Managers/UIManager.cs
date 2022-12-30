using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Menus")]
    public MenuPanel HUD;
    public MenuPanel MainMenu;
    public MenuPanel TargetOptions;
    public MenuPanel PlayerOptions;

    [Header("Menu Options")]
    public Dropdown WeaponDropDown;
    public Toggle ScopeToggle;
    public Slider BulletSpeedSlider;

    [Header("Target Options")]
    public Toggle CanWalkToggle;
    public Toggle CanRunToggle;
    public Toggle RandomSpawnToggle;
    public Slider CoverSlider;
    public Slider TwitchTimeSlider;

    [Header("Player Options")]
    public Toggle LoweredViewToggle;
    public Toggle HunterControlSchemeToggle;
    public Slider DownSensSlider;
    public Slider HipSensSlider;
    public Slider ADSSensSlider;

    public List<MenuPanel> PanelList = new List<MenuPanel>();

    public Text ControlText;

    private void Awake()
    {
        Instance = this;
        PopulateDropdown(WeaponDropDown, PropManager.Instance.Player.GetComponent<WeaponHandler>().Weapons);
    }
    private void Update()
    {
        //Pause the game, bring up the menus
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameManager.Instance.TogglePause())
                MainMenu.EnablePanel();
            else
                HUD.EnablePanel();            
        }
    }
    public void CloseMenus()
    {
        foreach (var item in PanelList)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void SetMainMenu()
    {
        //set weapon dropdown
        WeaponDropDown.value = PlayerManager.Instance.Player.GetComponent<WeaponHandler>().CurrentWeaponIndex; //.Instance.Player.GetComponent<WeaponHandler>().CurrentWeaponIndex;
        //set scope bool
        ScopeToggle.isOn = PlayerManager.Instance.Player.GetComponent<WeaponHandler>().UseScope;
        //set bullet speed
        BulletVelocitySetFix();//BulletSpeedSlider.value = PlayerManager.Instance.Player.GetComponent<WeaponHandler>().CurrentBase.CurrentProjectileVelocity;
    }
    public void SetTargetOptionsMenu()
    {
        //can walk
        CanWalkToggle.isOn = TargetManager.Instance.CanWalk;
        //can run
        CanRunToggle.isOn = TargetManager.Instance.CanRun;
        //random spawn
        RandomSpawnToggle.isOn = TargetManager.Instance.RandomSpawn;
        //cover count
        CoverSlider.value = PropManager.Instance.CoverCount;
        //twitch time
        TwitchTimeSlider.value = TargetManager.Instance.TwitchTime;
    }
    public void SetPlayerOptionsMenu()
    {
        //lowered view
        LoweredViewToggle.isOn = PlayerManager.Instance.Player.GetComponent<HuntView>().Lowered;
        //hunter control scheme
        HunterControlSchemeToggle.isOn = PlayerManager.Instance.Player.GetComponent<HunterAim>().UseGunslinger;
        //down sens
        DownSensSlider.value = PlayerManager.Instance.Player.GetComponent<SensitivityLevels>().DownSens;
        //hip sens
        HipSensSlider.value = PlayerManager.Instance.Player.GetComponent<SensitivityLevels>().HipSens;
        //ads sens
        ADSSensSlider.value = PlayerManager.Instance.Player.GetComponent<SensitivityLevels>().ADSSens;
    }

    private void PopulateDropdown(Dropdown dropdown, GameObject[] optionsArray)
    {
        List<string> options = new List<string>();
        foreach (var option in optionsArray)
        {
            options.Add(option.name);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    public void ControlTextChange(bool GunslingerControl)
    {
        ControlText.text = GunslingerControl ?
            "Right click ADS, Left click shoot, Shift sprint" :
            "Right click Hipfire, Right click + Shift ADS, Shift sprint";
    }
    public void BulletVelocitySetFix()
    {
        BulletSpeedSlider.value = PlayerManager.Instance.Player.GetComponent<WeaponHandler>().CurrentBase.CurrentProjectileVelocity;
    }
}
