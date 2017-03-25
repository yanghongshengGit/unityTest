/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/


using UnityEngine;

namespace Assets.Plugins.SmartLevelsMap.Scripts
{
    public class PlayerPrefsMapProgressManager : IMapProgressManager
    {
        private string GetLevelKey(int number)
        {
            return string.Format("Level.{0:000}.StarsCount", number);
        }

        public int LoadLevelStarsCount(int level)
        {
            return PlayerPrefs.GetInt(GetLevelKey(level), 0);
        }

        public void SaveLevelStarsCount(int level, int starsCount)
        {
            PlayerPrefs.SetInt(GetLevelKey(level), starsCount);
        }

        public void ClearLevelProgress(int level)
        {
            PlayerPrefs.DeleteKey(GetLevelKey(level));
        }
    }
}
