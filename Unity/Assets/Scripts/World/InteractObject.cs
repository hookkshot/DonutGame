using UnityEngine;
using System.Collections;

public class InteractObject : MonoBehaviour {

    public string Text = "Interact";

    public delegate void InteractEvent(CharacterController player);

    public InteractEvent InteractThrough;

	public void Interact(CharacterController player)
    {
        if (InteractThrough != null)
        {
            InteractThrough(player);
        }
    }
}
