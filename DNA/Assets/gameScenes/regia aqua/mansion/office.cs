using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class office : MonoBehaviour
{
    public Sprite portrait;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            if (playerData.items.Contains("Desk Key"))
            {
                SceneManager.LoadScene("office");
            }
            else
            {
                StartCoroutine(text());
            }
        }
    }

    public IEnumerator text()
    {
        generalText t = generalText.create("The door is locked, and I don't have the key.", portrait, null);
        yield return new WaitUntil(() => t.done);
        t.destroy();
    }
}
