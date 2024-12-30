using System.Collections;
using UnityEngine;

public class crowtroller : MonoBehaviour
{
    public GameObject crow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Animator anim = crow.GetComponent<Animator>();
            anim.SetInteger("state", 1);
            StartCoroutine(switchAnim());
        }
    }
    
    public IEnumerator switchAnim()
    {
        yield return new WaitForSeconds(1f);
        Animator anim = crow.GetComponent<Animator>();
        anim.SetInteger("state", 2);
        for (int i = 0;i< 1000; i++)
        {
            crow.transform.Translate(new Vector3(-0.03f, 0.02f, 0));
            yield return new WaitForFixedUpdate();
        }
    }
}
