using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chaseDoor : overworldInteractable
{
    public override void interact()
    {
        if (playerData.guard2stage == 3)
        {
            StartCoroutine(locked());
        }
        else if (playerData.guard2stage == -1)
        {
            SceneManager.LoadScene("chase");
        }
    }

    public Sprite portrait;

    public IEnumerator locked()
    {
        generalText t = generalText.create("The door is locked.", portrait, null);

        if (t is null)
        {
            yield break;
        }

        yield return new WaitUntil(() => t.done);

        t.changeText("I think we're stuck...");

        yield return new WaitUntil(() => t.done);

        t.destroy();

        playerData.chaseLockedDoorSeen = true;
    }
}
