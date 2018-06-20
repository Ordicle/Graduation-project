using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    private GameObject DialoguePanel;
    private bool IsPausePanel = false;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject person;
    public FirstPersonController personScript;
    public KeyCode PauseButton;

	
	void Start () {
        personScript = person.GetComponent<FirstPersonController>();
	}

    void Update()
    {
        if (Input.GetKeyDown(PauseButton) && !IsPausePanel)
        {
            if (DialogueSystem.IsDialShow)
                DialoguePanel.SetActive(false);
            pausePanel.SetActive(true);
            personScript.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            IsPausePanel = true;
        }

        else if (Input.GetKeyDown(PauseButton) && IsPausePanel)
        {
            ContinueGame();
        }   
     
    }

    public void Continue()
    {
        ContinueGame();
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

    void ContinueGame()
    {       
        IsPausePanel = false;
        pausePanel.SetActive(false);
        if (!DialogueSystem.IsDialShow)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            DialoguePanel.SetActive(false);
            personScript.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            DialoguePanel.SetActive(true);
            personScript.enabled = false;
        }
        Time.timeScale = 1;
    }
}
