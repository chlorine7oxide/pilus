using UnityEngine;

public class geyserController : MonoBehaviour
{
    public static float geyserTime = 10000000000;
    public static float cd;
    public static bool combat = false;

    public Sprite[] geyserAnim;

    private void Start()
    {
        cd = 100000000;
        geyser.geyserAnim = geyserAnim;
    }

    void Update()
    {
        if (cd < 0 && !combat)
        {
            Vector3 water = boss1Controller.closestWater();
            geyser.create(water + (water - GameObject.FindGameObjectWithTag("player").transform.position).normalized);
            cd = geyserTime;
        }
        else
        {
            cd -= Time.deltaTime;
        }
    }
}
