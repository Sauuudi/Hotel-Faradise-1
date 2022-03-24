using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public HingeJoint2D hj;

    public float pushForce = 10f;

    public bool attached = false;
    public Transform attachedTo;
    private GameObject disregard;

    public GameObject pulleySelected = null;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        hj = gameObject.GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyboardInputs();
        CheckPulleyInputs();
    }
/*      KEYBOARD CONTROLS
 * a or left arrow  = swing left on rope
 * d or right arrow = swing right on rope
 * space            = detach from rope
 * w or up arrow    = climb up rope
 * s or down arrow  = climb down rope
 */

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

        if((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && attached)
        {
            Slide(1);
        }

        if ((Input.GetKeyDown("s") || Input.GetKeyDown("down")) && attached)
        {
            Slide(-1);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Detach();
        }
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
        //hj.connectedBody.gameObject.GetComponent<RopeSegment>().isPlayerAttached = false;
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
        if(direction > 0)
        {
            if(myConnection.connectedAbove != null)
            {
                if(myConnection.connectedAbove.gameObject.GetComponent<RopeSegment>() != null)
                {
                    newSeg = myConnection.connectedAbove;
                }
            }
        }
        else
        {
            if(myConnection.connectedBelow != null)
            {
                newSeg = myConnection.connectedBelow;
            }
        }
        if(newSeg != null)
        {
            transform.position = newSeg.transform.position;
            myConnection.isPlayerAttached = false;
            newSeg.GetComponent<RopeSegment>().isPlayerAttached = true;
            hj.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!attached)
        {
            if(col.gameObject.tag == "Rope")
            {
                if(attachedTo != col.gameObject.transform.parent)
                {
                    if(disregard == null || col.gameObject.transform.parent.gameObject != disregard)
                    {
                        Attach(col.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
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
            if(hit.collider != null && hit.transform.gameObject.tag == "Crank")
            {
                if(pulleySelected != hit.transform.gameObject)
                {
                    if(pulleySelected != null)
                    {
                        pulleySelected.GetComponent<Crank>().Deselect();
                    }
                    pulleySelected = hit.transform.gameObject;
                    pulleySelected.GetComponent<Crank>().Select();
                }
                else if (pulleySelected == hit.transform.gameObject)
                {
                    pulleySelected.GetComponent<Crank>().Deselect();
                    pulleySelected = null;
                }
            }
            else
            {
                if(pulleySelected != null)
                {
                    pulleySelected.GetComponent<Crank>().Deselect();
                    pulleySelected = null;
                }
            }
        }
        if(Input.GetKeyDown("f")&&pulleySelected != null)
        {
            pulleySelected.GetComponent<Crank>().Rotate(1);
        }

        if (Input.GetKeyDown("r") && pulleySelected != null)
        {
            pulleySelected.GetComponent<Crank>().Rotate(-1);
        }
    }
}
