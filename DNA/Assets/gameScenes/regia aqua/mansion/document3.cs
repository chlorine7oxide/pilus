using System.Collections;
using UnityEngine;

public class document3 : overworldInteractable
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
        t.changeText("It reads \"Public service announcement.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText("\"I know these are trying times for us, but if we band togethere as people, we can get through this.\"");
        yield return new WaitUntil(() => t.done);
        t.changeText("\" - M\"");
        yield return new WaitUntil(() => t.done);
        playerData.documentsRead++;
        t.destroy();
    }
}
