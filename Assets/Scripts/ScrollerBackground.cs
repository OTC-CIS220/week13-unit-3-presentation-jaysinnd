using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerBackground : MonoBehaviour {

    public float backgroundScrollSpeed = 0.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = new Vector2(Time.time * backgroundScrollSpeed, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
