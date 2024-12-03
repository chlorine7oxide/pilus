using UnityEngine;

public class heatUp : overworldInteractable
{
    public override void interact()
    {
        heatController.increaseHeat();
    }
}
