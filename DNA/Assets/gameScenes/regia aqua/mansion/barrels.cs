using System.Collections;
using UnityEngine;

public class barrels : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A bunch of old barrels.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("There's nothing in them.");
        yield return new WaitUntil(() => t.done);
        t.changeText("People probably just sat on them while fishing.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
