using System.Collections;
using UnityEngine;

public class desk : overworldInteractable
{
    public Sprite portrait;

    public override void interact()
    {
        StartCoroutine(text());
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("A desk covered in papers, cards and books.", portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("They seem to all be some sort of semi-official documents.");
        yield return new WaitUntil(() => t.done);
        t.changeText("Permits, licences, letters and reports");
        yield return new WaitUntil(() => t.done);
        t.changeText("None of it would be useful since it's all out of date.");
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
