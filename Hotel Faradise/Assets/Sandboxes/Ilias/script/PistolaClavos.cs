using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaClavos : MonoBehaviour
{   

    public float offset;
    public Transform firePoint;
	public GameObject bulletPrefab;
    private int nailCount;
    public int maximumNails;

    public List<GameObject> nailedList = new List<GameObject>();

    public Kate1 kate;

    
    // Start is called before the first frame update
    void Start()
    {       
        nailCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // pa girar la pistola con raton
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
    }

   

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public bool nailAdd()
    {
        nailCount++;
        Debug.Log("nails: "+ nailCount);

        if (nailCount - 1 >= maximumNails)
        {
            return true;
            
        }
        else
        {
            return false;
        }
         
    }

    public void saveNailed(GameObject nailed)
    {
        nailedList.Add(nailed);
    }

    public void borrarOldestNailed()
    {        
           if(nailedList[0] != null)
            {                
                Destroy(nailedList[0]);
                
            }
            
            kate.DecreaseSelected(nailedList[0]);
            
            nailedList.RemoveAt(0);
    }
}
