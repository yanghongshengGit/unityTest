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


public class GUIEvents : MonoBehaviour {

    void Update()
    {
        if (name == "FaceBook" || name == "Share")
        {
            if (LevelManager.THIS.FacebookDisable)
                gameObject.SetActive(false);
        }
    }

    public void Settings()
    {
        SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);

        GameObject.Find("CanvasGlobal").transform.Find("Settings").gameObject.SetActive(true);

    }
    public void Play()
    {
        SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);

        transform.Find("Loading").gameObject.SetActive(true);
        Application.LoadLevel("game");
    }

    public void Pause()
    {
        SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.click);

        if(LevelManager.THIS.gameStatus == GameState.Playing)
            GameObject.Find("CanvasGlobal").transform.Find("MenuPause").gameObject.SetActive(true);

    }

    public void FaceBookLogin()
    {
        InitScript.Instance.CallFBLogin();
    }
    public void Share()
    {
        InitScript.Instance.Share();
    }

}
