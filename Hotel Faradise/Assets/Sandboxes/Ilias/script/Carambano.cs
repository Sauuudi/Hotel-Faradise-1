using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carambano : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Clavo"){
            if(isFalling == false){
                rb.gravityScale = 5;
                isFalling = true;
                Invoke("Freeze", 1);
            }
        }
    }

    void Freeze(){
        rb.constraints =  RigidbodyConstraints2D.FreezeAll ;
        gameObject.layer = LayerMask.NameToLayer("Objects");
    }
}
