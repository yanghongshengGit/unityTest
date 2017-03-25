/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Plugins.SmartLevelsMap.Scripts
{
	public class MapCamera : MonoBehaviour
	{
		private Vector2 _prevPosition;
		private Transform _transform;

		public Camera Camera;
		public Bounds Bounds;

		public void Awake ()
		{
			_transform = transform;
		}

		public void OnDrawGizmos ()
		{
			Gizmos.DrawWireCube (Bounds.center, Bounds.size);
		}

		public void Update ()
		{

#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
			HandleTouchInput();
#else
			HandleMouseInput ();
#endif
		}

		private void HandleTouchInput ()
		{
			if (Input.touchCount == 1) {

				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began) {
					_prevPosition = touch.position;
				} else if (touch.phase == TouchPhase.Moved) {
					Vector2 curPosition = touch.position;
					MoveCamera (_prevPosition, curPosition);
					_prevPosition = curPosition;
				}
			}
		}

		private void HandleMouseInput ()
		{
			if (Input.GetMouseButtonDown (0))
				_prevPosition = Input.mousePosition;
			
			if (Input.GetMouseButton (0)) {
				Vector2 curMousePosition = Input.mousePosition;
				MoveCamera (_prevPosition, curMousePosition);
				_prevPosition = curMousePosition;
			}
		}

		private void MoveCamera (Vector2 prevPosition, Vector2 curPosition)
		{
			if (EventSystem.current.IsPointerOverGameObject (-1))
				return;
			SetPosition (
				transform.localPosition +
				(Camera.ScreenToWorldPoint (prevPosition) - Camera.ScreenToWorldPoint (curPosition)));
		}

		public void SetPosition (Vector2 position)
		{
			Vector2 validatedPosition = ApplyBounds (position);
			_transform = transform;
			_transform.position = new Vector3 (validatedPosition.x, validatedPosition.y, _transform.position.z);
		}

		private Vector2 ApplyBounds (Vector2 position)
		{
			float cameraHeight = Camera.orthographicSize * 2f;
			float cameraWidth = (Screen.width * 1f / Screen.height) * cameraHeight;
			position.x = Mathf.Max (position.x, Bounds.min.x + cameraWidth / 2f);
			position.y = Mathf.Max (position.y, Bounds.min.y + cameraHeight / 2f);
			position.x = Mathf.Min (position.x, Bounds.max.x - cameraWidth / 2f);
			position.y = Mathf.Min (position.y, Bounds.max.y - cameraHeight / 2f);
			return position;
		}
	}
}
