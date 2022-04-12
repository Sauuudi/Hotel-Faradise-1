using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class toNextLevel : MonoBehaviour
{
    public Transform Mask;
    public GameObject Circle;
    private bool pressC = false;
    private bool pressK = false;
    private float totalAmount = 0f;
    private bool onCoroutine = false;
    private Material mySpriteMaterial;
    private float originalX;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteMaterial = Circle.GetComponent<SpriteRenderer>().material;
        originalX = Mask.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        totalAmount = 0f;
        if (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey("q"))
        {
            pressK = true;
            totalAmount += 0.5f;
        }
        else
        {
            mySpriteMaterial.SetFloat("_FillAmount", 0f);
            //Circle.fillAmount = 0f;
            pressK = false;
        }
        if (Input.GetKey(KeyCode.Joystick2Button1) || Input.GetKey("m"))
        {
            pressC = true;
            totalAmount += 0.5f;
        }
        else
        {
            mySpriteMaterial.SetFloat("_FillAmount", 0f);
            //Circle.fillAmount = 0f;
            pressC = false;
        }
        switch (totalAmount)
        {
            case (0.5f):
                Mask.position = new Vector3(originalX + 0.205f, Mask.position.y, Mask.position.z);
                break;
            case (1f):
                Mask.position = new Vector3(originalX + 0.4f, Mask.position.y, Mask.position.z);
                break;
            default:
                Mask.position = new Vector3(originalX, Mask.position.y, Mask.position.z);
                break;
        }
        //Mask.transform.position = totalAmount;
        if (pressK && pressC && !onCoroutine)
        {
            StartCoroutine(CompleteCircle());
        }
    }

    IEnumerator CompleteCircle()
    {
        onCoroutine = true;
        bool release = false;
        while (!release && pressC && pressK && mySpriteMaterial.GetFloat("_FillAmount") < 1f)
        {
            if(pressC && pressK)
            {
                mySpriteMaterial.SetFloat("_FillAmount", mySpriteMaterial.GetFloat("_FillAmount") + Time.deltaTime);
                //Circle.fillAmount += Time.deltaTime;
                yield return null;
            } else
            {
                release = true;
                break;
            }
            
        }
        onCoroutine = false;
        if (!release && mySpriteMaterial.GetFloat("_FillAmount") >= 1) SceneManager.LoadScene(3);
    }
}
