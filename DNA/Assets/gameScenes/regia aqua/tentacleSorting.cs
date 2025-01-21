using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class tentacleSorting : MonoBehaviour
{
    public List<GameObject> tentacles;

    void Update()
    {
        foreach (GameObject tent in tentacles)
        {
            if (tent is not null)
            {
                if (tent.GetComponent<tentacle>().active)
                {
                    tent.GetComponent<SpriteRenderer>().sortingOrder = (int)(-tent.transform.position.y * 100);
                }
            }
            
        }
        if (GameObject.FindGameObjectWithTag("player") is not null)
        {
            GameObject.FindGameObjectWithTag("player").GetComponent<SpriteRenderer>().sortingOrder = (int)(-GameObject.FindGameObjectWithTag("player").transform.position.y * 100);
        }
        
    }

    public void Start()
    {
        StartCoroutine(getTents());
        
    }

    public IEnumerator getTents()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("tentacle").Length > 0);
        GameObject[] tents = GameObject.FindGameObjectsWithTag("tentacle");
        foreach (GameObject tent in tents)
        {
            tentacles.Add(tent);
        }
    }
}
