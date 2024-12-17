using UnityEngine;

public class fishspawner : MonoBehaviour
{
    public fish fish;
    public Sprite trout, eel, sturgeon, box;

    void Start()
    {
        fish.box = box;
        fish = fish.create(trout, 1);
    }

    void Update()
    {
        
    }
}
