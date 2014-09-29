using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public const int LEVEL_MENU = 0;
    public const int LEVEL_GAME = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Application.LoadLevel(LEVEL_GAME);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
