using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClavoCuerdaObject : MonoBehaviour

{

    private Transform selected;
    public bool isRoped = false;
    public bool isDoubleRoped = false;
    private HingeJoint2D hj;
    private Rigidbody2D rb;
    private void Start()
    {
        selected = transform.Find("Selected");
        rb = gameObject.GetComponent<Rigidbody2D>();
        hj = gameObject.GetComponent<HingeJoint2D>();
    }
    public void Select()
    {
        selected.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        selected.gameObject.SetActive(false);
    }

    public void changeRoped()
    {
        isRoped = !isRoped;
    }

    public void changeDoubleRoped()
    {
        isDoubleRoped = !isDoubleRoped;
    }
}
