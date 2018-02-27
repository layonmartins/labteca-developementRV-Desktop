using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutImage : MonoBehaviour {

    private Image img;
    private float time;
   

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        time = 0;
    }

    void Update()
    {
        if(time == 0)
            StartCoroutine(FadeInOut());

        time += 1 * Time.deltaTime;

        if(time > 3f)
        {
            StopCoroutine(FadeInOut());
            time = 0;
        }
        
    }

    IEnumerator FadeInOut()
    {
        while (true)
        {
            img.CrossFadeAlpha(0, 1, false);
            yield return new WaitForSeconds(1);
            img.CrossFadeAlpha(1, 1, false);
            yield return new WaitForSeconds(2);
        }
    }


	
}
