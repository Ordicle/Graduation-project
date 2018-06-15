using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    [HideInInspector]
    public int NumButton;
    [HideInInspector]
    public int curI;
    [HideInInspector]
    public string end;
    private DialogueSystem ds;



    void Start ()
    {
        ds = GetComponentInParent<DialogueSystem>();
	}

	public void _next()
    {
        ds.dialogueSetting.node[ds.i].answers[NumButton].SelectAns = "true";
        SaveInfo.IsClickAnswer = true;
        ds.NextMethod(curI, end);
        
    }   
}
