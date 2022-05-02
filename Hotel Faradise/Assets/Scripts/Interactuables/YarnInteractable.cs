
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    public string conversationStartNode;
    private DialogueRunner dialogueRunner;
    private bool isCurrentConversation;
    private bool dialogueThrown = false;
    public GameObject activateAfterDialogue = null;
    public void Start() {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }
    public void StartConversation() {
        //Time.timeScale = 0f;
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
    }
    private void EndConversation(){
    
            if (isCurrentConversation) { 
               // Time.timeScale = 1f;
            isCurrentConversation = false;
            }
    }

    public void DisableConversation(){
        interactable=false;
    }

    public void EnableConversation()
    {
        interactable = true;
    }

    // whether this character should be enabled right now
    // (begins true, but may not always be true)
    private bool interactable = true; 

    public void OnMouseDown() {
        // if this character is enabled and no conversation is already running
        if (interactable && !dialogueRunner.IsDialogueRunning) {
            // then run this character's conversation
            StartConversation();
            dialogueThrown = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D col){
       // if(Toca a Kate o connor){
            if (interactable && !dialogueRunner.IsDialogueRunning) {
                // then run this character's conversation
                StartConversation();
                DisableConversation();
                dialogueThrown = true;
        }
       // }
    }

    public void activateObjectAfterDialogue()
    {
        if(dialogueThrown && activateAfterDialogue != null)
        {
            activateAfterDialogue.SetActive(true);
        }
    }
}