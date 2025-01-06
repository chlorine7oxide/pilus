using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follower : MonoBehaviour
{
    public Vector3 entry;
    public GameObject mc;

    public static IEnumerator create(GameObject mc, Vector3 startPos)
    {
        yield return new WaitUntil(() => (mc.transform.position - startPos).magnitude > 1);
        GameObject g = new();
        follower f = g.AddComponent<follower>();
        f.mc = mc;
        f.entry = startPos;
    }

    public Vector2[] positions = new Vector2[50];
    public int posIndex = 0;

    private void FixedUpdate()
    {
        if (positions[posIndex] != null)
        {
            // use it placeholder
        }

        positions[posIndex] = mc.transform.position;
        posIndex++;
        posIndex %= positions.Length;
    }
}
