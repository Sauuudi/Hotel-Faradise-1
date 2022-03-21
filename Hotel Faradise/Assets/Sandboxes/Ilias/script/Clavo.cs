using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clavo : MonoBehaviour
{
    public float speed = 20f;
	public Rigidbody2D rb;
	//public GameObject clavadoEffect;
    public GameObject nailedPrefab;
    
    public int lifeTime;

    private PistolaClavos pistola;


    // Start is called before the first frame update
    void Start()
    {   
        GameObject go = GameObject.Find("firePoint");
        pistola = (PistolaClavos) go.GetComponent(typeof(PistolaClavos));

        rb.velocity = transform.right * speed;   
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update() {
        
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
	{   
        if (hitInfo.tag.Equals("IceWall"))
        {
            //Instantiate(clavadoEffect, transform.position, transform.rotation); //for the effect of collision do after
            GameObject clone = Instantiate(nailedPrefab, transform.position, transform.rotation);
            
            pistola.saveNailed(clone);
            bool delete = pistola.nailAdd();

            if(delete)
            {
                pistola.borrarOldestNailed();
            }   
        }	
		
        Destroy(gameObject);
	}

     void DestroyProjectile() {
        Destroy(gameObject);
    }
}
