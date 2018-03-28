using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public TextAsset textAsset;
    public Text text_interface;
    private int i;

    DialogueSettings dialogueSettings;
	void Start ()
    {

        dialogueSettings = DialogueSettings.Load(textAsset);
        text_interface.text = dialogueSettings.node[0].text_dialogue;
       
    }

    void TextShow()
        {
            text_interface.text = dialogueSettings.node[i].text_dialogue;
         
        }
   
	

	void Update () {
        for (i = 0; i < dialogueSettings.node.Length - 1; i++)
        {
            Invoke("TextShow", 2);

        }
    }

}
