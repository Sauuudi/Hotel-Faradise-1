using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarambanoLoop : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        Invoke("DestroyAfter",3);
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Rope")){
            Destroy(other.gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    void DestroyAfter(){
        Destroy(gameObject);
    }
}
