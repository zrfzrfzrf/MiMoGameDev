using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShardInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    
    {
        Debug.Log("collision");
        if (other.CompareTag("Mi") || other.CompareTag("Mo"))
        {
            Destroy(gameObject);
            GameManager.Instance.CollectShard();
        }
    }

}
