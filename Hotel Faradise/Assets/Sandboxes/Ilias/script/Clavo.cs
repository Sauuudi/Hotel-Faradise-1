using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clavo : MonoBehaviour
{
    public float speed = 20f;
	public Rigidbody2D rb;
    public GameObject nailedPrefab;
    public GameObject clavoSaltoPrefab;
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
        Debug.Log("he chocado contra: " + hitInfo.tag);
        if (hitInfo.gameObject.CompareTag("IceGround"))
        {
            GameObject clone = Instantiate(nailedPrefab, transform.position, transform.rotation);
            clone.GetComponentInChildren<ClavoCuerda>().isHangingPlatform = false;
            pistola.saveNailed(clone);
            bool delete = pistola.nailAdd();

            if(delete){
                pistola.borrarOldestNailed();
               
            }   
        } else if(hitInfo.gameObject.CompareTag("hangingPlatform"))
        {
            GameObject clone = Instantiate(nailedPrefab, transform.position, transform.rotation);
            GameObject emptyChild = new GameObject();
            emptyChild.transform.parent = hitInfo.transform;
            clone.transform.parent = emptyChild.transform;


            clone.GetComponentInChildren<ClavoCuerda>().isHangingPlatform = true;
            clone.GetComponentInChildren<ClavoCuerda>().hangingPlatform = hitInfo.gameObject;
            pistola.saveNailed(clone);
            bool delete = pistola.nailAdd();

            if (delete)
            {
                pistola.borrarOldestNailed();

            }
        } else if (hitInfo.gameObject.CompareTag("DiagonalGroundIce")) 
        {
            GameObject clone = Instantiate(clavoSaltoPrefab, transform.position, transform.rotation);
            
            pistola.saveNailedObject(clone);
            bool delete = pistola.nailAddObject();

            if(delete){
                pistola.borrarOldestNailedObject();
               
            }  
        } else if (hitInfo.gameObject.CompareTag("Button"))
        {
            hitInfo.gameObject.GetComponent<buttonDoor>().activatedButton();
        }

		
        Destroy(gameObject);
	}

     void DestroyProjectile() {
        Destroy(gameObject);
    }
}
