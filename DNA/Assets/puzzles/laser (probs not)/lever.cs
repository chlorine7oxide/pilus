using UnityEngine;

public class lever : overworldInteractable
{
    public GameObject source;

    public override void interact()
    {
        source.tag = "laserEnd";
        Debug.Log("activated");
    }

    protected override void Update()
    {
        base.Update();
        if (source.GetComponent<laserSource>().ball == null)
        {
            source.tag = "Untagged";
        }
    }
}

