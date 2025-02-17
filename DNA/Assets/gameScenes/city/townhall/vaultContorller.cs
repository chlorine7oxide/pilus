using UnityEngine;

public class vaultContorller : MonoBehaviour
{
    public GameObject mc;
    public GameObject bg1, bg2, bg3;
    public GameObject triggers;
    public int threshold = 20;

    void Update()
    {
        if (mc.transform.position.x > threshold)
        {
            threshold += 9;
            bg1.transform.Translate(9, 0, 0);
            bg2.transform.Translate(9, 0, 0);
            bg3.transform.Translate(9, 0, 0);
            triggers.transform.Translate(9, 0, 0);
        }
    }
}
