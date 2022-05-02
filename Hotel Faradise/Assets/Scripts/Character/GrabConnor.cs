using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabConnor : MonoBehaviour
{
    public Transform _grabDetect;
    public Transform _boxHolder;
    public Animator _anim;
    private bool grabbing = false;
    public float rayDistance;
    private GameObject boxTaken;
    private Rigidbody2D body;
    private bool aiming = false;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick2Button5))
        {
            aiming = true;
        } else
        {
            aiming = false;
        }
            RaycastHit2D grabCheck = Physics2D.Raycast(_grabDetect.position, Vector2.right * transform.localScale, rayDistance);

        if(grabCheck.collider != null && grabCheck.collider.tag == "Box") //tag de objetos moviles
        {
            if(Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Joystick2Button2) && !aiming)
            {
                if (!grabbing)
                {
                    boxTaken = grabCheck.collider.gameObject;
                    grabbing = true;
                    _anim.SetBool("grabbing", true);
                    boxTaken.transform.parent = _boxHolder;

                    //boxTaken.transform.position = _boxHolder.position;
                    boxTaken.GetComponent<HingeJoint2D>().connectedBody = body;
                    boxTaken.GetComponent<HingeJoint2D>().enabled = true;
                    this.GetComponent<MovementConnor>().jumpForce = this.GetComponent<MovementConnor>().jumpForce + 18;

                    boxTaken.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    //grabCheck.collider.gameObject.transform.parent = _boxHolder;
                    //grabCheck.collider.gameObject.transform.position = _boxHolder.position;
                    //Physhics off
                    //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                }
            } /*
            else
            {
                grabbing = false;
                _anim.SetBool("grabbing", false);
                boxTaken.transform.parent = null;
                //grabCheck.collider.gameObject.transform.parent = null;
                //Physhics on
                //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            } */
        }

        if(grabbing && !(Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Joystick2Button2)))
        {
            this.GetComponent<MovementConnor>().jumpForce = this.GetComponent<MovementConnor>().jumpForce - 18;
            grabbing = false;
            _anim.SetBool("grabbing", false);
            boxTaken.transform.parent = null;
            boxTaken.GetComponent<HingeJoint2D>().enabled = false;
            boxTaken.GetComponent<HingeJoint2D>().connectedBody = null;
            boxTaken.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //boxTaken.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }
}
