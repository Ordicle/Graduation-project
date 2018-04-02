using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {


    public KeyCode key;
   [SerializeField] private GameObject game;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        if (Input.GetKeyDown(key))
        {
            if (Physics.Raycast(game.transform.position, Vector3.forward,out hit,20f, 1 << 9))
            {
                Debug.Log(hit.point);
            }
        }

	}
}
