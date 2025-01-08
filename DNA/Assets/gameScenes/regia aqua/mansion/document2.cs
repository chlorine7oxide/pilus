using System.Collections;
using UnityEngine;

public class document2 : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("An official looking document.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("It reads \"Official notice of evacuation.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText("\"If you can read this, you must evacuate the town to the nearest city.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText("\"This decision was not made lightly, we regretfully had to resort to this as our last option.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText("I wonder what happened here?");
        yield return new WaitUntil(() => t.done);
        playerData.documentsRead++;
        t.destroy();
    }
}
