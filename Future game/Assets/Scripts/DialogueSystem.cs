using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public GameObject SkipButton;
    public Text press_dialogue;
    public KeyCode dialogueShow;

    [SerializeField] private GameObject raycast;
    [SerializeField] private TextAsset[] textAssets;
    [SerializeField] private Text text_interface;

    private int i;
    private int dialInt;
    private PauseMenu pauseMenu;

    private bool raydialogue;

    DialogueSettings[] dialogueSettings;

	void Start ()
    {
        dialogueSettings = new DialogueSettings[textAssets.Length];
        press_dialogue.enabled = false;
        text_interface.enabled = false;
        pauseMenu = GetComponent<PauseMenu>();
        i = 0;
        dialInt = 0;
        foreach (TextAsset t in textAssets)
        {
            dialogueSettings[dialInt] = DialogueSettings.Load(textAssets[dialInt]);
            dialInt++;
        }
    }   
	
    void FixedUpdate()
    {
        if (dialogueSettings.Length != textAssets.Length)
            Debug.Log("GG");
        if (Physics.Raycast(raycast.transform.position, transform.forward, 4f, 1 << 8))
        {
            raydialogue = true;
        }
        else
        {
            raydialogue = false;
        }
    }

	void Update ()
    {

        if (raydialogue)
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
                if (dialogueSettings[dialInt].node[i].IsSentence)
                {
                    SkipButton.SetActive(true);
                    StopAllCoroutines();
                    StartCoroutine(TextShowCorutine(dialogueSettings[dialInt].node[i].text_dialogue));
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
        if(i!=dialogueSettings[dialInt].node.Length - 1)
        {
            i++;
        }
        StartCoroutine(TextShowCorutine(dialogueSettings[dialInt].node[i].text_dialogue));
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
