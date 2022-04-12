using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey("q") ||
            Input.GetKey(KeyCode.Joystick2Button1) || Input.GetKey("m"))
        {
            //Debug.Log("hola");
            this.GetComponent<Button>().onClick.Invoke();
        }
    }
}
