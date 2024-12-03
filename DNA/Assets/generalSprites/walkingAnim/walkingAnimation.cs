using NUnit.Framework.Constraints;
using UnityEngine;

public class walkingAnimation : MonoBehaviour
{
    public string last = "none";
    public static int direction = 0; // up 0 right 1 down 2 left 3

    public Sprite up1, up2, up3, up4, d1, d2, d3, d4, side1, side2, side3, side4;
    public int count = 1;

    public float cd = 0.15f;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && cd < 0)
        {
            if (last != "up")
            {
                count = 1;
            }
            switch (count)
            {
                case 1:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = up2;
                    count = 2;
                    break;
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = up3;
                    count = 3;
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = up4;
                    count = 4;
                    break;
                case 4:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = up1;
                    count = 1;
                    break;
            }
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            last = "up";
            cd = 0.15f;
            direction = 0;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && cd < 0)
        {
            if (last != "down")
            {
                count = 1;
            }
            switch (count)
            {
                case 1:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = d2;
                    count = 2;
                    break;
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = d3;
                    count = 3;
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = d4;
                    count = 4;
                    break;
                case 4:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = d1;
                    count = 1;
                    break;
            }
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            last = "down";
            cd = 0.15f;
            direction = 2;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && cd < 0)
        {
            if (last != "left")
            {
                count = 1;
            }
            switch (count)
            {
                case 1:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side2;
                    count = 2;
                    break;
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side3;
                    count = 3;
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side4;
                    count = 4;
                    break;
                case 4:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side1;
                    count = 1;
                    break;
            }
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            last = "left";
            cd = 0.15f;
            direction = 3;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && cd < 0)
        {
            if (last != "right")
            {
                count = 1;
            }
            switch (count)
            {
                case 1:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side2;
                    count = 2;
                    break;
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side3;
                    count = 3;
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side4;
                    count = 4;
                    break;
                case 4:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = side1;
                    count = 1;
                    break;
            }
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            last = "right";
            cd = 0.15f;
            direction = 1;
        }
        else
        {
            if (cd < 0)
            {
                switch (last)
                {
                    case "up":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = up1;
                        count = 1;
                        break;
                    case "down":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = d1;
                        count = 1;
                        break;
                    case "left":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = side1;
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                        count = 1;
                        break;
                    case "right":
                        this.gameObject.GetComponent<SpriteRenderer>().sprite = side1;
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        count = 1;
                        break;
                }
                last = "none";
            }
        }

        cd -= Time.deltaTime;

    }
}
