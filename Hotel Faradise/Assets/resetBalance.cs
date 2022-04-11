using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetBalance : MonoBehaviour
{

    private Vector3 initialPos;
    [SerializeField]
    private int notColliding;
    private bool restart = false;
    public GameObject upperPlatform;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        upperPlatform.GetComponent<resetAllBalance>().increaseCol(id);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        upperPlatform.GetComponent<resetAllBalance>().decreaseCol(id);
    }
}
