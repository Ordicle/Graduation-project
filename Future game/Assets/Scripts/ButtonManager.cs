using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    public int curI;
    public string end;
    private DialogueSystem ds;

    void Start ()
    {
        ds = GetComponentInParent<DialogueSystem>();
	}

	void Update ()
    {
        ds.NextMethod(curI, end);
    }
}
