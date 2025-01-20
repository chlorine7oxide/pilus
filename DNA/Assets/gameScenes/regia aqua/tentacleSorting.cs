using System.Collections.Generic;
using UnityEngine;

public class tentacleSorting : MonoBehaviour
{
    public List<GameObject> tentacles;

    void Update()
    {
        foreach (GameObject tent in tentacles)
        {
            tent.GetComponent<SpriteRenderer>().sortingOrder = (int)(-tent.transform.position.y * 100);
        }
    }

    public void Start()
    {
        GameObject[] tents = GameObject.FindGameObjectsWithTag("tentacle");
        foreach (GameObject tent in tents)
        {
            tentacles.Add(tent);
        }
    }
}
