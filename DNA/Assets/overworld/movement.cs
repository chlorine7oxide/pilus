using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public Vector2 move;

    public GameObject inv;

    void Update()
    {
        int presses = 0;
        move = Vector2.zero;
        if (Input.GetKey(KeyCode.DownArrow))
        {
            presses++;
            move += new Vector2(0, -speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            presses++;
            move += new Vector2(speed, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            presses++;
            move += new Vector2(-speed, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            presses++;
            move += new Vector2(0, speed);
        }
        if (presses == 2)
        {
            move /= 1.41421356f;
        }
        GameObject d = GameObject.FindGameObjectWithTag("dialogue");
        if (d is null)
        {
            if (inv.GetComponent<inventoryController>().active)
            {
                
                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = move;
            }
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
