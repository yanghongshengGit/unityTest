using UnityEngine;
using System.Collections;

namespace DataManager
{
    //定义图块
    public enum BaseElement
    {
        None = 0,       //无
        Jin = 1,        //金
        Mu,             //木
        Shui,           //水
        Huo,            //火
        Tu,             //土
		Count,			//总数
    }
	//定义技能等级
	public enum SkillLv
	{
		SL_Elementary = 3,	//初级
		SL_Medium = 4,		//中级
		SL_Senior = 5		//高级
	}
    //定义技能数据
    public enum SkillData
    {
		None 	= BaseElement.Count,    												//无
		Jin_3	= BaseElement.Count + BaseElement.Jin * SkillLv.SL_Elementary,    		//锐金利剑
		Jin_4	= BaseElement.Count + BaseElement.Jin * SkillLv.SL_Medium,      		//金钟罩
		Jin_5	= BaseElement.Count + BaseElement.Jin * SkillLv.SL_Senior,      		//黄金领域
		Mu_3	= BaseElement.Count + BaseElement.Mu * SkillLv.SL_Elementary,       	//刺血荆棘
		Mu_4	= BaseElement.Count + BaseElement.Mu * SkillLv.SL_Medium,          		//回春符
		Mu_5	= BaseElement.Count + BaseElement.Mu * SkillLv.SL_Senior,          		//禁咒：万物复苏
		Shui_3	= BaseElement.Count + BaseElement.Shui * SkillLv.SL_Elementary,     	//水愈术
		Shui_4	= BaseElement.Count + BaseElement.Shui * SkillLv.SL_Medium,        		//冰冻结界
		Shui_5	= BaseElement.Count + BaseElement.Shui * SkillLv.SL_Senior,        		//禁咒：水幕天华
		Huo_3	= BaseElement.Count + BaseElement.Huo * SkillLv.SL_Elementary,     		//白炽火球
		Huo_4	= BaseElement.Count + BaseElement.Huo * SkillLv.SL_Medium,         		//大火球
		Huo_5	= BaseElement.Count + BaseElement.Huo * SkillLv.SL_Senior,         		//禁咒：赤炎红莲
		Tu_3	= BaseElement.Count + BaseElement.Tu * SkillLv.SL_Elementary,       	//土刺
		Tu_4	= BaseElement.Count + BaseElement.Tu * SkillLv.SL_Medium,          		//岩钢咒
		Tu_5	= BaseElement.Count + BaseElement.Tu * SkillLv.SL_Senior          		//禁咒：大地守护
    }
    //定义包头
    public enum NetPackage
    {
        NP_Login = 1,   //登录
        NP_Register,    //注册
        NP_Match,       //匹配玩家
        NP_Exit,        //退出
        NP_PersonInfo,  //获取个人信息
        NP_Friend,      //获取好友列表
        NP_AddFriend,   //填加好友
        NP_AnswerAdd,   //填加好友消息回复
        NP_Competition, //挑战消息
        NP_ChatRecord,  //获取聊天记录
        NP_SendMessage, //发送聊天消息
        NP_Operator,    //操作消息
        NP_UpdatePerson,//更新人物信息
        NP_UpLoadArray  //上传初始数组
    }

    public class DataDefine
	{
        //定义数据常量
        public const int MaxGrade = 50;     	//最大等级
        public const int BasicHP = 100;     	//基础血量
        public const int AddHP = 5;         	//升级增加血量
        public const int RowNum = 9;        	//图块行数
        public const int ColNum = 9;        	//图块列数
		public const int MinEliminateNum = 3;	//最低消除数量
    }
}
