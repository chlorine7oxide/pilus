using System.Collections;
using UnityEditor.AssetImporters;
using UnityEngine;

public class button : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("puzzlePart"))
        {
            Destroy(door);
            Destroy(collision.gameObject.GetComponent<box>());
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            StartCoroutine(moveWait(collision.gameObject));
        }
    }

    public IEnumerator moveWait(GameObject g)
    {
        yield return new WaitForSeconds(0.1f);
        g.transform.position = this.gameObject.transform.position;
    }

}
