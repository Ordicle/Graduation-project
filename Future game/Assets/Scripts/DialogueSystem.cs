using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public GameObject SkipButton;
    public Text press_dialogue;
    public KeyCode dialogueShow;

    [SerializeField] private GameObject raycast;
    [SerializeField] private TextAsset textAsset;
    [SerializeField] private Text text_interface;

    private Ray ray;
    private int i;
    private PauseMenu pauseMenu;

    DialogueSettings dialogueSettings;
	void Start ()
    {
        press_dialogue.enabled = false;
        text_interface.enabled = false;
        pauseMenu = GetComponent<PauseMenu>();
        i = 0;
        dialogueSettings = DialogueSettings.Load(textAsset);
      
    }   
	

	void Update ()
    {
        ray = new Ray(raycast.transform.position, transform.forward);
        if (Physics.Raycast(ray, 4f,1 << 8))
        {
            if (pauseMenu.personScript.enabled)
            {
                press_dialogue.enabled = true;
            }
            else
            {
                press_dialogue.enabled = false;
            }
            if (Input.GetKeyDown(dialogueShow))
            {           
                pauseMenu.personScript.enabled = false;
                text_interface.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                if (dialogueSettings.node[i].IsSentence)
                {
                    SkipButton.SetActive(true);
                    StopAllCoroutines();
                    StartCoroutine(TextShowCorutine(dialogueSettings.node[i].text_dialogue));
                }
            }
      
        }
        else
        {
            press_dialogue.enabled = false;
        }


    }

    public void NextSentence()
    {
        if(i!=dialogueSettings.node.Length - 1)
        {
            i++;
        }
        
        StartCoroutine(TextShowCorutine(dialogueSettings.node[i].text_dialogue));
    }


    IEnumerator TextShowCorutine(string dialogue)
    {
        text_interface.text = "";
        foreach (char ch in dialogue.ToCharArray())
        {       
            text_interface.text += ch;
            yield return null;
        }
 
    }

}
