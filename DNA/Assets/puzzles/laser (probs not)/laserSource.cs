using Unity.VisualScripting;
using UnityEngine;

public class laserSource : MonoBehaviour
{
    public GameObject ball;
    public Sprite laserImg;
    public PhysicsMaterial2D laserMat;

    private void Update()
    {
        if (ball == null)
        {
            ball = new();
            ball.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            ball.transform.position = this.gameObject.transform.position;
            ball.AddComponent<laserBall>();
            ball.AddComponent<SpriteRenderer>().sprite = laserImg;
            ball.GetComponent<SpriteRenderer>().sortingOrder = 3;
            ball.AddComponent<Rigidbody2D>().sharedMaterial = laserMat;
            ball.AddComponent<CircleCollider2D>();
            ball.GetComponent<Rigidbody2D>().gravityScale = 0;
            ball.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-1f,0);
            ball.GetComponent<Rigidbody2D>().mass = 0;
            ball.GetComponent<Rigidbody2D>().excludeLayers = 1 << 3;
            ball.GetComponent<laserBall>().source = this.gameObject;
        }
    }
}
