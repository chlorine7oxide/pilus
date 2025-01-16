using System.Collections;
using UnityEngine;

public class followerCreator : MonoBehaviour
{
    public static Sprite f1, f2, f3, f4, s1, s2, s3, s4, b1, b2, b3, b4;
    public Sprite f1_, f2_, f3_, f4_, s1_, s2_, s3_, s4_, b1_, b2_, b3_, b4_;

    public static GameObject followerPrefab;
    public GameObject followerPrefab_;

    private void Start()
    {
        f1 = f1_;
        f2 = f2_;
        f3 = f3_;
        f4 = f4_;
        s1 = s1_;
        s2 = s2_;
        s3 = s3_;
        s4 = s4_;
        b1 = b1_;
        b2 = b2_;
        b3 = b3_;
        b4 = b4_;

        followerPrefab = followerPrefab_;

        StartCoroutine(startFetch());
    }

    public IEnumerator startFetch()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        startpos = this.gameObject.transform.position;
        yield return new WaitUntil(() => ((this.gameObject.transform.position - startpos).magnitude > 0.5f && !created && playerData.companion));
        follower.create(gameObject, transform.position);
        created = true;
    }
    

    public Vector3 startpos;
    public bool created = false;

}

