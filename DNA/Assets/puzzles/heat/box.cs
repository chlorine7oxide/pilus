using NUnit.Framework;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class box : overworldInteractable
{
    public Tilemap water;
    public Tile[] waterTile;

    public override void interact()
    {
        if (this.gameObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude == 0)
        {
            if (heatController.heat < 0)
            {
                if (walkingAnimation.direction == 1)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(4, 0);
                }
                if (walkingAnimation.direction == 3)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-4, 0);
                }
                if (walkingAnimation.direction == 0)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 4);
                }
                if (walkingAnimation.direction == 2)
                {
                    this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -4);
                }
            }
            if (heatController.heat == 0)
            {
                if (walkingAnimation.direction == 1)
                {
                    StartCoroutine(slideStop(new Vector2(4, 0)));
                }
                if (walkingAnimation.direction == 3)
                {
                    StartCoroutine(slideStop(new Vector2(-4, 0)));
                }
                if (walkingAnimation.direction == 0)
                {
                    StartCoroutine(slideStop(new Vector2(0, 4)));
                }
                if (walkingAnimation.direction == 2)
                {
                    StartCoroutine(slideStop(new Vector2(0, -4)));
                }
            }
        }           
    }

    private void Start()
    {
        heatController.decreaseHeat();
    }

    public IEnumerator slideStop(Vector2 dir)
    {
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = dir;
        yield return new WaitForSeconds(0.24f);
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 0);
    }

    protected override void Update()
    {
        base.Update();


        /*
        if (heatController.heat <= 0)
        {
            water.SetTile(water.WorldToCell(this.transform.position), waterTile[(int)(this.transform.position.x + this.transform.position.y) % 4]);
        }
        else
        {
            water.SetTile(water.WorldToCell(this.transform.position), null);
        }
        */
        if (this.gameObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude == 0)
        {
            if (Mathf.Round(2 * this.gameObject.transform.position.x) != 2* this.gameObject.transform.position.x)
            {
                this.gameObject.transform.position = new Vector3(Mathf.Round(2 * this.gameObject.transform.position.x) / 2.0f, this.gameObject.transform.position.y, 0); ;
            }
            if (Mathf.Round(2 * this.gameObject.transform.position.y) != 2 * this.gameObject.transform.position.y)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, Mathf.Round(2 * this.gameObject.transform.position.y) / 2.0f, 0); ;
            }
        }
        else if(this.gameObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude < 1)
        {
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = this.gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized * 4;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("endButton"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
            this.gameObject.transform.position = collision.gameObject.transform.position;
        }
    }


}
