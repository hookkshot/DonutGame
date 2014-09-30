using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputControl
{
    public ControlType Type = ControlType.Keyboard;

    private Dictionary<ControlButton, ControlKey> Buttons = new Dictionary<ControlButton, ControlKey>();

    public InputControl(ControlType t)
    {
        this.Type = t;
        switch (t)
        {
            case ControlType.Keyboard:
                Buttons.Add(ControlButton.Jump, new ControlKey(KeyCode.Space, true));
                Buttons.Add(ControlButton.Reload, new ControlKey(KeyCode.R, true));
                Buttons.Add(ControlButton.Shoot, new ControlKey(KeyCode.Mouse0, false));
                break;
            case ControlType.Controller1:
                Buttons.Add(ControlButton.Jump, new ControlKey(KeyCode.Joystick1Button0, true));
                Buttons.Add(ControlButton.Reload, new ControlKey(KeyCode.Joystick1Button3, true));
                Buttons.Add(ControlButton.Shoot, new ControlKey(KeyCode.Joystick1Button2, false));
                break;
            case ControlType.Controller2:
                Buttons.Add(ControlButton.Jump, new ControlKey(KeyCode.Joystick4Button0, true));
                Buttons.Add(ControlButton.Reload, new ControlKey(KeyCode.Joystick4Button3, true));
                Buttons.Add(ControlButton.Shoot, new ControlKey(KeyCode.Joystick4Button2, false));
                break;
            case ControlType.Controller3:
                Buttons.Add(ControlButton.Jump, new ControlKey(KeyCode.Joystick4Button0, true));
                Buttons.Add(ControlButton.Reload, new ControlKey(KeyCode.Joystick4Button3, true));
                Buttons.Add(ControlButton.Shoot, new ControlKey(KeyCode.Joystick4Button2, false));
                break;
            case ControlType.Controller4:
                Buttons.Add(ControlButton.Jump, new ControlKey(KeyCode.Joystick4Button0, true));
                Buttons.Add(ControlButton.Reload, new ControlKey(KeyCode.Joystick4Button3, true));
                Buttons.Add(ControlButton.Shoot, new ControlKey(KeyCode.Joystick4Button2, false));
                break;
        }
    }

    public bool GetButton(ControlButton c)
    {

        ControlKey k = null;
        if (Buttons.ContainsKey(c))
            k = Buttons[c];

        if(k != null)
        {
            if (k.Press)
                return Input.GetKeyDown(k.Key);
            else
                return Input.GetKey(k.Key);
        }

        return false;
    }

    public float GetAxis()
    {
        if(Type == ControlType.Keyboard)
        {
            return GetKeyboardAxis();
        }
        else
        {
            int c = 1;
            if (Type == ControlType.Controller1)
                c = 1;
            if (Type == ControlType.Controller2)
                c = 2;
            if (Type == ControlType.Controller3)
                c = 3;
            if (Type == ControlType.Controller4)
                c = 4;

            float a = Input.GetAxisRaw("XboxAxisXJoy" + c);
            return a;
        }
    }

    private static float GetKeyboardAxis()
    {
        float x = 0;
        if (Input.GetKey(KeyCode.A)) x -= 1;
        if (Input.GetKey(KeyCode.D)) x += 1;

        return x;
    }

    public static ControlType GetInputType()
    {
        ControlType c =  ControlType.None;
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            c = ControlType.Controller1;
        if (Input.GetKeyDown(KeyCode.Joystick2Button7))
            c = ControlType.Controller2;
        if (Input.GetKeyDown(KeyCode.Joystick3Button7))
            c = ControlType.Controller3;
        if (Input.GetKeyDown(KeyCode.Joystick4Button7))
            c = ControlType.Controller4;
        if (Input.GetKeyDown(KeyCode.Return))
            c = ControlType.Keyboard;
        return c;
    }
}

public class ControlKey
{
    public bool Press;
    public KeyCode Key;

    public ControlKey(KeyCode k, bool p)
    {
        Press = p;
        Key = k;
    }
}

public enum ControlType
{
    Keyboard,
    Controller1,
    Controller2,
    Controller3,
    Controller4,
    None
}

public enum ControlButton
{
    Jump,
    Shoot,
    ShootAlt,
    Reload,
}
