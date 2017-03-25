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
using UnityEngine.UI;

public class Level : MonoBehaviour {
    public int number;
    public Text label;
    public GameObject lockimage;

	// Use this for initialization
	void Start () {
        if( PlayerPrefs.GetInt( "Score" + (number-1) ) > 0  )
        {
            lockimage.gameObject.SetActive( false );
            label.text = "" + number;
        }

        int stars = PlayerPrefs.GetInt( string.Format( "Level.{0:000}.StarsCount", number ), 0 );

        if( stars > 0 )
        {
            for( int i = 1; i <= stars; i++ )
            {
                transform.Find( "Star" + i ).gameObject.SetActive( true );
            }

        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartLevel()
    {
//        InitScript.Instance.OnLevelClicked(number);

    }
}
