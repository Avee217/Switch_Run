using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour {

	public float speed;

	Vector2 bgPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		MoveBg ();
	}

	void MoveBg()
	{
		bgPos = new Vector2(Time.time * speed, 0);
		GetComponent<Renderer> ().material.mainTextureOffset = bgPos;
	}
}
