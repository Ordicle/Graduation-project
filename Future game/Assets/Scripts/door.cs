using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour {

    [SerializeField] private GameObject FPS;
    [SerializeField] private RaycastHit petl;
    public KeyCode key;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Input.GetKey(key))
        {
            if (Physics.Raycast(FPS.transform.position, FPS.transform.forward, out hit, 20f, 1 << 11))
            {
                petl.rigidbody.transform.rotation = new Quaternion (2,0, 90, 0);
            }
        }
    }
}
