using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class staffArea : MonoBehaviour
{

    public Sprite portrait;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            StartCoroutine(staffAreaEvent());
        }
    }

    public IEnumerator staffAreaEvent()
    {
        generalText t = generalText.create("It says staff only!", portrait, null);

        if (t is null)
        {
            yield break;
        }

        yield return new WaitUntil(() => t.done);

        t.changeText("I can't go in there!");

        yield return new WaitUntil(() => t.done);

        t.destroy();
    }
}
