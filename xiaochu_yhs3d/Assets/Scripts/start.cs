using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DataManager;

public class start : MonoBehaviour {
	// 图块的模型
	public GameObject[] baseElementType = new GameObject[(int)BaseElement.Count];
	// 图块数组
	private GameObject[,] myGameObject = new GameObject[DataDefine.RowNum, DataDefine.ColNum];


	// 根据数组生成图块
	private void refreshBlock(int[,] blockArray)
	{
		for(int i = 0; i < DataDefine.RowNum; i++)
		{
			for(int j = 0; j < DataDefine.ColNum; j++)
			{
				if (myGameObject [i, j] != null)
					Destroy (myGameObject [i, j]);
				myGameObject[i,j] = Instantiate(baseElementType[blockArray[i, j]],	//原型预设
												new Vector3(i,j,0),					//坐标
												Quaternion.identity) as GameObject;	//角度
			}
		}
	}

	// Use this for initialization
	void Start () {

//		//生成初始数组
//		int[,] startArray = DataDeal.Instance.initArray();
//
//		//生成界面
//		refreshBlock(startArray);
	}

	private float GameTime = 5.0f;
	void Update () {
//		if (GameTime < 0)
//		{
//			//测试
//			DataDeal dataDeal = DataDeal.Instance;
//
//			//扫描
//			dataDeal.ScanArray2 ();
//			int[,] coreArray = dataDeal.outCoreArray ();
//			bool[,] deleteArray = dataDeal.outDeleteArray ();
//			printArr (coreArray);
//			//消除
//			List<SkillData> skillList;
//			dataDeal.GetSkillList (out skillList);
//			coreArray = dataDeal.outCoreArray ();
//			deleteArray = dataDeal.outDeleteArray ();
//
//			//下落
//			dataDeal.Fall ();
//			coreArray = dataDeal.outCoreArray ();
//			deleteArray = dataDeal.outDeleteArray ();
//
//			//填补
//			List<FillPiece> fillList;
//			dataDeal.Fill (out fillList);
//			coreArray = dataDeal.outCoreArray ();
//			deleteArray = dataDeal.outDeleteArray ();
//			printArr (coreArray);
//			//显示
//			refreshBlock (dataDeal.outCoreArray ());
//
//			GameTime = 5.0f;
//		}
//		GameTime -= Time.deltaTime;
	}
		
}
