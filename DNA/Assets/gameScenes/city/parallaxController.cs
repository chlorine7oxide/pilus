using UnityEngine;

public class parallaxController : MonoBehaviour
{
    public float speed;
    public Vector2 move;

    public GameObject inv;

    public int numLayers, layerOrder;

    void Update()
    {
        int presses = 0;
        move = Vector2.zero;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            presses++;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            presses++;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            presses++;
            move += new Vector2(speed, 0) * (float)layerOrder / numLayers;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            presses++;
            move += new Vector2(-speed, 0) * (float)layerOrder / numLayers;
        }
        if (presses == 2)
        {
            move /= 1.41421356f;
        }
        GameObject d = GameObject.FindGameObjectWithTag("dialogue");
        if (d is null)
        {
            if (inv.GetComponent<inventoryController>().active || lockDoor1.lockActive)
            {

                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("player").transform.position.x > -27 && GameObject.FindGameObjectWithTag("player").transform.position.x < 27)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = move;
                }
                else
                {
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                }
                
            }
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
        if (GameObject.FindGameObjectWithTag("book") is not null)
        {
            if (GameObject.FindGameObjectWithTag("book").GetComponent<bookSelector>().active)
            {
                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }
    }
}


// for a revolving effect, set the order to negative of what it should be