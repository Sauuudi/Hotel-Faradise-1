using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kate1 : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public HingeJoint2D hj;

    public float pushForce = 10f;
    //[HideInInspector]
    public bool attached = false;
    [HideInInspector]
    public Transform attachedTo;
    private GameObject disregard = null;


    [Range(2, 4)]
    public int MaxClavos = 2;
    //[HideInInspector]
    public GameObject[] pulleySelected;
    private int clavosSelected = 0;

    public GameObject RopePrefab;
    public int lianaLength = 5;

    
    private GameObject currentRope;
    [SerializeField]
    private GameObject hangingOn = null;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hj = gameObject.GetComponent<HingeJoint2D>();
        pulleySelected = new GameObject[MaxClavos];
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyboardInputs();
        CheckPulleyInputs();
    }

    void CheckKeyboardInputs()
    {
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
            }
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (attached)
            {
                rb.AddRelativeForce(new Vector3(1, 0, 0) * pushForce);
            }
        }

        if (Input.GetKeyDown("r") && pulleySelected[0] != null)
        {
            GameObject clavo0 = pulleySelected[0];
            if(clavo0 != null)
            {
                GameObject clavo1 = pulleySelected[1];
                if (clavo1 == null)
                {
                    if (!clavo0.GetComponent<ClavoCuerda>().isRoped)
                    {
                        GenerateRope(clavo0,false);
                        clavo0.GetComponent<ClavoCuerda>().changeRoped();
                    }
                    else
                    {
                        DestroyRope(clavo0,"Gancho");
                        clavo0.GetComponent<ClavoCuerda>().changeRoped();
                    }
                }
                else
                {
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(clavo0.transform.position.x, clavo0.transform.position.y), new Vector2(clavo1.transform.position.x, clavo1.transform.position.y));
                    if (hit.transform.gameObject.CompareTag("Clavo") && !clavo0.GetComponent<ClavoCuerda>().isDoubleRoped && !clavo1.GetComponent<ClavoCuerda>().isDoubleRoped)
                    {
                        GenerateBridge(clavo0, clavo1);
                        clavo0.GetComponent<ClavoCuerda>().changeDoubleRoped();
                        clavo1.GetComponent<ClavoCuerda>().changeDoubleRoped();
                    }else if (clavo0.GetComponent<ClavoCuerda>().isDoubleRoped && clavo1.GetComponent<ClavoCuerda>().isDoubleRoped)
                    {
                        DestroyRope(clavo0,"GanchoDoble", clavo1);
                        clavo0.GetComponent<ClavoCuerda>().changeDoubleRoped();
                        clavo1.GetComponent<ClavoCuerda>().changeDoubleRoped();

                    }
                }
            }
        }
        

        if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && attached)
        {
            Slide(1);
        }

        if ((Input.GetKeyDown("s") || Input.GetKeyDown("down")) && attached)
        {
            Slide(-1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (attached)
            {
                Detach();
            }
        }
    }

    void GenerateRope(GameObject clavo, bool obj)
    {   
        int aux = lianaLength;
        Rigidbody2D prevBod = clavo.GetComponent<Rigidbody2D>(); ; //previous rigidBody
        currentRope = new GameObject();
        currentRope.name = "Gancho";
        currentRope.transform.parent = clavo.transform.parent.transform;
        if(obj){
           aux = 2;
        }
        for (int i = 0; i < aux; i++)
        {
            GameObject newSeg = Instantiate(RopePrefab);
            newSeg.transform.parent = currentRope.transform;
            //newSeg.transform.position = clavo.transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod; //the first one will be connected to the hook

            prevBod = newSeg.GetComponent<Rigidbody2D>(); // and then the others will be connected to the previous newSeg

            //alreadyRoped[0] = newSeg;
        }
    }

    void GenerateBridge(GameObject clavo1, GameObject clavo2)
    {
        Rigidbody2D prevBod = clavo1.GetComponent<Rigidbody2D>(); ; //previous rigidBody
        float segmentRope = RopePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector2 clavo1V = new Vector2(clavo1.transform.position.x, clavo1.transform.position.y);
        Vector2 clavo2V = new Vector2(clavo2.transform.position.x, clavo2.transform.position.y);
        Debug.DrawRay(clavo2V, clavo1V, Color.green, 20);
        float distancia = Vector2.Distance(clavo1V, clavo2V);
        int numberSegments = (int)(distancia / segmentRope) + 1;
        currentRope = new GameObject();
        currentRope.name = "GanchoDoble";
        currentRope.transform.parent = clavo1.transform.parent.transform;
        for (int i = 0; i < numberSegments; i++)

        {
            GameObject newSeg = Instantiate(RopePrefab);
            newSeg.GetComponent<BoxCollider2D>().isTrigger = false;
            newSeg.transform.parent = currentRope.transform;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            newSeg.GetComponent<HingeJoint2D>().connectedBody = prevBod;
            prevBod = newSeg.GetComponent<Rigidbody2D>();
            //alreadyRoped[0] = newSeg;
            if(i + 1 == numberSegments)
            {
                newSeg.AddComponent<HingeJoint2D>();
                Component[] hingeJoints;

                hingeJoints = newSeg.GetComponents(typeof(HingeJoint2D));

                foreach (HingeJoint2D joint in hingeJoints)
                {
                    if (joint.autoConfigureConnectedAnchor)
                    {
                        joint.autoConfigureConnectedAnchor = false;
                        joint.connectedBody = clavo2.GetComponent<Rigidbody2D>();
                        //newSeg.GetComponent<RopeSegment>().connectedBelow = clavo2;
                        newSeg.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
        }
    }

    void DestroyRope(GameObject clavo, string nombre, GameObject clavo1 = null)
    {
        try
        {
            Destroy(clavo.transform.parent.Find(nombre).gameObject);
            if(clavo.transform.parent.Find(nombre).gameObject == hangingOn)
            {
                Detach();
                hangingOn = null;
            }
        } catch (NullReferenceException)
        {
            if (clavo1)
            {
                 Destroy(clavo1.transform.parent.Find(nombre).gameObject);
            }
        }
    }

    void CheckPulleyInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.transform.gameObject.tag == "Clavo")
            {
                GameObject current = hit.transform.gameObject;
                if (checkSelected(current))
                {
                    current.GetComponent<ClavoCuerda>().Deselect();
                    clavosSelected--;
                }
                else if (clavosSelected < MaxClavos)
                {
                    pulleySelected[clavosSelected] = current;
                    pulleySelected[clavosSelected].GetComponent<ClavoCuerda>().Select();
                    clavosSelected++;
                }
            }          
        }
    }

    bool checkSelected(GameObject clavo)
    {
        for (int i = 0; i < clavosSelected; i++)
        {
            if (pulleySelected[i] == clavo)
            {
                pulleySelected[i] = null;
                for (int j = i; j < MaxClavos - 1; j++)
                {
                    if (pulleySelected[j + 1] == null)
                    {
                        pulleySelected[j] = null;
                        break;
                    }
                    else
                    {
                        pulleySelected[j] = pulleySelected[j + 1];
                    }
                }
                pulleySelected[MaxClavos - 1] = null;
                return true;
            }
        }
        return false;
    }

    public void DecreaseSelected(GameObject clavo){ 

        if(checkSelected(clavo)){
            clavosSelected--;            
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (!attached)
        {
            if (col.gameObject.tag == "Rope")
            {
               
                if (attachedTo != col.gameObject.transform.parent && !col.gameObject.GetComponent<RopeSegment>().connectedAbove.CompareTag("Clavo"))
                {
                    if (disregard == null || col.gameObject.transform.parent.gameObject != disregard)
                    {
                        hangingOn = col.gameObject.transform.parent.gameObject;
                        Attach(col.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    public void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<RopeSegment>().isPlayerAttached = true;
        hj.connectedBody = ropeBone;
        hj.enabled = true;
        attached = true;
        attachedTo = ropeBone.gameObject.transform.parent;
    }

    void Detach()
    {
        hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
        attached = false;
        hj.enabled = false;
        hj.connectedBody = null;
        StartCoroutine(AttachedNull());
    }

    IEnumerator AttachedNull()
    {

        yield return new WaitForSeconds(0.5f);
        attachedTo = null;

    }

    public void Slide(int direction)
    {
        RopeSegment myConnection = hj.connectedBody.gameObject.GetComponent<RopeSegment>();
        GameObject newSeg = null;
        if (direction > 0)
        {
            if (myConnection.connectedAbove != null && !myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>().connectedAbove.CompareTag("Clavo"))
            {
                if (myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = myConnection.connectedAbove;
                }
            }
        }
        else
        {
            if (myConnection.connectedBelow != null)
            {
                newSeg = myConnection.connectedBelow;
            }
        }
        if (newSeg != null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}
