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

namespace Assets.Plugins.SmartLevelsMap.Scripts
{
	public class Path : MonoBehaviour
	{
        [HideInInspector]
		public List<Transform> Waypoints = new List<Transform>();

        [HideInInspector]
        public bool IsCurved;

        [HideInInspector]
        public Color GizmosColor = Color.white;

        [HideInInspector]
	    public float GizmosRadius = 10.0f;

        public void OnDrawGizmos()
		{
			Gizmos.color = GizmosColor;
			for (int i = 0; i < Waypoints.Count; i++)
			{
                Gizmos.DrawSphere(Waypoints[i].transform.position, GizmosRadius);
			    if (i < Waypoints.Count - 1)
			        DrawPart(i);
			}
		}

		private void DrawPart(int ind)
		{
		    if (IsCurved)
		    {
		        Vector2[] devidedPoints = GetDivededPoints(ind);
		        for (int i = 0; i < devidedPoints.Length - 1; i++)
		            Gizmos.DrawLine(devidedPoints[i], devidedPoints[i + 1]);
		    }
		    else
		    {
                Gizmos.DrawLine(Waypoints[ind].position, Waypoints[(ind + 1) % Waypoints.Count].position);
		    }
		}

	    private Vector2[] GetDivededPoints(int ind)
	    {
	        Vector2[] points = new Vector2[11];
	        int pointInd = 0;
	        int[] indexes = GetSplinePointIndexes(ind, true);
	        Vector2 a = Waypoints[indexes[0]].transform.position;
	        Vector2 b = Waypoints[indexes[1]].transform.position;
	        Vector2 c = Waypoints[indexes[2]].transform.position;
	        Vector2 d = Waypoints[indexes[3]].transform.position;
	        for (float t = 0; t <= 1.001f; t += 0.1f)
	            points[pointInd++] = SplineCurve.GetPoint(a, b, c, d, t);
	        return points;
	    }

	    public int[] GetSplinePointIndexes(int baseInd, bool isForwardDirection)
	    {
	        int dInd = isForwardDirection ? 1 : -1;
	        return new int[]
	        {
	            Mathf.Clamp(baseInd - dInd, 0, Waypoints.Count - 1),
	            baseInd,
	            Mathf.Clamp(baseInd + dInd, 0, Waypoints.Count - 1),
	            Mathf.Clamp(baseInd + 2*dInd, 0, Waypoints.Count - 1)
	        };
	    }

	    public float GetLength()
        {
            float length = 0;
            for (int i = 0; i < Waypoints.Count; i++)
            {
                Vector2 p1 = Waypoints[i].transform.position;
                Vector2 p2 = Waypoints[(i + 1) % Waypoints.Count].transform.position;
                length += Vector2.Distance(p1, p2);
            }
            return length;
        }

	    public float GetLength(int startInd)
	    {
	        return Vector2.Distance(
	            Waypoints[startInd].transform.position,
                Waypoints[(startInd + 1) % Waypoints.Count].transform.position);
	    }

	}
}
