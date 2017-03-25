/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;

namespace Assets.Plugins.SmartLevelsMap.Scripts
{
    public class ApiTest : MonoBehaviour, IMapProgressManager
    {
        private int _levelNumber = 1;
        private int _starsCount = 1;
        private bool _isShow;

        public DemoButton YesButton;
        public DemoButton NoButton;
        public GameObject ConfirmationView;
        public int SelectedLevelNumber;

        public void Awake()
        {
            //Uncomment to set this script as IMapProgressManager
            //LevelsMap.OverrideMapProgressManager(this);
        }

        #region Events

        public void OnEnable()
        {
            Debug.Log("Subscribe to events.");
            LevelsMap.LevelSelected += OnLevelSelected;
            LevelsMap.LevelReached += OnLevelReached;
            //            YesButton.Click += OnYesButtonClick;
            //            NoButton.Click += OnNoButtonClick;
        }

        public void OnDisable()
        {
            Debug.Log("Unsubscribe from events.");
            LevelsMap.LevelSelected -= OnLevelSelected;
            LevelsMap.LevelReached -= OnLevelReached;
            //           YesButton.Click -= OnYesButtonClick;
            //           NoButton.Click -= OnNoButtonClick;
        }

        private void OnLevelReached(object sender, LevelReachedEventArgs e)
        {
            Debug.Log(string.Format("Level {0} reached.", e.Number));
        }

        #endregion

        #region Api test
        public void OnGUI()
        {
            GUILayout.BeginVertical();

            DrawToggleShowButton();

            if (_isShow)
            {
                DrawInputParameters();
                if (GUILayout.Button("Complete all  levels"))
                {
                    for (int i = 1; i < GameObject.Find("Levels").transform.childCount; i++)
                    {
                       // LevelsMap.CompleteLevel(i, _starsCount);
						SaveLevelStarsCount(i, _starsCount);
                    }
                }

                if (GUILayout.Button("Complete level"))
                {
                    if (LevelsMap.IsStarsEnabled())
                        LevelsMap.CompleteLevel(_levelNumber, _starsCount);
                    else
                        LevelsMap.CompleteLevel(_levelNumber);
                }

                if (GUILayout.Button("Go to level"))
                {
                    LevelsMap.GoToLevel(_levelNumber);
                }

                if (GUILayout.Button("Is level locked"))
                {
                    bool isLocked = LevelsMap.IsLevelLocked(_levelNumber);
                    Debug.Log(string.Format("Level {0} is {1}",
                        _levelNumber,
                        isLocked ? "locked" : "not locked"));
                }

                if (GUILayout.Button("Clear all progress"))
                {
                    LevelsMap.ClearAllProgress();
                }
            }

            GUILayout.EndVertical();
        }

        private void DrawToggleShowButton()
        {
            if (!_isShow)
            {
                if (GUILayout.Button("Show API tests"))
                {
                    _isShow = true;
                }
            }
            if (_isShow)
            {
                if (GUILayout.Button("Hide API tests"))
                {
                    _isShow = false;
                }
            }
        }

        private void DrawInputParameters()
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label("Level number:");
            string strLevelNumber = GUILayout.TextField(_levelNumber.ToString(), 10, GUILayout.Width(80));
            int.TryParse(strLevelNumber, out _levelNumber);

            if (LevelsMap.IsStarsEnabled())
            {
                GUILayout.Label("Stars count:");
                string strStarsCount = GUILayout.TextField(_starsCount.ToString(), 10, GUILayout.Width(80));
                int.TryParse(strStarsCount, out _starsCount);
            }

            GUILayout.EndHorizontal();
        }

        #endregion

        #region IMapProgressManager
		        private string GetLevelKey(int number)
        {
            return string.Format("Level.{0:000}.StarsCount", number);
        }

        public int LoadLevelStarsCount(int level)
        {
            return level > 10 ? 0 : (level % 3 + 1);
        }

        public void SaveLevelStarsCount(int level, int starsCount)
        {
            Debug.Log(string.Format("Stars count {0} of level {1} saved.", starsCount, level));
			            PlayerPrefs.SetInt(GetLevelKey(level), starsCount);

        }

        public void ClearLevelProgress(int level)
        {

        }

        #endregion

        #region Confirmation demo

        private void OnLevelSelected(object sender, LevelReachedEventArgs e)
        {
            if (LevelsMap.GetIsConfirmationEnabled())
            {
                SelectedLevelNumber = e.Number;
                // ConfirmationView.SetActive(true);
            }
        }

        private void OnNoButtonClick(object sender, EventArgs e)
        {
            ConfirmationView.SetActive(false);
        }

        private void OnYesButtonClick(object sender, EventArgs e)
        {
            ConfirmationView.SetActive(false);
            LevelsMap.GoToLevel(SelectedLevelNumber);
        }

        #endregion

    }
}
