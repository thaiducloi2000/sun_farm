using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace nano.vs2
{
	
	public class VisualStateEditorWindow : EditorWindow {
		[MenuItem("Window/Visual State")]
		internal static void ShowWindow(){
			GetWindow<VisualStateEditorWindow>("Visual State").Show();
		}
		void OnEnable()
		{
			OnSelectionChange();
			
			titleContent = new GUIContent("Visual State");
		}
        private VisualState currentTarget;
        private VSEditor m_VSEditor;

        public void OnSelectionChange()
		{
            if (m_VSEditor != null && m_VSEditor.lockTarget && currentTarget != null) return;
			if(Selection.activeGameObject == null)
			{
				// m_VSEditor = null;
				return;
			}
			var vs = Selection.activeGameObject.GetComponent<VisualState>();
			if(vs == null)
			{
				// m_VSEditor = null;
				return;
			}

            if (VisualState.recordingTarget != null) return;

            //if (currentTarget != null && currentTarget == vs) return;
            currentTarget = vs;
			m_VSEditor = new VSEditor(vs, this);
		}
		[ContextMenu("Fit to view")]
        public void FitToView()
        {
            if (m_VSEditor == null) return;
            m_VSEditor.FitToView();
        }
        public void OnGUI()
        {
            if (m_VSEditor == null)
                return;

            // Profiler.BeginSample("AnimationWindow.OnGUI");
            // titleContent = m_AnimEditor.state.recording ? m_RecordTitleContent : m_DefaultTitleContent;

            m_VSEditor.OnAnimEditorGUI(position);
            // Profiler.EndSample();
        }

		// public void Update()
        // {
        //     if (m_AnimEditor == null)
        //         return;

        //     m_AnimEditor.Update();
        // }
    }

}