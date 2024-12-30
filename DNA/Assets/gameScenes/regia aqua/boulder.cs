using System.Collections;
using UnityEngine;

public class boulder : overworldInteractable
{
    public Sprite portrait;
    public Sprite face;

    public static int x = 0;

    public override void interact()
    {
        StartCoroutine(text());
        x++;
    }

    public IEnumerator text()
    {
        generalText g = generalText.create((x == 6) ? "I'm CHOOSING not to move this one." : "This boulder is too heavy to move.", portrait, face);
        if (g == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => g.done);
        g.destroy();
    }
}
