using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{



    public KeyCode key;
    public KeyCode tp;
    [SerializeField] private GameObject game;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        if (Input.GetKey(key))
        {
            if (Physics.Raycast(game.transform.position, game.transform.forward, out hit, 20f))
            {
                hit.rigidbody.transform.position = new Vector3(55, 100, 52);
            }
        }
        if (Input.GetKey(tp))
        {
            game.transform.position = new Vector3(55, 100, 52);
        }
    }
}
