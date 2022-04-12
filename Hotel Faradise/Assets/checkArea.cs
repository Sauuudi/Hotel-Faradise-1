using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkArea : MonoBehaviour
{
    public GameObject nextLevelPanel;
    public bool inK = false;
    public bool inC = false;
    // Start is called before the first frame update
    void Start()
    {
        nextLevelPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(inC && inK)
        {
            nextLevelPanel.SetActive(true);
        } else
        {
            nextLevelPanel.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Kate"))
        {
            inK = true;

        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Connor"))
        {
            inC = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Kate"))
        {
            inK = false;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Connor"))
        {
            inC = false;
        }
    }
}
