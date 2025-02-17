using System.Collections;
using UnityEngine;

public class barrier2Trigger : MonoBehaviour
{
    public barrier2 barrier2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (playerData.guard2stage == 0)
            {
                barrier2.StartCoroutine(barrier2.dialogue());
            }
            else if (playerData.guard2stage > 0)
            {
                StartCoroutine(text());
            }
        }
    }

    public Sprite portrait;

    public IEnumerator text()
    {
        generalText t = generalText.create("I think we should follow them.", portrait, null);

        yield return new WaitUntil(() => t.done);

        t.destroy();
    }  
}
