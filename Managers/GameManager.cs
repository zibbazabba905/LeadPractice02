using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject Player;
    public UnityEvent<float> IntONE;
    public GameObject Menu;
    public GameObject Hud;
    //public bool InMenus = false;
    public bool Paused = false;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GameInit();
    }
    private void GameInit()
    {
        Cursor.lockState = Paused ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = false;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        //Screen.SetResolution(1920, 1080, true);
        ResetGame();
    }
    public void ResetGame()
    {
        IntONE.Invoke(1);
        //SetTarget.Invoke();
    }
    public bool TogglePause()
    {
        Paused = !Paused;
        Cursor.lockState = Paused ? CursorLockMode.Confined : CursorLockMode.Locked;
        Cursor.visible = Paused;
        Time.timeScale = Paused ? 0 : 1;
        return Paused;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
