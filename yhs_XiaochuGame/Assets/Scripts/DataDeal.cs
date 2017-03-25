using System.Collections.Generic;
using System;
using UnityEngine;

namespace DataManager
{
	//填充图块信息
    public struct FillPiece
    {
        public int row;
        public int col;
        public int type;
    }
    //继承单例类
	public class DataDeal
    {
		
		private int[,] coreArray;                               //存图块类型的数组
		private bool[,] deleteArray;                            //消除的图块

		//构造函数
		public DataDeal()
		{
			this.Init();
		}
		//初始化
		private void Init() 
		{
			coreArray = new int[DataDefine.RowNum, DataDefine.ColNum];
			deleteArray = new bool[DataDefine.RowNum, DataDefine.ColNum];
			clearDeleteArray(deleteArray);						//清空消除数组
		}

		//返回消除数组
        public bool[,] outDeleteArray()
        {
            return deleteArray;
        }

		//返回图块数组
        public int[,] outCoreArray()
        {
            return coreArray;
        }

		//清空消除数组
		private void clearDeleteArray(bool[,] deleteArray)
        {
            for(int i = 0; i < DataDefine.RowNum; i++)
            {
                for(int j = 0; j < DataDefine.ColNum; j++)
                {
                    deleteArray[i, j] = false;
                }
            }
        }

		//初始化数组
		public int[,] initArray()
		{
			//随机对象
			System.Random rand = new System.Random();
			for (int i = 0; i < DataDefine.RowNum; i++)
			{
				for (int j = 0; j < DataDefine.ColNum; j++)
				{
					coreArray[i, j] = rand.Next(1, 6);
				}
			}
			return coreArray;
		}

        public void InputArray(int[,] array)                    //输入数组
        {
            for (int i = 0; i < DataDefine.RowNum; i++)
                for (int j = 0; j < DataDefine.ColNum; j++)
                    coreArray[i, j] = array[i, j];
			clearDeleteArray (deleteArray);
        }

		public void Exchange(int row1, int col1, int row2, int col2)	//交换两个位置数据
		{
			if (row1 >= DataDefine.RowNum && row1 < 0 && row2 >= DataDefine.RowNum && row2 < 0 &&
			   col1 >= DataDefine.ColNum && col1 < 0 && col2 >= DataDefine.ColNum && col2 < 0)
				return;

			//交换
			int temp = coreArray [row1, col1];
			coreArray [row1, col1] = coreArray [row2, col2];
			coreArray [row2, col2] = temp;
		}

        public void ScanArray()                                 //扫描数组
        {
            for (int i = 0; i < DataDefine.RowNum; i++)
                for (int j = 0; j < DataDefine.ColNum; j++)
                {
                    CheckPiece(i, j);
                }
        }

        private void CheckPiece(int x, int y)                   //检查该图块的周围类型
        {      
            int count = 1;										//一行或一列消除图块计数
            int temX = x;
            int temY = y;
			int minCount = DataDefine.MinEliminateNum;			//最低消除数量

			//右边
			//找右边连续同色的图块数量
            while(temX + 1 < DataDefine.ColNum)
            {
                if(coreArray[y, temX] == coreArray[y, temX + 1])
                {
                    count++;
                    temX++;
                }
                else
                {
                    break;
                }
            }
			//如果消除图块数量大于等于最低数量就标记这些图块为删除图块
			if(count >= minCount)
            {
                for (int i = 0; i < count; i++)
                    deleteArray[y, x + i] = true;
            }

            //下边
            count = 1;
			//找下边连续同色的图块数量
            while (temY + 1 < DataDefine.RowNum)
            {
                if (coreArray[temY, x] == coreArray[temY+1, x])
                {
                    count++;
                    temY++;
                }
                else
                {
                    break;
                }
            }
			//如果消除图块数量大于等于最低数量就标记这些图块为删除图块
			if (count >= minCount)
            {
                for (int i = 0; i < count; i++)
                    deleteArray[y+i, x] = true;
            }
        }
        public void GetSkillList(out List<SkillData> skillList) //获取技能释放列表
        {
			skillList = new List<SkillData> ();

			//计数
			int[] typeCount = new int[(int)BaseElement.Count];

			//遍历图块数组，根据消除数组，找到不同图块的要消除的数量，并把消除位置赋为0
            for (int i = 0; i < DataDefine.RowNum; i++)
                for (int j = 0; j < DataDefine.ColNum; j++)
                {
                    if(deleteArray[i, j] == true)
                    {
                        //获得消除图块的数量
						for (int type = 0; type < (int)BaseElement.Count; type++)
						{
							if (coreArray[i,j] == type)
							{
								typeCount[type]++;
								break;
							}
						}

                        //将要消除的位置赋为空
                        coreArray[i, j] = (int)BaseElement.None;
                    }
                }
			
			//根据消除数量添加技能
			for (int type = 0; type < (int)BaseElement.Count; type++)
			{
				if (typeCount [type] == (int)SkillLv.SL_Elementary) 
				{
					int skillType = (int)BaseElement.Count + type * (int)SkillLv.SL_Elementary;
					skillList.Add ((SkillData)skillType);
				}
				else if (typeCount [type] == (int)SkillLv.SL_Medium) 
				{
					int skillType = (int)BaseElement.Count + type * (int)SkillLv.SL_Medium;
					skillList.Add ((SkillData)skillType);
				}
				else if (typeCount [type] >= (int)SkillLv.SL_Senior) 
				{
					int skillType = (int)BaseElement.Count + type * (int)SkillLv.SL_Senior;
					skillList.Add ((SkillData)skillType);
				}
			}
				
        }
		//下落 返回下落距离数组
        public int[,] Fall()
        {
			//下落距离数组，存放每个图块需要下落的距离
            int[,] fallDistanceArray = new int[DataDefine.RowNum, DataDefine.ColNum];
            for (int i = 0; i < DataDefine.RowNum; i++)
            {
                for (int j = 0; j < DataDefine.ColNum; j++)
                {
                    if(coreArray[i, j] == (int)BaseElement.None)
                    {
                        fallDistanceArray[i, j] = 0;
                    }
                    else
                    {
                        fallDistanceArray[i, j] = CheckColNone(i, j);
                    }
                }
            }
			//根据下落距离完成下落
            fallUpdateArray(fallDistanceArray);

            return fallDistanceArray;
        }
		//检查该图块下面空几格 返回下落的距离
        private int CheckColNone(int row, int col)
        {
            int count = 0;
            int temRow = row;
            while(temRow < DataDefine.RowNum)
            {
                if (coreArray[temRow, col] == (int)BaseElement.None)
                    count++;
                temRow++;
            }

            return count;
        }
		//根据下落距离完成下落更新
        private void fallUpdateArray(int[,] fallDistanceArray)
        {
            for (int i = DataDefine.RowNum - 1; i >= 0; i--)
            {
                for (int j = 0; j < DataDefine.ColNum; j++)
                {
                    int fallDistance = fallDistanceArray[i, j];
                    //交换上下
                    int tempType = coreArray[i, j];
                    coreArray[i, j] = coreArray[i + fallDistance, j];
                    coreArray[i + fallDistance, j] = tempType;
                }
            }
        }
		//填补空白
		public int[,] Fill(out List<FillPiece> fillList)
        {
			fillList = new List<FillPiece> ();
			
			//随机对象
            System.Random rand = new System.Random();
			//遍历图块数组找出所有空白
            for (int i = 0; i < DataDefine.RowNum; i++)
            {
                for (int j = 0; j < DataDefine.ColNum; j++)
                {
                    if(coreArray[i, j] == (int)BaseElement.None)
                    {
						//随机1-5的数字，填充空白
                        coreArray[i, j] = rand.Next(1, 6);
						//生成填充图块对象
                        FillPiece fillpiece = new FillPiece();
                        fillpiece.row = i;
                        fillpiece.col = j;
                        fillpiece.type = coreArray[i, j];
						//添加到列表
                        fillList.Add(fillpiece);
                    }
                }
            }
			return coreArray;
        }


