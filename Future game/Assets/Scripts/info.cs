using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info : MonoBehaviour {

        public GameObject panel;
        public KeyCode key;
        [SerializeField] private GameObject game;
        void Start()
        {
        }

        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(game.transform.position, game.transform.forward, out hit, 20f, 1 << 10))
            {
            panel.transform.position = game.transform.position;
            }
        }
    }
