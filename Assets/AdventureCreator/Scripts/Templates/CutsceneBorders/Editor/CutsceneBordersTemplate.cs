#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AC.Templates.CutsceneBorders
{

	public class CutsceneBordersTemplate : Template
	{

		#region Variables

		[SerializeField] private MenuManager defaultMenuManager = null;

		#endregion


		#region PublicFunctions

		public override bool CanInstall (ref string errorText)
		{
			if (Resource.References.menuManager == null || defaultMenuManager == null)
			{
				errorText = "No Menu Manager assigned";
				return false;
			}

			if (Resource.References.menuManager == defaultMenuManager)
			{
				errorText = "Wrong Menu Manager assigned";
				return false;
			}

			return true;
		}


		public override bool CanSuggest (NGWData data)
		{
			return true;
		}

		#endregion


		#region ProtectedFunctions

		protected override void MakeChanges (string installPath, bool canDeleteOldAssets, System.Action onComplete, System.Action<string> onFail)
		{
			Undo.RecordObjects (new UnityEngine.Object[] { Resource.References.menuManager }, "");

			CopyMenus (installPath, defaultMenuManager, Resource.References.menuManager, canDeleteOldAssets ? ExistingMenuBehaviour.Delete : ExistingMenuBehaviour.Rename);

			EditorUtility.SetDirty (Resource.References.menuManager);

			onComplete.Invoke ();
		}

		#endregion


		#region GetSet

		public override string Label { get { return "Cutscene borders"; }}
		public override string PreviewText { get { return "Adds animated borders to the top and bottom of the screen during cutscenes"; }}
		public override Type[] AffectedManagerTypes { get { return new Type[] { typeof (MenuManager) }; }}
		public override bool RequiresInstallPath { get { return true; }}
		public override string FolderName { get { return "CutsceneBorders"; }}
		public override TemplateCategory Category { get { return TemplateCategory.Misc; }}
		public override bool IsExclusiveToCategory { get { return false; }}

		#endregion

	}


	[CustomEditor (typeof (CutsceneBordersTemplate))]
	public class CutsceneBordersTemplateEditor : TemplateEditor
	{}

}

#endif