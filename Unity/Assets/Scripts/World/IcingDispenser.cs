using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InteractObject))]
public class IcingDispenser : MonoBehaviour {
    public PowerType Type = PowerType.Speed;

    void Awake()
    {
        GetComponent<InteractObject>().InteractThrough += Interact;
    }

    void Interact(CharacterController player)
    {
        player.SetPower(Type);
    }
}
