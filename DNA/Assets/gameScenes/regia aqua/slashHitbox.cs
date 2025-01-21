using System.Collections;
using UnityEngine;

public class slashHitbox : MonoBehaviour
{
    public bool flipped;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "player")
        {
            other.gameObject.GetComponent<realcombatController>().takeDamage(25);
        }
    }

    private void Start()
    {
        StartCoroutine(rotate());
    }

    IEnumerator rotate()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 19; i++)
        {
            yield return new WaitForSeconds(0.02f);
            transform.Rotate(0, 0, (flipped) ? -4.5f : 4.5f);
        }
    }
}
