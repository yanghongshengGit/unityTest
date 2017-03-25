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

public class Tutorial : MonoBehaviour {
    public Sprite[] sprites;
    public Image image;
    int i;
    public void Next()
    {
        i++;
        i = Mathf.Clamp(i, 0, sprites.Length-1);
        SetImage();
    }
    public void Back()
    {
        i--;
        i = Mathf.Clamp(i, 0, sprites.Length-1);
        SetImage();
    }

    void SetImage()
    {
        image.sprite = sprites[i];
        image.SetNativeSize();
    }
}
