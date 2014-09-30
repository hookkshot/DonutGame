using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    public RectTransform[] PlayerPanels;
    public GameObject[] PlayerHUD;

    public GameObject PlayerPrefab;

	void Awake()
    {

    }

    void Start()
    {
        PlayerHUD = new GameObject[PlayerPanels.Length];
        for(int i = 0; i < Game.ActivePlayerCount(); i++)
        {
            PlayerHUD[i] = (GameObject)GameObject.Instantiate(PlayerPrefab);
        }
    }

    public void UpdateHealth(int player, float h, float m)
    {
        //HealthText.text = (int)h + " / " + (int)m;
    }

}
