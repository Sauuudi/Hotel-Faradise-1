using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuMandos : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject controlesMenuUI;
    public int PauseOptions = 4;
    public int SettingsOptions = 4;
    public int ControlsOptions = 4;
    private bool changingMenu = false;
    private int optionPauseIndex = 100000;
    private int optionSettingsIndex = 100000;
    private int optionControlesIndex = 100000;
    private bool settingsOpened = false;
    private bool controlsOpened = false;
    void Update (){

        if (settingsMenuUI.activeSelf)
        {
            settingsOpened = true;
        }
        else
        {
            settingsOpened = false;
        }

        if (controlesMenuUI.activeSelf)
        {
            controlsOpened = true;
        }
        else
        {
            controlsOpened = false;

        }
        if (!changingMenu)
        {
            if (!settingsOpened && !controlsOpened)
            {
                if (Input.GetAxis("Vertical joyconR joystick") != 0 || Input.GetAxis("Vertical joyconL joystick") != 0)
                {
                    float verticalAxis1 = Input.GetAxisRaw("Vertical joyconR joystick");
                    float verticalAxis2 = Input.GetAxisRaw("Vertical joyconL joystick");
                    optionPauseIndex += (int)verticalAxis1 * -1 + (int)verticalAxis2 * -1;
                    pauseMenuUI.transform.GetChild(0).transform.GetChild(Mathf.Abs(optionPauseIndex % PauseOptions)).gameObject.GetComponent<Button>().Select();
                    changingMenu = true;
                    StartCoroutine(menuDelay(0.2f));
                }
            }
            else if(!controlsOpened)
            {
                if (Input.GetAxis("Vertical joyconR joystick") != 0 || Input.GetAxis("Vertical joyconL joystick") != 0)
                {
                    float verticalAxis1 = Input.GetAxisRaw("Vertical joyconR joystick");
                    float verticalAxis2 = Input.GetAxisRaw("Vertical joyconL joystick");
                    optionSettingsIndex += (int)verticalAxis1 * -1 + (int)verticalAxis2 * -1;
                    if(optionSettingsIndex % SettingsOptions < 2)
                    {
                        settingsMenuUI.transform.GetChild(Mathf.Abs(optionSettingsIndex % SettingsOptions)).gameObject.GetComponent<Slider>().Select();
                    } else
                    {
                        settingsMenuUI.transform.GetChild(Mathf.Abs(optionSettingsIndex % SettingsOptions)).gameObject.GetComponent<Button>().Select();
                    }
                    
                    changingMenu = true;
                    StartCoroutine(menuDelay(0.2f));
                }
            }
            else
            {
                if (Input.GetAxis("Vertical joyconR joystick") != 0 || Input.GetAxis("Vertical joyconL joystick") != 0)
                {
                    float verticalAxis1 = Input.GetAxisRaw("Vertical joyconR joystick");
                    float verticalAxis2 = Input.GetAxisRaw("Vertical joyconL joystick");
                    optionControlesIndex += (int)verticalAxis1 * -1 + (int)verticalAxis2 * -1;
                    controlesMenuUI.transform.GetChild(Mathf.Abs(optionControlesIndex % ControlsOptions)).gameObject.GetComponent<Button>().Select();
                    changingMenu = true;
                    StartCoroutine(menuDelay(0.2f));
                }
            }
            
            
        }
        if(settingsOpened && (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick2Button0))){
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
    }


    IEnumerator menuDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        changingMenu = false;
    }
}
