using Unity.VisualScripting;
using UnityEngine;

public class hitboxSlam : overworldInteractable
{
    public tentacle t;

    public override void interact()
    {
        if (t.active && t.alive && t.combatable)
        {
            t.interactable = false;
            StartCoroutine(tentacle.controller.enterCombat(t));
        }
    }
}
