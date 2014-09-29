using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    public Text HealthText;

	void Awake()
    {

    }

    public void UpdateHealth(float h, float m)
    {
        HealthText.text = (int)h + " / " + (int)m;
    }
}
