using UnityEngine;
using System.Collections;
using DataManager;
using System.Collections.Generic;

public class BlockUI : MonoBehaviour {
	//消散粒子
	public GameObject particle;
	// 图块的模型
	public GameObject[] baseElementType = new GameObject[(int)BaseElement.Count];
	// 图块数组
	private GameObject[,] myGameObject = new GameObject[DataDefine.RowNum, DataDefine.ColNum];
	// 数据处理对象
	private DataDeal dataDealer = new DataDeal();
	// 射线
	public Ray myRay;
	// 射线碰撞信息
	public RaycastHit Hit;
	// 交换位置对象1
	GameObject object1 = null;
	// 交换位置对象2
	GameObject object2 = null;
	// 交换位置1
	Vector3 position1;
	// 交换位置2
	Vector3 position2;


	// 根据数组生成图块
	public void refreshBlock(int[,] blockArray)
	{
		for(int i = 0; i < DataDefine.RowNum; i++)
		{
			for(int j = 0; j < DataDefine.ColNum; j++)
			{
				//删除原来图块
				if (myGameObject [i, j] != null)
					Destroy (myGameObject [i, j]);
				//生成图块
				myGameObject[i,j] = Instantiate(baseElementType[blockArray[i, j]],	//原型预设
					new Vector3(i,j,0),												//坐标
					Quaternion.identity) as GameObject;								//角度
			}
		}
	}
		
	// 图块交换动作
	private void blockExchangeAnimate(GameObject obj1, GameObject obj2)
	{
		if (obj1 == null || obj2 == null)
			return;
		
		Vector3 position1 = obj1.transform.position;
		Vector3 position2 = obj2.transform.position;

		iTween.MoveTo (obj1, iTween.Hash ("position", position2, "time", 1f));
		iTween.MoveTo (obj2, iTween.Hash ("position", position1, "time", 1f));

	}
	//图块交换
	private void blockExchange(int row1, int col1, int row2, int col2)
	{
		if (row1 >= DataDefine.RowNum && row1 < 0 && row2 >= DataDefine.RowNum && row2 < 0 &&
			col1 >= DataDefine.ColNum && col1 < 0 && col2 >= DataDefine.ColNum && col2 < 0)
			return;

		GameObject obj1 = myGameObject [row1, col1];
		GameObject obj2 = myGameObject [row2, col2];

		//交换
		myGameObject [row1, col1] = obj2;
		myGameObject [row2, col2] = obj1;

		//图块交换动作
		blockExchangeAnimate (obj1, obj2);

		//底层数据交换
		dataDealer.Exchange(row1, col1, row2, col2);
	}
	// 获取鼠标下坐标
	private Vector3 getMousePosition()
	{
		// 发射射线
		myRay = GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
		// 射线碰撞
		if (Physics.Raycast (myRay, out Hit)) {	
			// 在场景视图中绘制射线  
			Debug.DrawLine(myRay.origin, Hit.point, Color.red);

			return Hit.collider.transform.position;
		}
		
		return Vector3.zero;
	}
	// 图块消除
	private void removeBlock()
	{
		//获取消除列表
		bool[,] deleteArray = dataDealer.outDeleteArray ();
		//遍历消除列表
		for (int i = 0; i < DataDefine.RowNum; i++)
			for (int j = 0; j < DataDefine.ColNum; j++) 
			{
				if (deleteArray [i, j] == true) 
				{
					// 删除对应对象
					Destroy(myGameObject[i,j]);
					// 播放删除粒子效果
					Instantiate(particle, new Vector3(i,j,0), Quaternion.identity);
				}
			}	
	}
	//下落
	private void fall(GameObject obj, int distance)
	{
		if (!(distance > 0))
			return;
		
		Vector3 position = obj.transform.position;
		position.y += distance;
		iTween.MoveTo (obj, iTween.Hash ("position", position, "time", 1f));
	}
	// 图块下落
	private void fallBlock()
	{
		//获取下落距离列表
		int[,] fallDistance = dataDealer.Fall();
		//遍历列表
		for (int i = 0; i < DataDefine.RowNum; i++)
			for (int j = 0; j < DataDefine.ColNum; j++) 
			{
				int distance = fallDistance [i, j];
				fall (myGameObject [i, j], distance);
			}
		//填补
		List<FillPiece> fillList;
		int[,] newData = dataDealer.Fill (out fillList);
		foreach (FillPiece piece in fillList) 
		{
			Vector3 position = Vector3.zero;
			position.x = piece.col;
			position.y = 0;
			GameObject block = Instantiate (baseElementType [piece.type], position, Quaternion.identity) as GameObject;
			fall (block, piece.row);
		}
		//刷新myGameObject

	}
	// Use this for initialization
	void Start () 
	{
		//生成初始数组
		int[,] startArray = dataDealer.initArray();

		//生成界面
		refreshBlock(startArray);

		bool[,] deleteArray = dataDealer.outDeleteArray ();
		deleteArray [2, 3] = true;
		deleteArray [2, 4] = true;
		deleteArray [2, 5] = true;
		//
		List<SkillData> skillList;
		dataDealer.GetSkillList (out skillList);

		removeBlock();

		fallBlock ();
	}
	
	// Update is called once per frame
	private bool isClicked = false;
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			position1 = getMousePosition ();
			Debug.Log(position1);
			//Debug.Log(Mathf.RoundToInt(position1.x), Mathf.RoundToInt(position1.y));
			Destroy (myGameObject[(int)position1.x, (int)position1.y]);

		}
//		else if (Input.GetMouseButtonUp (0)) 
//		{
//			position2 = getMousePosition ();
//			Debug.Log (position2);
//			Debug.Log(myGameObject[(int)position1.x, (int)position1.y]);
//		}
//		if (Input.GetMouseButtonDown (0)) 
//		{
//			if (!isClicked) 
//			{
//				GameObject block = getMouseObject (Input.mousePosition);
//				Debug.Log (Input.mousePosition);
//				if (block != null) 
//				{
//					if (obj1 == null)
//						obj1 = block;
//					else if (obj2 == null) 
//					{
//						obj2 = block;
//						blockExchangeAnimate (obj1, obj2);
//						obj1 = null;
//						obj2 = null;
//					}
//				}
//			}
//		} 
//		else if (Input.GetMouseButtonUp (0)) 
//		{
//			GameObject block = getMouseObject (Input.mousePosition);
//			if (block != null)
//				obj2 = block;
//
//			blockExchangeAnimate (obj1, obj2);
//			obj1 = null;
//			obj2 = null;
//			isClicked = false;
//		}
		
	}
}
