using System.Collections;
using UnityEngine;

public class codeHint : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("An important looking note.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("It reads \"In case you have forgotten the second code to the docks.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText("\"It's the same as the first code, but in reverse.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText(" - M");
        yield return new WaitUntil(() => t.done);
        playerData.documentsRead++;
        t.destroy();
    }
}
