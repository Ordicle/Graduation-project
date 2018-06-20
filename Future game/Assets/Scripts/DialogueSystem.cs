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
    public KeyCode InpNextBtn;

    [SerializeField] private GameObject AnswerTable;
    [SerializeField] private GameObject raycast;
    [SerializeField] private Text text_interface;

    [HideInInspector]
    public int i = 0;
    private int dialInt;
    private PauseMenu pauseMenu;

    [HideInInspector]
    public static bool IsDialShow;
    private bool raydialogue;

    private Text SkipBtnText;

    public List<DialogueAsset> AssetHuman;

    [HideInInspector]
    public DialogueSettings dialogueSetting;
    RaycastHit hit;
    public GameObject AnsContent;

    private int NumAsset;

    private CreateAnswers createAnswers;

    void Awake()
    {
        Time.timeScale = 1;
    }
	void Start ()
    {
        SkipBtnText = SkipButton.GetComponentInChildren<Text>();
        AnswerTable.SetActive(false);
        createAnswers = AnsContent.GetComponent<CreateAnswers>();
        DialoguePanel.SetActive(false);
        press_dialogue.enabled = false;
        text_interface.enabled = false;
        pauseMenu = GetComponent<PauseMenu>();
            
        SkipBtnText.text = $"Next[{InpNextBtn}]";

    }   
	
    void FixedUpdate()
    {
        if (Physics.Raycast(raycast.transform.position, raycast.transform.forward, out hit, 4f, 1 << 8))
        {            
            //if(hit.collider.gameObject.layer == LayerMask.NameToLayer("sfsgs"))
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
                    AnswerTable.SetActive(false);
                    SkipButton.SetActive(true);
                    StopAllCoroutines();
                    StartCoroutine(TextShowCorutine(dialogueSetting.node[i].text_dialogue));
                }
                else
                {
                    AnswerTable.SetActive(true);
                    SkipButton.SetActive(false);
                    createAnswers.ShowAnswer();
                }
            }
        }

        else
        { 
            press_dialogue.enabled = false;
        }
        
        if(SkipButton.activeSelf && Input.GetKeyDown(InpNextBtn))
        {
            NextB();
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
                AnswerTable.SetActive(false);
                SkipButton.SetActive(true);      
            }

            else
            {
                AnswerTable.SetActive(false); // <-- не трогать это
                AnswerTable.SetActive(true);
                SkipButton.SetActive(false);
                createAnswers.ShowAnswer();
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
