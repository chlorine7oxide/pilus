using NUnit.Framework.Constraints;
using UnityEngine;

public class walkingAnimation : MonoBehaviour
{
    public string last = "none";

    public static bool heldItem = false;

    public Sprite[] up, down, side, uparm, downarm, sidearm;
    public int count = 0;

    public static int direction = 0;

    public float cd = 0.15f;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && cd < 0)
        {
            direction = 0;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            last = "up";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? up[count] : uparm[count];
            count++;
            count %= 4;
            cd = 0.15f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && cd < 0)
        {
            direction = 2;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            last = "down";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? down[count] : downarm[count];
            count++;
            count %= 4;
            cd = 0.15f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && cd < 0)
        {
            direction = 3;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            last = "left";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? side[count] : sidearm[count];
            count++;
            count %= 4;
            cd = 0.15f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && cd < 0)
        {
            direction = 1;
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            last = "right";
            this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? side[count] : sidearm[count];
            count++;
            count %= 4;
            cd = 0.15f;
        }
        else
        { 
            if (cd < 0)
            {
                
                switch (last)
                {
                    case "up":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? up[1] : uparm[1];
                        break;
                    case "down":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? down[1] : downarm[1];
                        break;
                    case "left":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? side[1] : sidearm[1];
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                        break;
                    case "right":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = (!heldItem) ? side[1] : sidearm[1];
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        break;
                }
                count = 1;
                
                last = "none";
                
            }

        }

        cd -= Time.deltaTime;

    }
}
