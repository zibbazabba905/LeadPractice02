using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PropManager : MonoBehaviour
{
    public static PropManager Instance { get; private set; }
    public GameObject CoverPrefab;
    public GameObject TargetPrefab;
    public GameObject Player;

    public UnityEvent CoverCountResetEvent;
    public UnityEvent ResetTarget;

    public float RangeHalfWidth { get; set; }
    public int ActiveRow { get; set; } = 10;


    public int CoverCount { get; set; }
    public float CoverPosition { get; set; }

    public List<GameObject> ActiveCoverPanels = new List<GameObject>();


    public void Awake()
    {
        Instance = this;
        TestFilling();
    }

    private void TestFilling()
    {
        RangeHalfWidth = 10;
    }

    public void InitCover(int count)
    {
        //Remove previous walls
        CoverCount = count;
        foreach (GameObject wall in ActiveCoverPanels)
        {
            Destroy(wall);
        }
        ActiveCoverPanels.Clear();
        //spawn new walls
        for (int i = 1; i < CoverCount+1; i++)
        {
            float x = CenterJustifiedCover(i);
            float y = 0f;
            float z = ActiveRow;

            GameObject WallClone = Instantiate(CoverPrefab, new Vector3(x,y,z), transform.rotation);
            ActiveCoverPanels.Add(WallClone);
        }
    }

    private float CenterJustifiedCover(int WhichCover)
    {
        //space walls evenly, probably better math for this, but it's only 3 walls
        float widthPercent = (float)WhichCover / ((float)CoverCount + 1f);
        return Mathf.Lerp(-RangeHalfWidth, RangeHalfWidth, widthPercent);
    }

    public void IncreaseDistance()
    {
        if (ActiveRow < 200)
            ActiveRow += 10;
        TargetManager.Instance.ResetTargets();
        InitCover(CoverCount);
    }
    public void DecreaseDistance()
    {
        if (ActiveRow > 10)
            ActiveRow -= 10;
        TargetManager.Instance.ResetTargets();
        InitCover(CoverCount);
    }
    public void SetCoverCount(float value)
    {
        CoverCount = (int)value;
        InitCover((int)value);
    }

    public Vector3 RandomPositionActiveRow()
    {
        float x = Random.Range(-RangeHalfWidth, RangeHalfWidth);
        float y = 0f; //Figure out this, height
        float z = ActiveRow;

        return new Vector3(x, y, z);
    }
    public Vector3 GetEdgeActiveRow(string words)
    {
        float x = (words == "Left")? -RangeHalfWidth : RangeHalfWidth;
        float y = 0f; //Figure out this, height
        float z = ActiveRow;
        return new Vector3(x, y, z);
    }

}

