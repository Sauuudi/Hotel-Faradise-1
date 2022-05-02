using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAreaDialogue : MonoBehaviour
{
    public YarnInteractable dialogueToActivate;
    public bool inK = false;
    public bool inC = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueToActivate.DisableConversation();
    }

    // Update is called once per frame
    void Update()
    {
        if (inC && inK)
        {
            dialogueToActivate.EnableConversation();
        }
        else
        {
            dialogueToActivate.DisableConversation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Kate"))
        {
            inK = true;

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Connor"))
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
