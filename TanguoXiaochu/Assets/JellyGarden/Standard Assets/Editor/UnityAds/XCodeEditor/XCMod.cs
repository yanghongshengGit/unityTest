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
using System.IO;

namespace UnityEngine.Advertisements.XCodeEditor
{
	public class XCMod
	{
		private Hashtable _datastore;
		private List<object> _libs;

		public string name { get; private set; }

		public string path { get; private set; }

		public string group {
			get {
				return (string)_datastore["group"];
			}
		}

		public List<object> patches {
			get {
				return (List<object>)_datastore["patches"];
			}
		}

		public List<object> libs {
			get {
				if(_libs == null) {
					List<object> libsCast = (List<object>)_datastore["libs"];
					int count = libsCast.Count;

					_libs = new List<object>(count);
					foreach(string fileRef in libsCast) {
						_libs.Add(new XCModFile(fileRef));
					}
				}
				return _libs;
			}
		}

		public List<object> librarysearchpaths {
			get {
				return (List<object>)_datastore["librarysearchpaths"];
			}
		}

		public List<object> frameworks {
			get {
				return (List<object>)_datastore["frameworks"];
			}
		}

		public List<object> frameworksearchpath {
			get {
				return (List<object>)_datastore["frameworksearchpaths"];
			}
		}

		public List<object> headerpaths {
			get {
				return (List<object>)_datastore["headerpaths"];
			}
		}

		public List<object> files {
			get {
				return (List<object>)_datastore["files"];
			}
		}

		public List<object> folders {
			get {
				return (List<object>)_datastore["folders"];
			}
		}

		public List<object> excludes {
			get {
				return (List<object>)_datastore["excludes"];
			}
		}

		public XCMod(string projectPath, string filename)
		{
			FileInfo projectFileInfo = new FileInfo(filename);
			if(!projectFileInfo.Exists) {
				Debug.LogWarning("File does not exist.");
			}

			name = System.IO.Path.GetFileNameWithoutExtension(filename);
			path = projectPath;

			string contents = projectFileInfo.OpenText().ReadToEnd();
			Dictionary<string, object> dictJson = MiniJSON.Json.Deserialize(contents) as Dictionary<string,object>;
			_datastore = new Hashtable(dictJson);
		}
	}

	public class XCModFile
	{
		public string filePath { get; private set; }

		public bool isWeak { get; private set; }

		public string sourceTree { get; private set; }

		public XCModFile(string inputString)
		{
			isWeak = false;
			sourceTree = "SDKROOT";
			if(inputString.Contains(":")) {
				string[] parts = inputString.Split(':');
				filePath = parts[0];
				isWeak = System.Array.IndexOf(parts, "weak", 1) > 0;

				if(System.Array.IndexOf(parts, "<group>", 1) > 0)
					sourceTree = "GROUP";
				else
					sourceTree = "SDKROOT";
			} else {
				filePath = inputString;
			}
		}
	}
}
