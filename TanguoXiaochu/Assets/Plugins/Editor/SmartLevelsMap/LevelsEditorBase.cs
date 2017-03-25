/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;
using System.Linq;
using Assets.Plugins.SmartLevelsMap.Scripts;
using UnityEditor;
using UnityEngine;

namespace Assets.Plugins.Editor.SmartLevelsMap
{
    public abstract class LevelsEditorBase : UnityEditor.Editor
    {
        protected List<MapLevel> GetMapLevels()
        {
            return FindObjectsOfType<MapLevel>().OrderBy(ml => ml.Number).ToList();
        }

        protected MapLevel CreateMapLevel(Vector3 position, int number, MapLevel mapLevelPrefab)
        {
            MapLevel mapLevel = PrefabUtility.InstantiatePrefab(mapLevelPrefab) as MapLevel;
            mapLevel.transform.position = position;
            return mapLevel;
        }

        protected void UpdateLevelsNumber(List<MapLevel> mapLevels)
        {
            for (int i = 0; i < mapLevels.Count; i++)
            {
                mapLevels[i].Number = i + 1;
                mapLevels[i].name = string.Format("Level{0:00}", i + 1);
            }
        }

        protected void UpdatePathWaypoints(List<MapLevel> mapLevels)
        {
            Path path = FindObjectOfType<Path>();
            path.Waypoints.Clear();
            foreach (MapLevel mapLevel in mapLevels)
                path.Waypoints.Add(mapLevel.PathPivot);
        }

        protected void SetAllMapLevelsAsDirty()
        {
            GetMapLevels().ForEach(EditorUtility.SetDirty);
        }

        protected void SetStarsEnabled(LevelsMap levelsMap, bool isEnabled)
        {
            levelsMap.SetStarsEnabled(isEnabled);
            if(isEnabled)
                levelsMap.SetStarsType(levelsMap.StarsType);
            EditorUtility.SetDirty(levelsMap);
            SetAllMapLevelsAsDirty();
        }
    }
}
