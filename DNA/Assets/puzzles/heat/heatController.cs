using UnityEngine;

public class heatController : MonoBehaviour
{
    public static int heat = 0;

    public static void increaseHeat()
    {
        heat++;
    }

    public static void decreaseHeat()
    {
        heat--;
    }   
}
