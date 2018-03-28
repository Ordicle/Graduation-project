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
        i = 0;
        dialogueSettings = DialogueSettings.Load(textAsset);
       
    }   
	

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StopAllCoroutines();
            StartCoroutine(TextShowCorutine(dialogueSettings.node[i].text_dialogue));
        }

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
