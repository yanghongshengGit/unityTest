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

public class MapLevelNumber : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        //renderer.sortingLayerID = 0;
        //renderer.sortingOrder = 2;
        int num = int.Parse( transform.parent.parent.name.Replace( "Level", "" ) );
        //GetComponent<TextMesh>().text = "" + num;
        GetComponent<Text>().text = "" + num;
      //  if( num >= 10 ) transform.position += Vector3.left * 0.05f;
   //     if( num == 1 || num == 11 ) transform.position -= Vector3.right * 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
