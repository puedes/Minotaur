﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float moveSpeed = 3.0f;

	Sprite[] playerSprite;
	SpriteRenderer sr;
	int idleFrame;
	int[] walkCycle;
	float currentFrame;
	bool isRotatingLeft;
	bool isRotatingRight;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<CircleCollider2D> ();
		gameObject.AddComponent<Rigidbody2D> ();
		gameObject.AddComponent<SpriteRenderer> ();

		CircleCollider2D cc = gameObject.GetComponent<CircleCollider2D> ();
		cc.radius = .25f;

		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D> ();
		rb.gravityScale = 0;

		sr = gameObject.GetComponent<SpriteRenderer> ();
		playerSprite = Resources.LoadAll<Sprite> ("Sprites/playerAnimated");
		idleFrame = 2;
		walkCycle = new int[]{0,1,2,3,4,2};
		currentFrame = idleFrame;
		sr.sprite = playerSprite [Mathf.RoundToInt(currentFrame)];

		transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
	}
	
	// Update is called once per frame
	void Update () {

		GameObject door = GameObject.Find("Door");
		if (Mathf.RoundToInt(door.transform.position.x) == Mathf.RoundToInt(gameObject.transform.position.x) && 
		    Mathf.RoundToInt(gameObject.transform.position.y) == Mathf.RoundToInt(door.transform.position.y)) {

		}

		bool moving = false;

		if (Input.GetKey ("a")) {
			transform.Translate (-moveSpeed * Time.deltaTime, 0, 0);
			moving = true;
		}
		if (Input.GetKey ("d")) {
			transform.Translate (moveSpeed * Time.deltaTime, 0, 0);
			moving = true;
		}
		if (Input.GetKey ("s")) {
			transform.Translate (0, -moveSpeed * Time.deltaTime, 0);
			moving = true;
		}
		if (Input.GetKey ("w")) {
			transform.Translate (0, moveSpeed * Time.deltaTime, 0);
			moving = true;
		}

		if (isRotatingLeft) {
			transform.Rotate (0, 0, 120 * Time.deltaTime);
			if (transform.eulerAngles.z % 90 < 1 || transform.eulerAngles.z % 90 > 89) {
				transform.Rotate (0, 0, Mathf.RoundToInt(transform.eulerAngles.z/90)*90 - transform.eulerAngles.z);
				isRotatingLeft = false;
			}
		}

		if (isRotatingRight) {
			transform.Rotate (0, 0, -120 * Time.deltaTime);
			if (transform.eulerAngles.z % 90 < 1 || transform.eulerAngles.z % 90 > 89) {
				transform.Rotate (0, 0, Mathf.RoundToInt(transform.eulerAngles.z/90)*90 - transform.eulerAngles.z);
				isRotatingRight = false;
			}
		}

		if (Input.GetKey ("j")) {
			isRotatingLeft = !isRotatingRight;
		}
		if (Input.GetKey ("l")) {
			isRotatingRight = !isRotatingLeft;
		}

		if (moving) {
			currentFrame += Time.deltaTime * 16;
			sr.sprite = playerSprite [walkCycle [Mathf.RoundToInt(currentFrame) % walkCycle.Length]];
		} else {
			sr.sprite = playerSprite[walkCycle[idleFrame]];
			currentFrame = idleFrame;
		}
	}
}
