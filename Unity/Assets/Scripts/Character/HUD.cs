using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    public Text HealthText;

	void Awake()
    {

    }

    public void UpdateHealth(int h, int m)
    {
        HealthText.text = h + " / " + m;
    }
}
