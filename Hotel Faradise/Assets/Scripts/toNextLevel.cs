using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class toNextLevel : MonoBehaviour
{
    public Image Mask;
    public Image Circle;
    private bool pressC = false;
    private bool pressK = false;
    private float totalAmount = 0f;
    private bool onCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        totalAmount = 0f;
        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            pressK = true;
            totalAmount += 0.5f;
        }
        else
        {
            Circle.fillAmount = 0f;
            pressK = false;
        }
        if (Input.GetKey(KeyCode.Joystick2Button1))
        {
            pressC = true;
            totalAmount += 0.5f;
        }
        else
        {
            Circle.fillAmount = 0f;
            pressC = false;
        }
        Mask.fillAmount = totalAmount;
        if (pressK && pressC && !onCoroutine)
        {
            StartCoroutine(CompleteCircle());
        }
    }

    IEnumerator CompleteCircle()
    {
        onCoroutine = true;
        bool release = false;
        while (!release && pressC && pressK && Circle.fillAmount < 1f)
        {
            if(pressC && pressK)
            {
                Circle.fillAmount += Time.deltaTime;
                yield return null;
            } else
            {
                release = true;
                break;
            }
            
        }
        onCoroutine = false;
        if (!release && Circle.fillAmount >= 1) SceneManager.LoadScene(3);
    }
}
