using System.Collections;
using UnityEngine;

public class combatheater : MonoBehaviour
{
    public bool moved = false;
    public int pos = 0;

    private void Update()
    {
        if (heatController.heat == 1 & !moved)
        {
            moved = true;
            Destroy(this.gameObject.GetComponent<PolygonCollider2D>());
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(move(new Vector3(30, 0, 0), 3));
            
        }
    }

    public IEnumerator move(Vector3 target, float time)
    {
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = (target - this.gameObject.transform.position) / time;
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        yield return new WaitForSeconds(time);
        this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.gameObject.AddComponent<PolygonCollider2D>();
        this.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        pos = 1;

    }
}
