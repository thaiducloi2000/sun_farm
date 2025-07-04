using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
namespace nano.vs2{
	[CustomEditor(typeof(VisualState))]
	public class VisualStateEditor : Editor {
		VisualState vs;
        private SerializedProperty onBeforeChangeProp;
        private SerializedProperty onUpadteChangeProp;
        private SerializedProperty onAfterChangeProp;
        private bool showEvent = false;
		void OnEnable()
		{
			vs = target as VisualState;
            onBeforeChangeProp = serializedObject.FindProperty("OnBeforeChange");
            onUpadteChangeProp = serializedObject.FindProperty("OnUpdateChange");
            onAfterChangeProp = serializedObject.FindProperty("OnAfterChange");
		}
		public override void OnInspectorGUI()
		{
            //DrawDefaultInspector();

            var slide = EditorGUILayout.IntSlider("state", vs.state, 0, vs.nStates - 1);
            if (slide != vs.state)
            {
                Undo.RecordObject(target, "Change state");
                vs.SetState(slide);
            }
            vs.useAnim = EditorGUILayout.Toggle(VSStyle.AnimateContent, vs.useAnim);
            if(vs.useAnim)
            {
                var cacheIndent = EditorGUI.indentLevel;
                EditorGUI.indentLevel++;
                vs.animTime = EditorGUILayout.FloatField("Anim Time", vs.animTime);
                vs.loopType = (LoopType)EditorGUILayout.EnumPopup("Loop", vs.loopType);
                var type = (EaseType)EditorGUILayout.EnumPopup("Easting", vs.easeType);
                if(vs.easeType != type)
                {
                    vs.easeType = type;
                    vs.ClearEasing();
                }
                EditorGUI.indentLevel = cacheIndent;
            }

			vs.setDefaultStateOnStart = EditorGUILayout.Toggle("Play On Awake", vs.setDefaultStateOnStart);
			if(vs.setDefaultStateOnStart)
			{
				EditorGUI.indentLevel++;
				vs.defaultState = EditorGUILayout.IntSlider("state", vs.defaultState, 0, vs.nStates - 1);
			}
			
            showEvent = EditorGUILayout.Foldout(showEvent, "Events");
            if(showEvent)
            {
                if (onBeforeChangeProp != null) EditorGUILayout.PropertyField(onBeforeChangeProp);
                if (onUpadteChangeProp != null) EditorGUILayout.PropertyField(onUpadteChangeProp);
                if (onAfterChangeProp != null) EditorGUILayout.PropertyField(onAfterChangeProp);
                serializedObject.ApplyModifiedProperties();
            }

            if (GUILayout.Button("Edit State"))
			{
                VisualStateEditorWindow.ShowWindow();
			}
			
			

		}
	}

}
#endif
