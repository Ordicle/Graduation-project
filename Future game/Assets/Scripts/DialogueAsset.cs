using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAsset : MonoBehaviour {

    public TextAsset asset;

    private string nameHuman;
    [HideInInspector]
    public int CurNode;

    public string NameHuman
    {
        get
        {
            return nameHuman = gameObject.name;
        }
        set
        {
            value = nameHuman;
        }
    }

    DialogueSystem assetD;

    void Start () {

        assetD = GameObject.Find("Interface").GetComponent<DialogueSystem>();
        assetD.AssetHuman.Add(this); 
	}
}
