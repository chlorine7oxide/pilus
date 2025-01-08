using System.Collections;
using UnityEngine;

public class enterEvents : MonoBehaviour
{
    public Sprite port;

    void Start()
    {
        if (playerData.visitedLibrary)
        {
            StartCoroutine(library());
        }
    }

    public IEnumerator library()
    {
        generalText t = generalText.create("What a strange library", port, null);
        if (t == null)
        {
            yield break;
        }
        yield return new WaitUntil(() => t.done);
        t.changeText("All the books were sorted by colour...");
        yield return new WaitUntil(() => t.done);
        t.destroy();
        playerData.strangeLibrary = true;
    }
}
