﻿using UnityEngine;
using System.Collections;

public class Door : Tile {

	public Door(int dir): base() {
		go = new GameObject ("Door");
		go.AddComponent<SpriteRenderer> ();
		
		Sprite s = Resources.Load<Sprite> ("Sprites/door"+dir);
		go.GetComponent<SpriteRenderer>().sprite = s;

		go.AddComponent<BoxCollider2D> ();
		go.GetComponent<BoxCollider2D> ().isTrigger = true;
	}

	void onTriggerEnter2D(Collider collisionInfo) {
		Application.LoadLevel ("Title");
	}
}
