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
        if (Input.GetKey(key))
        {
            if (Physics.Raycast(game.transform.position, game.transform.forward ,out hit,20f, 1 << 9))
            {
                hit.rigidbody.transform.Rotate(45, 10, 0);         
            }
        }      


    }
}
