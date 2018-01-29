using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
	{
		public GameObject target;
		public float speed = 3f;
		public float attack1Range = 1f;
		public int attack1Damage = 1;
		public float timeBetweenAttacks;


		// Use this for initialization
		void Start ()
		{
			Rest ();
		}

		// Update is called once per frame
		void Update ()
		{

		}

		public void MoveToPlayer ()
		{
			//rotate to look at player
			//transform.LookAt (target.transform.position);
			//transform.Rotate (new Vector2 (0, -90), Space.Self);

			//move towards player
			if (Vector2.Distance (transform.position, target.transform.position) > attack1Range) 
			{
				transform.Translate (new Vector2 (speed * Time.deltaTime, 0));
			}
		}

		public void Rest ()
		{

		}
	}