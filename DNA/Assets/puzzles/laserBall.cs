using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class laserBall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("laserEnd"))
        {
            Debug.Log("collided with end");
            source.GetComponent<SpriteRenderer>().color = Color.green;
            Destroy(this.gameObject);
        }
    }

    float time = 0;
    public GameObject source;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 10)
        {
            Destroy(this.gameObject);
        }   
        if (this.gameObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude != 3)
        {
            this.gameObject.GetComponent<Rigidbody2D>().linearVelocity /= this.gameObject.GetComponent<Rigidbody2D>().linearVelocity.magnitude / 3;
        }
    }
}
