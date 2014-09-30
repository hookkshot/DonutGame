using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    public static HUD Instance;

    public Slider[] PlayerSliders;
    public RectTransform[] PlayerHUD;

    public Text TimerText;
    private float start;

	void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for(int i = 0; i < Game.ActivePlayerCount(); i++)
        {
            PlayerHUD[i].gameObject.SetActive(true);
        }

        
    }

    void Update()
    {
        TimerText.text = (Time.time - start).ToString();
    }

    public void UpdateHealth(int player, float h, float m)
    {
        float p = ((h/m)*100);
        PlayerSliders[player].value = p;

        
    }

}
