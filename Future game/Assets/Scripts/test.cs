using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{



    public KeyCode key;
    public KeyCode tp;
    [SerializeField] private GameObject game;
    [Range(0,10)]
    public int up = 10;


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
                hit.rigidbody.transform.position = new Vector3(hit.rigidbody.transform.position.x, hit.rigidbody.transform.position.y + up, hit.rigidbody.transform.position.z);
            }
        }
        if (Input.GetKey(tp))
        {
            game.transform.position = new Vector3(game.transform.position.x, game.transform.position.y +up, game.transform.position.z);
        }
    }
}
