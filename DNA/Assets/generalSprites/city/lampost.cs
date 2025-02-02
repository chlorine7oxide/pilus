using UnityEngine;

public class lampost : MonoBehaviour
{

    void Update()
    {
        if (this.gameObject.transform.position.y - 0.75f> GameObject.FindGameObjectWithTag("player").transform.position.y)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 12;
        }
    }
}
