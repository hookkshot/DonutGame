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

    /// <summary>
    /// Checks teh game system whether their is already a specific control type being used by a player.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool ControlInUse(ControlType t)
    {
        for(int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null && Players[i].Type == t)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Unnasigns a player from being controlled
    /// </summary>
    /// <param name="i">Index of the player to be unnassigned.</param>
    public static void UnassignPlayer(int i)
    {
        Players[i] = null;
    }

    /// <summary>
    /// Finds the next player slot which has not been assigned to a player yet.
    /// </summary>
    /// <returns>Index of the player to be assigned.</returns>
    public static int NextUnassignedPlayer()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] == null)
                return i;
        }
        return -1;
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

[System.Serializable]
public class Profile
{
    public List<Level> Levels = new List<Level>();
    public List<Bonus> Bonuses = new List<Bonus>();

    public List<Bonus> GetBonuses(string level)
    {
        List<Bonus> list = new List<Bonus>();
        foreach(Bonus b in Bonuses)
        {
            if (b.Level == level)
                list.Add(b);
        }
        return list;
    }
}

[System.Serializable]
public class Bonus
{
    public string Level = "Level";
    public bool Aquuired = false;
}

[System.Serializable]
public class Level
{
    public string Name = "Level";
    public float BestTime = float.MaxValue;

}


