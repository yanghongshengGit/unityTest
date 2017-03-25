/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if UNITY_ANDROID || UNITY_IOS

namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;

  internal class AsyncExec {
    private static GameObject asyncExecGameObject;
    private static MonoBehaviour coroutineHost;
    private static AsyncExec asyncImpl;
    private static bool init = false;

    private static MonoBehaviour getImpl() {
      if(!init) {
        asyncImpl = new AsyncExec();
        asyncExecGameObject = new GameObject("Unity Ads Coroutine Host") { hideFlags = HideFlags.HideAndDontSave };
        coroutineHost = asyncExecGameObject.AddComponent<MonoBehaviour>();
        Object.DontDestroyOnLoad(asyncExecGameObject);
        init = true;
      }

      return coroutineHost;
    }

    private static AsyncExec getAsyncImpl() {
      if(!init) {
        getImpl();
      }

      return asyncImpl;
    }

    public static void runWithCallback<K,T>(System.Func<K,System.Action<T>,IEnumerator> asyncMethod, K arg0, System.Action<T> callback) {
      getImpl().StartCoroutine(asyncMethod(arg0, callback));
    }
  }
}

#endif
