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
using System.Collections.Generic;

public class BoostAnimation : MonoBehaviour
{
    public Square square;

    public void ShowEffect()
    {
        GameObject partcl = Instantiate(Resources.Load("Prefabs/Effects/Firework"), transform.position, Quaternion.identity) as GameObject;
        partcl.GetComponent<ParticleSystem>().startColor = LevelManager.THIS.scoresColors[square.item.color];
        Destroy(partcl, 1f);

    }
    public void OnFinished(BoostType boostType)
    {
        if (boostType == BoostType.Random_color)
        {

            List<Item> itemsList = LevelManager.THIS.GetItemsAround(square);
            foreach (Item item in itemsList)
            {
                if (item != null)
                {
                    if (item.currentType == ItemsTypes.NONE)
                        item.GenColor(-1, true);
                }
            }

        }
        if (boostType == BoostType.Bomb)
        {
            square.item.DestroyItem();
        }
        LevelManager.THIS.StartCoroutine(LevelManager.THIS.FindMatchDelay());
        SoundBase.Instance.GetComponent<AudioSource>().PlayOneShot(SoundBase.Instance.explosion);

        Destroy(gameObject);
    }
}
