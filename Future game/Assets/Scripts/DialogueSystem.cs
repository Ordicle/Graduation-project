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
    [SerializeField] private GameObject[] buttons;

    private int i;
    private int dialInt;
    private PauseMenu pauseMenu;

    private bool raydialogue;

    DialogueSettings dialogueSetting;

    void Awake()
    {
        Time.timeScale = 1;
    }
	void Start ()
    {
        press_dialogue.enabled = false;
        text_interface.enabled = false;
        pauseMenu = GetComponent<PauseMenu>();
        i = 0;
        dialogueSetting = DialogueSettings.Load(textAssets[0]);
           
      
    }   
	
    void FixedUpdate()
    {
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
                if (dialogueSetting.node[i].IsSentence)
                {
                    SkipButton.SetActive(true);
                    StopAllCoroutines();
                    StartCoroutine(TextShowCorutine(dialogueSetting.node[i].text_dialogue));
                }
                else
                {
                    SkipButton.SetActive(false);

                    for(int j = 0; j < dialogueSetting.node[i].answers.Length; j++)
                    {
                        buttons[j].SetActive(true);
                        buttons[j].GetComponentInChildren<Text>().text = dialogueSetting.node[i].answers[j].anstext;

                    }
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
        if(i!=dialogueSetting.node.Length - 1)
        {
            i++;
        }
        StartCoroutine(TextShowCorutine(dialogueSetting.node[i].text_dialogue));
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
