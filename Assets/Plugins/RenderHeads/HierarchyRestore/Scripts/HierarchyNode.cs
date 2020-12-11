#if UNITY_5_3 || UNITY_5_4_OR_NEWER
	#define RH_UNITY_FEATURE_SCENEMANAGMENT
#endif
#if UNITY_5_4_OR_NEWER || (UNITY_5 && !UNITY_5_0)
	#define RH_UNITY_FEATURE_DEBUG_ASSERT
#endif

using UnityEngine;
// Note: This script is only for the editor, but we don't put it into an Editor folder because it needs to be attached to a Monobehaviour
#if UNITY_EDITOR
using UnityEditor;
#if RH_UNITY_FEATURE_SCENEMANAGMENT
using UnityEditor.SceneManagement;
#endif

//-----------------------------------------------------------------------------
// Copyright 2017-2019 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

namespace RenderHeads.Framework.Editor
{
	/// <summary>
	/// This component is mainly used to detect when a scene is unloaded as it uses the OnDestroy() hook to save the current hierarchy state
	/// </summary>
	[ExecuteInEditMode]
	public class HierarchyNode : MonoBehaviour
	{
		private string _sceneGUID;
		private string _selectionPathList;
		private string _hierarchyPathList;
		private int editorUpdateCount;
		private bool _isHierarchyDirty;
		private static Transform[] _lastSelection;

        public object HierarchyUtils { get; private set; }

        private void Start()
		{
			RestoreSelection();
		}

		private void OnEnable()
		{
			EditorApplication.update += OnEditorUpdate;
		}
		
		private void OnDisable()
		{
			EditorApplication.update -= OnEditorUpdate;
		}

		// When the scene closes, this node will get destroyed, at which point we save the scene state information
		private void OnDestroy()
		{
		}

		private void RestoreSelection()
		{
		}

		private void OnEditorUpdate()
		{
		}

		public void SetHierarchyDirty()
		{
		}

		private bool HasHierarchyChanged()
		{
			return false;
		}

		private static bool HasSelectionChanged()
		{
			return false;
		}

		private void CacheHierarchy()
		{
		}

		private void UpdateSceneGUID()
		{
		}

		public static string GetSceneGUID(string path)
		{
			return null;
		}

		private static string GetFullPath(string relativePath)
		{
			return null;
		}

		public static string RestoreSelectionState(string sceneId, bool logMissingNodes)
		{
			return null;
		}

		public static string RestoreHierarchyState(string sceneId, bool logMissingNodes)
		{
			return null;
		}

		public static void SaveSelectionState(string state, string sceneId)
		{
		}

		public static void SaveHierarchyState(string state, string sceneId)
		{
		}

		private static void SaveState(string stateName, string state, string sceneId)
		{
		}

		private static string GetEditorPref(string name, string sceneId)
		{
			return null;
		}
	}
}
#endif