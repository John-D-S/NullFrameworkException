using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException
{

	public class MonoSingleton<TSingletonType> : MonoBehaviour where TSingletonType : MonoSingleton<TSingletonType>
	{
		public static TSingletonType Instance
		{
			get
			{
				// the internal instance isn't set, so attempt to find it in the scene
				if(instance == null)
				{
					instance = FindObjectOfType<TSingletonType>();

					// No instance was found, so throw a nullreferenceeception detailing what singleton caused the error
					if(instance == null)
					{
						// the "typof(TsingletonType).Name" shows the exact class name of the generic type
						// This line will also give us a stacktrace, showing where the call to the Instance was before it existed
						throw new NullReferenceException($"No objects of type: {typeof(TSingletonType).Name} was found.");
					}
				}

				return instance;
			}
		}

		private static TSingletonType instance = null;

		/// <summary>
		/// Has the singleton been generated?
		/// </summary>
		public static bool IsSingletonValid() => instance != null;

		/// <summary>
		/// Force the singleton instance to not be destroyed 
		/// </summary>
		public static void FlagAsPersistant() => DontDestroyOnLoad(Instance.gameObject);

		/// <summary>
		/// Finds the instance within the scene
		/// </summary>
		protected TSingletonType CreateInstance() => Instance;
	}
}

