using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public GameObject PlayerPrefab;
    public GameObject Player;
    public List<GameObject> ActivePlayerList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }
}
