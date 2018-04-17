using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkStation : MonoBehaviour {

    public GameObject panel;
    public Text text;
    public KeyCode key;
    [SerializeField] private GameObject game;
    void Start () {
        text = text.GetComponent<Text>();
	}
	
	void Update () {
        text.text = "Нажмите"+key;
        RaycastHit hit;
        if (Physics.Raycast(game.transform.position, game.transform.forward, out hit, 20f, 1 << 10))
            {
                panel.SetActive(true);
            }
        else
        {
            panel.SetActive(false);
        }
        if (Input.GetKeyDown(key))
        {

        }
    }
}
