using NUnit;
using UnityEngine;

public class summitParalax : MonoBehaviour
{
    public float speed;
    public Vector2 move;

    public GameObject inv;

    void Update()
    {
        move = Vector2.zero;
        int presses = 0;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            presses++;
            move += new Vector2(0, -speed);
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
            if (inv.GetComponent<inventoryController>().active || lockDoor1.lockActive)
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
        if (GameObject.FindGameObjectWithTag("book") is not null)
        {
            if (GameObject.FindGameObjectWithTag("book").GetComponent<bookSelector>().active)
            {
                this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            }
        }

    }
}
