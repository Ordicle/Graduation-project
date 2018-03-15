using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private bool IsPausePanel = false;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject person;
    private FirstPersonController personScript;
    public KeyCode PauseButton;

	
	void Start () {
        personScript = person.GetComponent<FirstPersonController>();
	}


    void Update()
    {

        if (Input.GetKeyDown(PauseButton) && !IsPausePanel)
        {
            pausePanel.SetActive(true);
            personScript.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            IsPausePanel = true;
        }

        else if (Input.GetKeyDown(PauseButton) && IsPausePanel)
        {
            IsPausePanel = false;
            pausePanel.SetActive(false);
            personScript.enabled = true;
            Time.timeScale = 1;
        }
    }

        public void Continue()
    {
        IsPausePanel = false;
        pausePanel.SetActive(false);
        personScript.enabled = true;
        Time.timeScale = 1;
    }

         public void Menu()
    {
        IsPausePanel = false;
        pausePanel.SetActive(false);
        personScript.enabled = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

         public void Quit()
    {
        Application.Quit();
    } 
   
}
