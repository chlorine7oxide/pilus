using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class skeletonTrigger : MonoBehaviour
{
    public GameObject skeleton;
    public PostProcessProfile scary;
    public GameObject post;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            if (skeleton != null)
            {
                post.GetComponent<PostProcessVolume>().profile = scary;
                skeleton.transform.position = new Vector3(0.5f, 3.5f, 0);
                skeleton.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -2);
                skeleton.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-5, 5);
                Destroy(this.gameObject);
            }
        }
    }

    private void Start()
    {
        if (playerData.skeletonFought)
        {
            Destroy(skeleton);
        }
    }
}

