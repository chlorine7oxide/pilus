using System.Collections;
using UnityEngine;

public class barMover : MonoBehaviour
{

    public string last = "left";
    public float speed = 0.1f;

    void FixedUpdate()
    {
        switch (last) {
            case "left":
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        last = "up";
                        StartCoroutine(moveOverTime(speed));
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow)){
                        last = "down";
                        StartCoroutine(moveOverTime(-speed));
                    }
                    break;
                }
            case "up":
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        last = "right";
                        StartCoroutine(moveOverTime(speed));
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        last = "left";
                        StartCoroutine(moveOverTime(-speed));
                    }
                    break;
                }
            case "right":
                {
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        last = "down";
                        StartCoroutine(moveOverTime(speed));
                    }
                    else if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        last = "up";
                        StartCoroutine(moveOverTime(-speed));
                    }
                    break;
                }
            case "down":
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        last = "left";
                        StartCoroutine(moveOverTime(speed));
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        last = "right";
                        StartCoroutine(moveOverTime(-speed));
                    }
                    break;
                }
        }

    }


    public IEnumerator moveOverTime(float amount)
    {
        for (int i = 0; i < 10; i++)
        {
            if (amount > 0)
            {
                Vector3 v = new Vector3(0, Mathf.Min(2.5f, this.transform.position.y + amount / 10), 0);
                if (v.y == 2.5f)
                {
                    this.transform.position = v;
                    yield break;
                }
                else
                {
                    this.transform.position = v;
                }
            }
            else
            {
                Vector3 v = new Vector3(0, Mathf.Max(-2.5f, this.transform.position.y + amount / 10), 0);
                if (v.y == -2.5f)
                {
                    this.transform.position = v;
                    yield break;
                }
                else
                {
                    this.transform.position = v;
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
