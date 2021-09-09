using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Mathematics;

using UnityEngine;

namespace AsteroidHell
{
	/// <summary>
	/// Handles the level background repeating.
	/// </summary>
	public class LevelManager : MonoBehaviour
	{
		public float speed = 5;
		private Vector3 startPos;

		private void Start()
		{
			startPos = transform.position;
		}

		private void Update()
		{
			transform.Translate(Vector3.down*speed*Time.deltaTime);
			if(transform.position.y < - 36.44f)
			{
				transform.position = startPos;
			}
		}
	}
}