using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolaClavos : MonoBehaviour
{   


    public bool enableShoot;
    public float offset;
    public Transform firePoint;
	public GameObject bulletPrefab;
    private int nailCount;
    private int nailObjectCount;
    public int maximumNails;
    public int maximumJumpNails;
    public Transform mirilla;
    
    private List<GameObject> nailedList = new List<GameObject>();
    public List<GameObject> nailedListJump = new List<GameObject>();
    public Kate1 kate;

    
    // Start is called before the first frame update
    void Start()
    {       
        nailCount = 0;
        nailObjectCount = 0;
        enableShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        // pa girar la pistola con raton
        

        if ( Input.GetKeyDown(KeyCode.Mouse1))
		{
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            Shoot();
		} else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Vector3 difference = mirilla.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            Shoot();
        }
    }

   

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public bool nailAdd(){
        nailCount++;

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
        kate.activeClavos = nailedList;
    }

    public void borrarOldestNailed()
    {        
           if(nailedList[0] != null)
            {  
                kate.DecreaseSelected(nailedList[0].transform.GetChild(0).gameObject);             
                Destroy(nailedList[0]);
            }
            
            nailedList.RemoveAt(0);
        kate.activeClavos = nailedList;
    }

    public bool nailAddObject(){
        nailObjectCount++;
        if (nailObjectCount - 1 >= maximumJumpNails)
        { 
            
            return true;            
        }
        else
        {
            return false;
        }         
    }

    public void saveNailedObject(GameObject nailed)
    {
        nailedListJump.Add(nailed);
    }

    public void borrarOldestNailedObject()
    {        
           if(nailedListJump[0] != null)
            {              
                Destroy(nailedListJump[0]);
            }
            
            nailedListJump.RemoveAt(0);
    }
}
