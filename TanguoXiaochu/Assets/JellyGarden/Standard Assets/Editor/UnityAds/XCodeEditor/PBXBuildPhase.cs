/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Advertisements.XCodeEditor
{
	public class PBXBuildPhase : PBXObject
	{
		protected const string FILES_KEY = "files";

		public PBXBuildPhase() :base()
		{
			internalNewlines = true;
		}

		public PBXBuildPhase(string guid, PBXDictionary dictionary) : base (guid, dictionary)
		{
			internalNewlines = true;
		}

		public bool AddBuildFile(PBXBuildFile file)
		{
			if(!ContainsKey(FILES_KEY)) {
				this.Add(FILES_KEY, new PBXList());
			}
			if(!HasBuildFile (file.guid)) {
				((PBXList)_data[FILES_KEY]).Add(file.guid);
				return true;
			}
			return false;
		}

		public void RemoveBuildFile(string id)
		{
			if(!ContainsKey(FILES_KEY)) {
				this.Add(FILES_KEY, new PBXList());
				return;
			}

			((PBXList)_data[FILES_KEY]).Remove(id);
		}

		public bool HasBuildFile(string id)
		{
			if(!ContainsKey(FILES_KEY)) {
				this.Add(FILES_KEY, new PBXList());
				return false;
			}

			if(!IsGuid(id))
				return false;

			return ((PBXList)_data[FILES_KEY]).Contains(id);
		}
	}

	public class PBXFrameworksBuildPhase : PBXBuildPhase
	{
		public PBXFrameworksBuildPhase(string guid, PBXDictionary dictionary) : base ( guid, dictionary )
		{
		}
	}

	public class PBXResourcesBuildPhase : PBXBuildPhase
	{
		public PBXResourcesBuildPhase(string guid, PBXDictionary dictionary) : base ( guid, dictionary )
		{
		}
	}

	public class PBXShellScriptBuildPhase : PBXBuildPhase
	{
		public PBXShellScriptBuildPhase(string guid, PBXDictionary dictionary) : base ( guid, dictionary )
		{
		}
	}

	public class PBXSourcesBuildPhase : PBXBuildPhase
	{
		public PBXSourcesBuildPhase(string guid, PBXDictionary dictionary) : base ( guid, dictionary )
		{
		}
	}

	public class PBXCopyFilesBuildPhase : PBXBuildPhase
	{
		public PBXCopyFilesBuildPhase(string guid, PBXDictionary dictionary) : base ( guid, dictionary )
		{
		}
	}
}
