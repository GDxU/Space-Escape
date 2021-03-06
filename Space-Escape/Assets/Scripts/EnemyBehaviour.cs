﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour 
{
	public GameObject EnemyLaser;
	public float health = 150f;
	public float enemyLaserSpeed = 10f;
	public float shotsPerSeconds = 0.5f;
	public int scoreValue = 150;
	private ScoreKepper scoreKeeper;

	//Enemy sound
	public AudioClip fireSound;
	public AudioClip deathSound;

	void Start()
	{
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKepper>();
	}
	void Update()
	{
		float probability = Time.deltaTime * shotsPerSeconds;

		if(Random.value < probability)
		{
			Fire();
		}
	}

	void Fire()
	{
		//Vector3 startPos = transform.position + new Vector3(0, -1, 0);
		GameObject enemyMissile = Instantiate(EnemyLaser, transform.position, Quaternion.identity) as GameObject;

		//Adding velocity.
		enemyMissile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);

		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		//Debug.Log(collider);
		Laser missile = collider.gameObject.GetComponent<Laser>();

		if(missile)
		{
			health -= missile.getDamage();

			missile.Hit();

			if(health <= 0)
			{
				EnemyDeath();
			}
		}
	}

	void EnemyDeath()
	{
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
		
}
