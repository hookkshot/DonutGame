using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
    public static Game Instance;

    public static InputControl[] Players = new InputControl[2];

    public static int ActivePlayerCount()
    {
        int c = 0;
        for (int i = 0; i < Players.Length; i++)
            if (Players[i] != null)
                c++;
        return c;
    }

    public GameObject PlayerPrefab;
    public Transform StartPosition;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        



        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null && PlayerPrefab != null)
            {
                GameObject g = (GameObject)GameObject.Instantiate(PlayerPrefab, StartPosition.position, Quaternion.identity);
                CharacterController c = g.GetComponent<CharacterController>();

                c.PlayerNum = i;

                EnemyManager.Instance.Player = g;
            }
        }
    }
}
