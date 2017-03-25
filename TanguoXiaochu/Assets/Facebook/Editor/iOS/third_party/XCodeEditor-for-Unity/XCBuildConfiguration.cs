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

namespace UnityEditor.XCodeEditor
{
	public class XCBuildConfiguration : PBXObject
	{
		protected const string BUILDSETTINGS_KEY = "buildSettings";
		protected const string HEADER_SEARCH_PATHS_KEY = "HEADER_SEARCH_PATHS";
		protected const string LIBRARY_SEARCH_PATHS_KEY = "LIBRARY_SEARCH_PATHS";
		protected const string FRAMEWORK_SEARCH_PATHS_KEY = "FRAMEWORK_SEARCH_PATHS";
		protected const string OTHER_C_FLAGS_KEY = "OTHER_CFLAGS";
		
		public XCBuildConfiguration( string guid, PBXDictionary dictionary ) : base( guid, dictionary )
		{
			internalNewlines = true;
		}
		
		public PBXDictionary buildSettings {
			get {
				if( ContainsKey( BUILDSETTINGS_KEY ) )
					return (PBXDictionary)_data[BUILDSETTINGS_KEY];
			
				return null;
			}
		}
		
		protected bool AddSearchPaths( string path, string key, bool recursive = true )
		{
			PBXList paths = new PBXList();
			paths.Add( path );
			return AddSearchPaths( paths, key, recursive );
		}
		
		protected bool AddSearchPaths( PBXList paths, string key, bool recursive = true )
		{	
			bool modified = false;
			
			if( !ContainsKey( BUILDSETTINGS_KEY ) )
				this.Add( BUILDSETTINGS_KEY, new PBXDictionary() );
			
			foreach( string path in paths ) {
				string currentPath = path;
				if( recursive && !path.EndsWith( "/**" ) )
					currentPath += "**";
				if( !((PBXDictionary)_data[BUILDSETTINGS_KEY]).ContainsKey( key ) ) {
					((PBXDictionary)_data[BUILDSETTINGS_KEY]).Add( key, new PBXList() );
				}
				else if( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[key] is string ) {
					PBXList list = new PBXList();
					list.Add( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[key] );
					((PBXDictionary)_data[BUILDSETTINGS_KEY])[key] = list;
				}
				
				
				if( !((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[key]).Contains( currentPath ) ) {
					((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[key]).Add( currentPath );
					modified = true;
				}
			}
		
			return modified;
		}
		
		public bool AddHeaderSearchPaths( PBXList paths, bool recursive = true )
		{
			return this.AddSearchPaths( paths, HEADER_SEARCH_PATHS_KEY, recursive );
		}
		
		public bool AddLibrarySearchPaths( PBXList paths, bool recursive = true )
		{
			return this.AddSearchPaths( paths, LIBRARY_SEARCH_PATHS_KEY, recursive );
		}
		
		public bool AddFrameworkSearchPaths( PBXList paths, bool recursive = true )
		{
			return this.AddSearchPaths( paths, FRAMEWORK_SEARCH_PATHS_KEY, recursive );
		}
		
		public bool AddOtherCFlags( string flag )
		{
			Debug.Log( "INIZIO 1" );
			PBXList flags = new PBXList();
			flags.Add( flag );
			return AddOtherCFlags( flags );
		}
		
		public bool AddOtherCFlags( PBXList flags )
		{
			Debug.Log( "INIZIO 2" );
			
			bool modified = false;
			
			if( !ContainsKey( BUILDSETTINGS_KEY ) )
				this.Add( BUILDSETTINGS_KEY, new PBXDictionary() );
			
			foreach( string flag in flags ) {
				
				if( !((PBXDictionary)_data[BUILDSETTINGS_KEY]).ContainsKey( OTHER_C_FLAGS_KEY ) ) {
					((PBXDictionary)_data[BUILDSETTINGS_KEY]).Add( OTHER_C_FLAGS_KEY, new PBXList() );
				}
				else if ( ((PBXDictionary)_data[BUILDSETTINGS_KEY])[ OTHER_C_FLAGS_KEY ] is string ) {
					string tempString = (string)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY];
					((PBXDictionary)_data[BUILDSETTINGS_KEY])[ OTHER_C_FLAGS_KEY ] = new PBXList();
					((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY]).Add( tempString );
				}
				
				if( !((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY]).Contains( flag ) ) {
					((PBXList)((PBXDictionary)_data[BUILDSETTINGS_KEY])[OTHER_C_FLAGS_KEY]).Add( flag );
					modified = true;
				}
			}
			
			return modified;
		}
		
	}
}
