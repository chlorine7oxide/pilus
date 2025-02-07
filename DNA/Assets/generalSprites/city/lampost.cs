using UnityEngine;

public class lampost : MonoBehaviour
{

    public float offset;

    void Update()
    {
        if (this.gameObject.transform.position.y - offset> GameObject.FindGameObjectWithTag("player").transform.position.y)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 9;
        }
    }
}
