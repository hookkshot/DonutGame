using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    public const int LEVEL_MENU = 0;
    public const int LEVEL_GAME = 1;

    public Transform StartScreen;
    public Transform MainScreen;

    public Text[] PlayerTellInput;
    public Text[] PlayerControlType;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ControlType c = InputControl.GetInputType();

        //New control select scheme
        int player = Game.NextUnassignedPlayer();
        if(PlayerTellInput.Length > player && player >= 0)
        {
            if(c != ControlType.None)
            {
                Game.Players[player] = new InputControl(c);
            }
        }

        FixPlayers();

	}

    private void FixPlayers()
    {

        for (int i = 0; i < Game.Players.Length; i++)
        {
            bool playerActive = Game.Players[i] != null;
            PlayerTellInput[i].gameObject.SetActive(false);
            PlayerControlType[i].gameObject.SetActive(playerActive);
            if(playerActive)
                PlayerControlType[i].text = Game.Players[i].Type.ToString();
        }

        int player = Game.NextUnassignedPlayer();
        if(player != -1)
        {
            PlayerTellInput[player].gameObject.SetActive(true);
            PlayerControlType[player].gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        StartScreen.gameObject.SetActive(true);
        MainScreen.gameObject.SetActive(false);
    }

    public void StartScreenStart()
    {
        if (Game.Players[0] == null)
            return;
        Application.LoadLevel(LEVEL_GAME);
    }

    public void StartScreenBack()
    {
        StartScreen.gameObject.SetActive(false);
        MainScreen.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