		public bool ScanArray2()                                 //扫描数组
		{
			//扫描数组，符合条件，就在deleteArray标记
			for (int i = 0; i < DataDefine.RowNum; i++)
				for (int j = 0; j < DataDefine.ColNum; j++)
				{
					bool[,] tempDeleteArray = new bool[DataDefine.RowNum, DataDefine.ColNum];
					clearDeleteArray (tempDeleteArray);

					if(CheckPiece2(i, j, tempDeleteArray)>1)
						copyToDeleteArray (tempDeleteArray);
				}
			//在deleteArray中有标记，可以消除
			for (int i = 0; i < DataDefine.RowNum; i++)
				for (int j = 0; j < DataDefine.ColNum; j++)
					if (deleteArray [i, j] == true)
						return true;
			return false;
		}
		private int CheckPiece2(int x, int y, bool[,] tempDeleteArray)
		{
			tempDeleteArray[x,y] = true;

			// 检查左边
			int nLeft = 0;
			if (x > 0) 
			{  
				// 如果左边的颜色和自己的一样, 并且左边还没有被标记
				if ((false == tempDeleteArray[x-1,y]) && (coreArray[x-1,y] == coreArray[x,y]))  
				{  
					tempDeleteArray[x-1,y] = true; 
					nLeft = CheckPiece2(x-1, y, tempDeleteArray);  
				}  
			}

			// 检查右边
			int nRight = 0;
			if (x+1 < DataDefine.ColNum) 
			{  
				// 如果右边的颜色和自己的一样, 并且右边还没有被标记
				if ((false == tempDeleteArray[x+1,y]) && (coreArray[x+1,y] == coreArray[x,y]))  
				{  
					tempDeleteArray[x+1,y] = true; 
					nRight = CheckPiece2(x+1, y, tempDeleteArray);  
				}  
			}

			// 检查下边
			int nDown = 0;
			if (y > 0) 
			{  
				// 如果左边的颜色和自己的一样, 并且左边还没有被标记
				if ((false == tempDeleteArray[x,y-1]) && (coreArray[x,y-1] == coreArray[x,y]))  
				{  
					tempDeleteArray[x,y-1] = true; 
					nDown = CheckPiece2(x, y-1, tempDeleteArray);  
				}  
			}

			// 检查上边
			int nUp = 0;
			if (y+1 < DataDefine.RowNum) 
			{  
				// 如果右边的颜色和自己的一样, 并且右边还没有被标记
				if ((false == tempDeleteArray[x,y+1]) && (coreArray[x,y+1] == coreArray[x,y]))  
				{  
					tempDeleteArray[x,y+1] = true; 
					nUp = CheckPiece2(x, y+1, tempDeleteArray);  
				}  
			}

			return nLeft + nRight + nDown + nUp + 1;
		}

		//tempDeleteArray copyto DeleteArray
		private void copyToDeleteArray(bool[,] tempDeleteArray)
		{
			for(int i = 0; i < DataDefine.RowNum; i++)
			{
				for(int j = 0; j < DataDefine.ColNum; j++)
				{
					if (tempDeleteArray [i, j] == true)
					{
						deleteArray [i, j] = true;
					}
				}
			}
		}

		//伪随机产生新数组
		//随机条件：
    }
}
