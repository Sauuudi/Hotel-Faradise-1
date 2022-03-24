using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabConnor : MonoBehaviour
{
    public Transform _grabDetect;
    public Transform _boxHolder;
    public Animator _anim;

    public float rayDistance;

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(_grabDetect.position, Vector2.right * transform.localScale, rayDistance);

        if(grabCheck.collider != null && grabCheck.collider.tag == "Box")//podemos poner tag de objetos moviles
        {
            if(Input.GetKey(KeyCode.C))
            {
                _anim.SetBool("grabbing", true);
                grabCheck.collider.gameObject.transform.parent = _boxHolder;
                grabCheck.collider.gameObject.transform.position = _boxHolder.position;
                //Physhics off
                //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                _anim.SetBool("grabbing", false);
                grabCheck.collider.gameObject.transform.parent = null;
                //Physhics on
                //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }
}
