using UnityEngine;

public class coolDown : overworldInteractable
{
    public override void interact()
    {
        heatController.decreaseHeat();
    }
}
