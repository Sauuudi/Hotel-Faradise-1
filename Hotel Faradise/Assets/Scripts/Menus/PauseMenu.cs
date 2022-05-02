using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public int PauseOptions = 4;
    public int SettingsOptions = 4;
    private bool changingMenu = false;
    private int optionPauseIndex = 100000;
    [SerializeField] private int optionSettingsIndex = 100000;
    private bool settingsOpened = false;
    void Update (){

        if (settingsMenuUI.activeSelf)
        {
            settingsOpened = true;
        }
        else
        {
            settingsOpened = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick2Button3) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Debug.Log("escape");
            if(GameIsPaused){
                if (settingsOpened)
                {
                    settingsMenuUI.SetActive(false);
                }
                Resume();
            }else{
                Pause();
            }
        }
        if (GameIsPaused && !changingMenu)
        {
            if (!settingsOpened)
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
            else
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
            
            
        }
        if(settingsOpened && (Input.GetKey(KeyCode.Joystick2Button0) || Input.GetKey(KeyCode.Joystick2Button0))){
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void mainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    IEnumerator menuDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        changingMenu = false;
    }
}
