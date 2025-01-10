using System.Collections;
using UnityEngine;

public class textInteraction : overworldInteractable
{
    public Sprite portrait;
    public string[] text;

    public override void interact()
    {
        StartCoroutine(text_());
    }

    public IEnumerator text_()
    {
        generalText t = generalText.create(text[0], portrait, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);

        if (text.Length > 1)
        {
            for (int i = 1;i < text.Length; i++)
            {
                t.changeText(text[i]);
                yield return new WaitUntil(() => t.done);
            }
        }

        t.destroy();
    }
}
