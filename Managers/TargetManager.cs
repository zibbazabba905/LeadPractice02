using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance { get; private set; }

    public GameObject TargetPrefab;
    public List<TargetScript> ActiveTargetsList = new List<TargetScript>();
    public int MaxTargetCount { get; set; } = 1;

    //target options
    public bool CanWalk { get; set; } = true;
    public bool CanRun { get; set; } = true;
    public bool RandomSpawn { get; set; } = true;
    public float TwitchTime { get; set; } = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (ActiveTargetsList.Count < MaxTargetCount)
        {
            SpawnTarget(GetSpawnPoint());
        }
    }

    public void SpawnTarget(Vector3 SpawnPosition)
    {
        GameObject NewTarget = Instantiate(TargetPrefab, SpawnPosition, transform.rotation);
        ActiveTargetsList.Add(NewTarget.GetComponent<TargetScript>());
    }
    private Vector3 GetSpawnPoint()
    {
        float x = Random.Range(-PropManager.Instance.RangeHalfWidth, PropManager.Instance.RangeHalfWidth);
        float y = 0f;
        float z = PropManager.Instance.ActiveRow;
        return new Vector3(x, y, z);
    }

    public void ResetTargets()
    {
        foreach (var target in ActiveTargetsList)
        {
            Destroy(target.gameObject);
        }
        ActiveTargetsList.Clear();
    }
}

