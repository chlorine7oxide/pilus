using UnityEngine;

public class followerCreator : MonoBehaviour
{
    public Sprite f1, f2, f3, f4, s1, s2, s3, s4, b1, b2, b3, b4;

    private void Start()
    {
        StartCoroutine(follower.create(gameObject, transform.position));
    }
}

