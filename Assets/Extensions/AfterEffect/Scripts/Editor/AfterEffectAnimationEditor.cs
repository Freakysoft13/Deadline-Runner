////////////////////////////////////////////////////////////////////////////////
//  
// @module Affter Effect Importer
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(AfterEffectAnimation))]
public class AfterEffectAnimationEditor : Editor {

	private float _timeScale;
	private AESettingsMode _mode;

	private SerializedProperty _GizmosColor;
	private SerializedProperty _MaterialColor;
	
	private SerializedProperty  _dataFile;
	private SerializedProperty  _imagesFolder;


	private SerializedProperty _PlayOnStart;
	private SerializedProperty _Loop;

	private SerializedProperty _currentFrame;

	private SerializedProperty _opacity;
	private SerializedProperty _IsForceSelected;

	private SerializedProperty _pivotCenterX;
	private SerializedProperty _pivotCenterY;



	//--------------------------------------
	// PUBLIC METHODS
	//--------------------------------------

	private void OnEnable () {
		_GizmosColor = serializedObject.FindProperty ("GizmosColor");


		_imagesFolder = serializedObject.FindProperty ("imagesFolder");
		_dataFile = serializedObject.FindProperty ("dataFile");

		_PlayOnStart = serializedObject.FindProperty ("PlayOnStart");
		_Loop = serializedObject.FindProperty ("Loop");

		_currentFrame = serializedObject.FindProperty ("currentFrame");

		_opacity = serializedObject.FindProperty ("opacity");

		_IsForceSelected = serializedObject.FindProperty ("IsForceSelected");

		_pivotCenterX = serializedObject.FindProperty ("pivotCenterX");
		_pivotCenterY = serializedObject.FindProperty ("pivotCenterY");
		
		_MaterialColor = serializedObject.FindProperty ("MaterialColor");

	}


	public override void OnInspectorGUI() {

		serializedObject.Update ();

		EditorGUILayout.Separator();

		if(targets.Length > 1) {
			EditorGUILayout.HelpBox("Multiedition Mode", MessageType.Info);
		} else {
			if(anim.dataFile != null) {
				if(anim.animationData != null) {
					float duration = anim.animationData.duration / anim.timeScale;

					string info = "";
					info += "Total Frames: " + anim.animationData.totalFrames + ", ";
					info += "Duration: " + duration + " sec ";
					EditorGUILayout.HelpBox(info, MessageType.Info);

				} else {
					EditorGUILayout.HelpBox("Calculating.....", MessageType.Info);
				}


			} else {
				EditorGUILayout.HelpBox("No Animation Data", MessageType.Warning);
			}
		}




		EditorGUILayout.Separator();


		EditorGUI.BeginChangeCheck();


		EditorGUILayout.PropertyField (_dataFile);
		EditorGUILayout.PropertyField (_imagesFolder);

		if (EditorGUI.EndChangeCheck ()) {
			ReloadAnimation ();
		}

		EditorGUILayout.PropertyField (_GizmosColor);
		
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField (_MaterialColor);
		if (EditorGUI.EndChangeCheck ()) {
			foreach(Object t in targets) {
			(t as AfterEffectAnimation).UpdateColor();
		}
		}


		if(anim.totalFrames != 0) {
			EditorGUI.BeginChangeCheck();

			EditorGUILayout.IntSlider(_currentFrame, 0, anim.totalFrames - 1);
			if (EditorGUI.EndChangeCheck ()) {
				OnEditorFrameChange();
			}
		} else {
			if(anim.dataFile != null) {
				//	ReloadAnimation ();
			}
		}

		_timeScale = EditorGUILayout.Slider ("Time Scale", anim.timeScale, 0.1f, 2f);
		foreach(Object t in targets) {
			(t as AfterEffectAnimation).timeScale = _timeScale;
		}




		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField (_opacity);
		if (EditorGUI.EndChangeCheck ()) {
			OnEditorFrameChange();
		}


		EditorGUILayout.PropertyField (_Loop);
		EditorGUILayout.PropertyField (_PlayOnStart);
	
		EditorGUILayout.Separator();
		ExtendedOptions ();
		EditorGUILayout.Separator();



		_mode = (AESettingsMode) EditorGUILayout.EnumPopup ("Mode", anim.mode);
		foreach(Object t in targets) {
			(t as AfterEffectAnimation).mode = _mode;
		}

		if(anim.mode == AESettingsMode.Advanced) {

			EditorGUILayout.PropertyField (_IsForceSelected, new GUIContent("Force Selection"));



			EditorGUI.BeginChangeCheck();

			EditorGUILayout.Slider (_pivotCenterX, 0f, 1f);
			EditorGUILayout.Slider (_pivotCenterY, 0f, 1f);


			if (EditorGUI.EndChangeCheck ()) {
				OnPivotPositionChnage ();
			}


			EditorGUI.BeginChangeCheck();
			anim.normal_mode_shader = EditorGUILayout.Popup ("Normal Mode Shader", anim.normal_mode_shader, AEShaders.importedShaders);
			anim.add_mode_shader = EditorGUILayout.Popup ("Add Mode Shader", anim.add_mode_shader, AEShaders.importedShaders);
			if (EditorGUI.EndChangeCheck ()) {
				ReloadAnimation ();
			}
		}

		EditorGUILayout.Separator();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if(GUILayout.Button(new GUIContent("Update"),   GUILayout.Width(100))) {
			ReloadAnimation ();
	    }
		EditorGUILayout.EndHorizontal();

		serializedObject.ApplyModifiedProperties ();

	}


	//--------------------------------------
	// GET / SET
	//--------------------------------------

	public AfterEffectAnimation anim {
		get {
			return target as AfterEffectAnimation;
		}
	}

	//--------------------------------------
	// EVENTS
	//--------------------------------------


	//--------------------------------------
	// PRIVATE METHODS
	//--------------------------------------

	private void OnPivotPositionChnage() {
		serializedObject.ApplyModifiedProperties ();
		foreach(Object t in targets) {
			(t as AfterEffectAnimation).OnPivotPositionChnage ();
		}
	}

	private void OnEditorFrameChange() {
		serializedObject.ApplyModifiedProperties ();
		foreach(Object t in targets) {
			(t as AfterEffectAnimation).OnEditorFrameChange ();
		}
	}

	protected virtual void ReloadAnimation() {
		serializedObject.ApplyModifiedProperties ();
		foreach(Object t in targets) {
			(t as AfterEffectAnimation).OnAnimationDataChange ();
		}
	}

	protected virtual void ExtendedOptions() {

	}




}
