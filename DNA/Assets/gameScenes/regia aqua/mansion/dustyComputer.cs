using System.Collections;
using UnityEngine;

public class dustyComputer : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("An old dusty computer.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("It's so big!");
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
