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
[RequireComponent( typeof(AudioSource) )]
public class SoundBase : MonoBehaviour
{
	public static SoundBase Instance;
	public AudioClip click;
	public AudioClip[] swish;
	public AudioClip[] drop;
	public AudioClip alert;
	public AudioClip timeOut;
	public AudioClip[] star;
	public AudioClip[] gameOver;
	public AudioClip cash;

	public AudioClip[] destroy;
	public AudioClip boostBomb;
	public AudioClip boostColorReplace;
	public AudioClip explosion;
	public AudioClip getStarIngr;
	public AudioClip strippedExplosion;
	public AudioClip[] complete;
	public AudioClip block_destroy;
	public AudioClip wrongMatch;
	public AudioClip noMatch;
	public AudioClip appearStipedColorBomb;
	public AudioClip appearPackage;
	public AudioClip destroyPackage;
	public AudioClip colorBombExpl;

	///SoundBase.Instance.audio.PlayOneShot( SoundBase.Instance.kreakWheel );

	// Use this for initialization
	void Awake ()
	{
		if (transform.parent == null) {
			transform .parent = Camera.main.transform;
			transform .localPosition = Vector3.zero;
		}
		DontDestroyOnLoad (gameObject);
		Instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
