using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClavoCuerda : MonoBehaviour

{

    private Transform selected;
    public bool isRoped = false;
    public bool isDoubleRoped = false;
    public GameObject hangingPlatform;
    public bool isHangingPlatform;

    private void Awake()
    {
        selected = transform.Find("Selected");
    }
    // Start is called before the first frame update
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
