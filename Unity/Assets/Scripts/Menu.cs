using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    public const int LEVEL_MENU = 0;
    public const int LEVEL_GAME = 1;

    public Transform StartScreen;
    public Transform MainScreen;

    public Button Player2Select;

    public Toggle Player2Toggle;

    public Text Player1ControlType;
    public Text Player2ControlType;

    public Text Player1TellInput;
    public Text Player2TellInput;

    private bool selectPlayer1 = false;
    private bool selectPlayer2 = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ControlType c = InputControl.GetInputType();

        if (c == ControlType.None)
            return;
        if (selectPlayer1)
        {
            Debug.Log("Control " + c.ToString());
            Game.Players[0] = new InputControl(c);
            selectPlayer1 = false;

            Player1ControlType.text = c.ToString();
            Player1TellInput.gameObject.SetActive(false);
        }

        if (selectPlayer2)
        {

            Game.Players[1] = new InputControl(c);
            selectPlayer2 = false;

            Player2ControlType.text = c.ToString();
            Player2TellInput.gameObject.SetActive(false);
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
            Game.Players[0] = new InputControl(ControlType.Keyboard);
        Application.LoadLevel(LEVEL_GAME);
    }

    public void StartScreenBack()
    {
        StartScreen.gameObject.SetActive(false);
        MainScreen.gameObject.SetActive(true);
    }

    public void SetInputP1()
    {
        selectPlayer1 = true;
        selectPlayer2 = false;

        Player1TellInput.gameObject.SetActive(true);
    }

    public void SetInputP2()
    {
        selectPlayer2 = true;
        selectPlayer1 = false;

        Player2TellInput.gameObject.SetActive(true);
    }

    public void ToggleP2(bool s)
    {
        bool val = Player2Toggle.isOn;
        Player2Select.gameObject.SetActive(val);
        Player2ControlType.gameObject.SetActive(val);
        if (!val)
            Game.Players[1] = null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
