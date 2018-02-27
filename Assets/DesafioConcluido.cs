using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesafioConcluido : MonoBehaviour {
    public GameObject concluido;
    public Canvas concluidoCanvas;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<TextMesh>().text == "7.00")
        {
            concluidoCanvas.GetComponent<CanvasGroup>().alpha = 1f;
            concluido.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
