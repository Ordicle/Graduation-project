using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    [SerializeField]
    private GameObject DialoguePanel;
    public GameObject SkipButton;
    public Text press_dialogue;
    public KeyCode dialogueShow;

    [SerializeField] private GameObject raycast;
    [SerializeField] private Text text_interface;
    [SerializeField] private GameObject[] buttons;

    [HideInInspector]
    public int i = 0;
    private int dialInt;
    private PauseMenu pauseMenu;

    [HideInInspector]
    public static bool IsDialShow;
    private bool raydialogue;


    public List<DialogueAsset> AssetHuman;

    [HideInInspector]
    public DialogueSettings dialogueSetting;
    RaycastHit hit;

    private int NumAsset;

    void Awake()
    {
        Time.timeScale = 1;
    }
	void Start ()
    {

        DialoguePanel.SetActive(false);

        for (int k = 0; k < buttons.Length; k++)
        {
            buttons[k].SetActive(false);
        }

        press_dialogue.enabled = false;
        text_interface.enabled = false;
        pauseMenu = GetComponent<PauseMenu>();

    }   
	
    void FixedUpdate()
    {
        if (Physics.Raycast(raycast.transform.position, raycast.transform.forward, out hit, 4f, 1 << 8))
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
            if (Input.GetKeyDown(dialogueShow) && !IsDialShow)
            {
                DialoguePanel.SetActive(true);

                for (int n = 0; n < AssetHuman.Count; n++)
                {
                   if (hit.collider.name == AssetHuman[n].NameHuman)
                    {
                        NumAsset = n;
                        dialogueSetting = DialogueSettings.Load(AssetHuman[n].asset);
                    }
                }

                i = AssetHuman[NumAsset].CurNode;

                IsDialShow = true;
                pauseMenu.personScript.enabled = false;
                text_interface.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (dialogueSetting.node[i].IsSentence)
                {

                    for (int k = 0; k < buttons.Length; k++)
                    {
                        buttons[k].SetActive(false);
                    }

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
                        buttons[j].GetComponent<ButtonManager>().end = "";
                        buttons[j].GetComponentInChildren<Text>().text = dialogueSetting.node[i].answers[j].anstext;
                        buttons[j].GetComponent<ButtonManager>().curI = dialogueSetting.node[i].answers[j].NValue;
                        buttons[j].GetComponent<ButtonManager>().NumButton = j;
                    }
                }
            }
        }

        else
        { 
            press_dialogue.enabled = false;
        }
        
    }

    public void NextMethod(int nexNode,string end)
    {      
        if (i < dialogueSetting.node.Length - 1)
        {
            i++;

            if (SaveInfo.IsClickAnswer)
                i = nexNode;

            if (dialogueSetting.node[i].IsSentence)
            {
                
                for (int k = 0; k < buttons.Length; k++)
                 {
                     buttons[k].SetActive(false);
                 }

                 SkipButton.SetActive(true);
                   
            }

            else
            {

                SkipButton.SetActive(false);

                for (int j = 0; j < dialogueSetting.node[i].answers.Length; j++)
                {
                    buttons[j].SetActive(true);
                    buttons[j].GetComponent<ButtonManager>().end = "";
                    buttons[j].GetComponentInChildren<Text>().text = dialogueSetting.node[i].answers[j].anstext;
                    buttons[j].GetComponent<ButtonManager>().curI = dialogueSetting.node[i].answers[j].NValue;
                    buttons[j].GetComponent<ButtonManager>().NumButton = j;
                }             

            }

            StopAllCoroutines();
            StartCoroutine(TextShowCorutine(dialogueSetting.node[i].text_dialogue));

        }

        else
        {
            IsDialShow = false;
            pauseMenu.personScript.enabled = true;
            text_interface.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            SkipButton.SetActive(!SkipButton.activeSelf);
            DialoguePanel.SetActive(false);
        }

        AssetHuman[NumAsset].CurNode = i;

    }

    public void NextB()
    {
        SaveInfo.IsClickAnswer = false;
        NextMethod(0, "");
        
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
