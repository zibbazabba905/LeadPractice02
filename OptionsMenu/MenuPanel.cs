using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuPanel : MonoBehaviour
{
    public UnityEvent InitializePanel;

    public void OnEnable()
    {
        InitializePanel.Invoke();
    }
    private void Start()
    {
        UIManager.Instance.PanelList.Add(this);
    }

    public void EnablePanel()
    {
        UIManager.Instance.CloseMenus();
        this.gameObject.SetActive(true);
    }
}
