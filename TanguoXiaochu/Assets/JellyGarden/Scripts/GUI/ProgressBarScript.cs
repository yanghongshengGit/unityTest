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

public class ProgressBarScript : MonoBehaviour {
	Image slider;
	public static ProgressBarScript Instance;
	float maxWidth;
    public GameObject[] stars;
	// Use this for initialization
	void OnEnable () {
		Instance = this;
        slider = GetComponent<Image>();
		maxWidth = 1;
		ResetBar();
        PrepareStars();
	}
	
	public void UpdateDisplay (float x) {
        slider.fillAmount = maxWidth * x;
		if(maxWidth * x >= maxWidth){
            slider.fillAmount = maxWidth;

		//	ResetBar();
		}
	}
	
	public void AddValue (float x) {
        UpdateDisplay(slider.fillAmount * 100 / maxWidth / 100 + x);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public bool IsFull()
	{
        if (slider.fillAmount >= maxWidth)
        { 
			ResetBar();
			return true;
		}
		else return false;
	}
	
	public void ResetBar(){
		UpdateDisplay(0.0f);
	}

    void PrepareStars()
    {
        float width = GetComponent<RectTransform>().rect.width;
        stars[0].transform.localPosition = new Vector3(LevelManager.Instance.star1 * 100 / LevelManager.Instance.star3 * width / 100 - (width / 2f), stars[0].transform.localPosition.y, 0);
        stars[1].transform.localPosition = new Vector3(LevelManager.Instance.star2 * 100 / LevelManager.Instance.star3 * width / 100 - (width / 2f), stars[1].transform.localPosition.y, 0);
        stars[0].transform.GetChild(0).gameObject.SetActive(false);
        stars[1].transform.GetChild(0).gameObject.SetActive(false);
        stars[2].transform.GetChild(0).gameObject.SetActive(false);
    }

}
