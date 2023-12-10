﻿using UnityEngine;

namespace RavenSoul.Utilities.Singleton
{
	public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
	{
		public static T Instance { get; protected set; }

		public static bool InstanceExists => Instance != null;

		protected virtual void Awake()
		{
			if (InstanceExists)
			{
				Destroy(gameObject);
			}
			else
			{
				Instance = (T) this;
			}
		}

		protected virtual void OnDestroy()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}
}