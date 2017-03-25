/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections;
using UnityEngine;

public class MapTargetIcon : MonoBehaviour
{
    private int num;
    public Sprite[] targetSprite;
    private Target tar;
    private LIMIT limitType;
    void OnEnable()
    {
        StartCoroutine(loadTarget());
    }

    IEnumerator loadTarget()
    {
        num = int.Parse(transform.parent.name.Replace("Level", ""));
        LoadLevel(num);
        yield return new WaitForSeconds(0.1f);
        if (limitType == LIMIT.TIME)
            GetComponent<SpriteRenderer>().sprite = targetSprite[4];
        else
            GetComponent<SpriteRenderer>().sprite = targetSprite[(int)tar];

    }

    void LoadLevel(int n)
    {
        TextAsset map = Resources.Load("Levels/" + n) as TextAsset;
        if (map != null)
        {
            string mapText = map.text;
            string[] lines = mapText.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            int mapLine = 0;
            foreach (string line in lines)
            {
                //check if line is game mode line
                if (line.StartsWith("MODE"))
                {
                    //Replace GM to get mode number, 
                    string modeString = line.Replace("MODE", string.Empty).Trim();
                    //then parse it to interger
                    tar = (Target)int.Parse(modeString);
                    //Assign game mode
                }
                else if (line.StartsWith("LIMIT"))
                {
                    string blocksString = line.Replace("LIMIT", string.Empty).Trim();
                    string[] sizes = blocksString.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                    limitType = (LIMIT)int.Parse(sizes[0]);
                }

            }
        }

    }

    void Update()
    {

    }
}
