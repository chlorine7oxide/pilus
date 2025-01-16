using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour
{
    public Vector3 entry;
    public GameObject mc;

    public static void create(GameObject mc, Vector3 startPos)
    {
        GameObject g = Instantiate(followerCreator.followerPrefab, startPos, Quaternion.identity);
        follower f = g.AddComponent<follower>();
        f.mc = mc;
        f.entry = startPos;
        g.GetComponent<SpriteRenderer>().sprite = null;
    }

    public Vector3[] positions = new Vector3[15];
    public int posIndex = 0;

    public float animCD = 0;
    public float animNum = 0;

    public bool startAnim = false;

    private void FixedUpdate()
    {
        if (positions[posIndex] != null && playerData.companion)
        {
            if (mc.transform.position != positions[(posIndex + 9) % 10])
            {
                if (startAnim)
                {
                    Vector3 movement = positions[posIndex] - this.gameObject.transform.position;
                    this.gameObject.transform.position = positions[posIndex];

                    if (animCD < 0)
                    {
                        if (movement.y > 0 && movement.y > Mathf.Abs(movement.x))
                        {
                            //up
                            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                            switch (animNum)
                            {
                                case 0:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.b1;
                                    break;
                                case 1:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.b2;
                                    break;
                                case 2:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.b3;
                                    break;
                                case 3:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.b4;
                                    break;
                            }
                            animNum++;
                            animNum %= 4;

                        }
                        else if (movement.y < 0 && Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
                        {
                            //down
                            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                            switch (animNum)
                            {
                                case 0:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.f1;
                                    break;
                                case 1:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.f2;
                                    break;
                                case 2:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.f3;
                                    break;
                                case 3:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.f4;
                                    break;
                            }
                            animNum++;
                            animNum %= 4;
                        }
                        else if (movement.x > 0)
                        {
                            //right
                            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                            switch (animNum)
                            {
                                case 0:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s1;
                                    break;
                                case 1:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s2;
                                    break;
                                case 2:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s3;
                                    break;
                                case 3:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s4;
                                    break;
                            }
                            animNum++;
                            animNum %= 4;
                        }
                        else
                        {
                            //left
                            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                            switch (animNum)
                            {
                                case 0:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s1;
                                    break;
                                case 1:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s2;
                                    break;
                                case 2:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s3;
                                    break;
                                case 3:
                                    this.gameObject.GetComponent<SpriteRenderer>().sprite = followerCreator.s4;
                                    break;
                            }
                            animNum++;
                            animNum %= 4;

                        }
                        animCD = 0.15f;
                    }
                }

                animCD -= Time.fixedDeltaTime;

                

                positions[posIndex] = mc.transform.position;
                posIndex++;
                posIndex %= positions.Length;

                if (posIndex == 14)
                {
                    startAnim = true;
                }
            }
        }
        else
        {
            //boat animate placholder
        }
    }

    private void Update()
    {
        if (this.gameObject.transform.position.y > mc.transform.position.y)
        {
            mc.GetComponent<SpriteRenderer>().sortingOrder = 8;
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 7;
        }
        else
        {
            mc.GetComponent<SpriteRenderer>().sortingOrder = 7;
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 8;
        }
    }
}
