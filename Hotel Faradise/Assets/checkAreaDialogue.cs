using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAreaDialogue : MonoBehaviour
{
    public YarnInteractable dialogueToActivate;
    public GameObject buttonA;
    public bool inK = false;
    public bool inC = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogueToActivate.DisableConversation();
        buttonA.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inC)
        {
            dialogueToActivate.EnableConversation();
            buttonA.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Joystick2Button1) || Input.GetKeyDown("q"))
            {
                dialogueToActivate.StartConversation();
            }
        } else if (inK)
        {
            dialogueToActivate.EnableConversation();
            buttonA.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown("m"))
            {
                dialogueToActivate.StartConversation();
            }
        }
        else
        {
            dialogueToActivate.DisableConversation();
            buttonA.SetActive(false);
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
