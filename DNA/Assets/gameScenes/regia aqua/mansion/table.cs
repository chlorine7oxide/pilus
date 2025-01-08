using System.Collections;
using UnityEngine;

public class table : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("An old table.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("It's super dusty.");
        yield return new WaitUntil(() => t.done);
        t.changeText("I don't really want to touch it.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
