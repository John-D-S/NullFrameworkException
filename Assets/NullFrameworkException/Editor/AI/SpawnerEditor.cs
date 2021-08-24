using NullFrameworkException.AI;

using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Collections;

using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.IMGUI.Controls;
using UnityEditor.Animations;
using UnityEngine;

namespace NullFrameworkException.Editor.AI
{
	[CustomEditor(typeof(Spawner))]
	public class SpawnerEditor : UnityEditor.Editor
	{
		// The reference to the component we are drawing the editor for
		private Spawner spawner;
		
		// the references to the values of the variables in held in the script
		private SerializedProperty sizeProperty;
		private SerializedProperty centerProperty;

		private SerializedProperty floorYPositionProperty;
		private SerializedProperty spawnRateProperty;

		private SerializedProperty shouldSpawnBossProperty;
		private SerializedProperty bossSpawnChanceProperty;
		private SerializedProperty bossPrefabProperty;
		private SerializedProperty enemyPrefabProperty;
		
		// the custom animation and scene elements
		private AnimBool shouldSpawnBoss = new AnimBool();// this allows the animation of showing the boss variables when the toggle is on.
		private BoxBoundsHandle handle; // This is the thing that will allow us to edit the bounds in the SceneView
		
		// OnEnable is the start of custom inspectors
		private void OnEnable()
		{
			// Convert the object that is being targeted to a spawner type as we know it is.
			spawner = target as Spawner;
			
			// Retrieve the serializedProperties from the object
			sizeProperty = serializedObject.FindProperty("size");
			centerProperty = serializedObject.FindProperty("center");

			floorYPositionProperty = serializedObject.FindProperty("floorYPosition");
			spawnRateProperty = serializedObject.FindProperty("spawnRate");

			shouldSpawnBossProperty = serializedObject.FindProperty("shouldSpawnBoss");
			bossSpawnChanceProperty = serializedObject.FindProperty("bossSpawnChance");

			bossPrefabProperty = serializedObject.FindProperty("bossPrefab");
			enemyPrefabProperty = serializedObject.FindProperty("enemyPrefab");
			
			// Set the Animation bool for the bossSpawning and create the handle
			shouldSpawnBoss.value = shouldSpawnBossProperty.boolValue;
			shouldSpawnBoss.valueChanged.AddListener(Repaint);
			handle = new BoxBoundsHandle();
		}
		
		// This allows us to modify and draw things in the sceneview
		private void OnSceneGUI()
		{
			//set the handles colour to green and store the original matrix value
			Handles.color = Color.green;
			Matrix4x4 original = Handles.matrix;
			
			// Change the handles matrix to be using the transform of this object
			Handles.matrix = spawner.transform.localToWorldMatrix;
			
			// Setup the box bounds handle with the spawners values
			handle.center = spawner.center;
			handle.size = spawner.size;
			
			//begin listening for changes to the handle and draw it
			EditorGUI.BeginChangeCheck();
			handle.DrawHandle();
			
			// check if any changes were made
			if(EditorGUI.EndChangeCheck())
			{
				// Register this change for the Undo-redo system
				Undo.RecordObject(spawner, "UPDATE_SPAWNER_BOUNDS");
				
				// reset the spawnedr values to the new handle values
				spawner.size = handle.size;
				spawner.center = handle.center;
			}
			
			//reset the handles matrix back to the original one
			Handles.matrix = original;
		}
		
		// this is where we draw the custom inspector window and render the scripts properties
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			// Create a vertical layout group visualized as a box
			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				// Draw the center and size properties exactly as unity would
				EditorGUILayout.PropertyField(centerProperty);
				EditorGUILayout.PropertyField(sizeProperty);
			}
			EditorGUILayout.EndVertical();

			EditorGUILayout.BeginVertical(GUI.skin.box);
			{
				EditorGUILayout.PropertyField(floorYPositionProperty);
				
				// Cache the original value of the spawn rate and create a label
				Vector2 spawnRate = spawnRateProperty.vector2Value;
				string label = $"Range ({spawnRate.x:0.0}s - {spawnRate.y:0.0}s)";

				EditorGUILayout.MinMaxSlider(label, ref spawnRate.x, ref spawnRate.y, 0, 3);
				spawnRateProperty.vector2Value = spawnRate;
				
				// Apply some spacing between lines
				EditorGUILayout.Space();
				
				// Render the enemyPrefab and shouldSpawnBoss as normal
				EditorGUILayout.PropertyField(enemyPrefabProperty);
				EditorGUILayout.PropertyField(shouldSpawnBossProperty);
				
				// Attempt to fade the next variables in and out
				shouldSpawnBoss.target = shouldSpawnBossProperty.boolValue;
				if(EditorGUILayout.BeginFadeGroup(shouldSpawnBoss.faded))
				{
					// Only visible when 'ShouldSpawnBoss' in spawner script is true
					
					// Indent the editor
					EditorGUI.indentLevel++;
					{
						// Draw bosSpawnChance and bosPrefab as normal
						EditorGUILayout.PropertyField(bossSpawnChanceProperty);
						EditorGUILayout.PropertyField(bossPrefabProperty);
					}
					EditorGUI.indentLevel--;
					
				}
				EditorGUILayout.EndFadeGroup();
			}
			EditorGUILayout.EndVertical();
			
			// Apply the changes we made in the inspector
			serializedObject.ApplyModifiedProperties();
		}
	}
}
