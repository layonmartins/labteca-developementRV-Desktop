using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEforInteractionOnDoor : MonoBehaviour {

    private GameObject player;
    private GameObject displayE;
    private float seconds;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        displayE = GameObject.Find("DisplayE");
        displayE.GetComponent<Text>().enabled = false;
        seconds = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(player.transform.position, transform.position) < 0.7f)
        {
            displayE.GetComponent<Text>().enabled = true;
        }

        seconds += Time.deltaTime;
        if (seconds > 0.5f)
        {
            displayE.GetComponent<Text>().enabled = false;
            seconds = 0;
        }

    }
}
