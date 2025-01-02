using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class alert : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(end());
    }

    public IEnumerator end()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }

    public GameObject target;

    private void Update()
    {
        if (target != null)
        {
            this.transform.position = target.transform.position + new Vector3(0, 1, 0);
        }
        
    }

}
