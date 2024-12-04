using Unity.VisualScripting;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int damage;
    public Vector3 dir;
    public float life;

    public void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().linearVelocity = dir;
        life -= Time.deltaTime;
        if (life < 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<realcombatController>().takeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
