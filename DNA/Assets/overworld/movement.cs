using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public Vector2 move;

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
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = move;
    }
}
