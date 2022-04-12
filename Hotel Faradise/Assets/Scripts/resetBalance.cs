using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetBalance : MonoBehaviour
{

    public GameObject upperPlatform;
    public int id;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        upperPlatform.GetComponent<resetAllBalance>().increaseCol(id);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        upperPlatform.GetComponent<resetAllBalance>().decreaseCol(id);
    }
}
