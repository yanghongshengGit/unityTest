﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 计时器组件 （挂在GameController游戏对像下）
/// 职责，游戏计时器，当此计时器为0时，当前游戏结束。
/// </summary>
public class Timer : MonoBehaviour
{

    public static Timer timer;

    //thay doi game time => thay doi timerbarprocess
    /// <summary>
    /// 本局游戏剩余时间
    /// </summary>
    public float GameTime = 270;

    /// <summary>
    /// 剩余游戏时间条
    /// </summary>
    public UnityEngine.UI.Image Timebar;

    public Texture2D timebarTexture;

    /// <summary>
    /// 计时器更新组件
    /// </summary>
    public TimerUpdate update;

    public GameObject NoSelect;

    public GameObject PauseUI;

    public GameObject WinUI;

    public GameObject LoseUI;

    public GameObject Nomove;

    private float _time;

    private const int ClassicBaseScore = 5000;

    private int ClassicTargetScore;

    public int ScoreStack = 0;

    private bool startplus;

    public bool isAds;

    public bool isreq;

    public enum GameState
    {
        PLAYING = 0,
        PAUSE = 1,
        WIN = 2,
        LOST = 3,
    }

    void Awake()
    {
        timer = this;
    }
    void Start()
    {
        _time = GameTime;
        Timebar.fillAmount = 0;
        StartCoroutine(AdsCd());
    }

    public void TimeTick(bool b)
    {
        if (b && PLayerInfo.MODE == 1)
        {
            //开始计时
            update.enabled = true;
        }
        else
        {
            //结束计时
            update.enabled = false;
        }

    }

    /// <summary>
    /// 更新游戏进度条
    /// </summary>
    /// <param name="time"></param>
    void timebarprocess(float time)
    {
        //计算时间进度条填充量
        float fillamount = time / _time;

        //更新剩余时间条
        Timebar.fillAmount = fillamount;
    }

    public void ScoreBarProcess(int score)
    {
        ScoreStack += score;
        if (!startplus)
        {
            startplus = true;
            StartCoroutine(IEScoreBarProcess());
        }


    }
    IEnumerator IEScoreBarProcess()
    {
        while (ScoreStack > 0 && GameController.action.GameState == (int)GameState.PLAYING)
        {
            ScoreStack -= 10;
            if (PLayerInfo.Info.Score + 10 < 5000 * PLayerInfo.MapPlayer.Level)
            { PLayerInfo.Info.Score += 10; }
            else
            {
                PLayerInfo.Info.Score = 5000 * PLayerInfo.MapPlayer.Level;
                break;
            }
            float fillamount = PLayerInfo.Info.Score / (5000f * PLayerInfo.MapPlayer.Level);
            Timebar.fillAmount = fillamount;
            yield return null;
        }

        startplus = false;
    }

    /// <summary>
    /// 滴答一下
    /// </summary>
    public void Tick()
    {
        if (GameTime > 0 && GameController.action.GameState == (int)GameState.PLAYING)
        {
            // Time.deltaTime 以秒计算，完成最后一帧的时间（只读）。使用这个函数使和你的游戏帧速率无关。
            GameTime -= Time.deltaTime;
            timebarprocess(GameTime);
        }
        else if (GameController.action.GameState == (int)GameState.PLAYING)
        {
            //游戏结束
            GameController.action.GameState = (int)GameState.LOST;
            GameTime = 0;
            Lost();
            //结束计时
            update.enabled = false;
        }
    }

    public void Win()
    {
        GameController.action.GameState = (int)GameState.WIN;
        NoSelect.SetActive(true);
        StartCoroutine(IEWin());
        //Debug.Log("WIN");
    }
    public void Lost()
    {
        GameController.action.GameState = (int)GameState.LOST;
        NoSelect.SetActive(true);
        EffectSpawner.effect.SetScore(PLayerInfo.Info.Score);
        StartCoroutine(DisableAll());
        SoundController.Sound.Lose();
        showFullAds();
        //Debug.Log("LOSE");
    }
    public void Pause()
    {
        SoundController.Sound.Click();
        if (GameController.action.GameState == (int)GameState.PLAYING)
        {
            GameController.action.GameState = (int)GameState.PAUSE;
            NoSelect.SetActive(true);
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

    }
    public void Resume()
    {
        SoundController.Sound.Click();
        if (GameController.action.GameState == (int)GameState.PAUSE)
        {
            GameController.action.GameState = (int)GameState.PLAYING;
            Time.timeScale = 1;
            NoSelect.SetActive(false);
            PauseUI.SetActive(false);
        }
    }
    public void Restart()
    {
        if (PLayerInfo.MODE == 1)
        {
            PLayerInfo.Info.Score = 0;
            ButtonActionController.Click.ArcadeScene(PLayerInfo.MapPlayer);
        }
        else
        {
            ButtonActionController.Click.ClassicScene(1);
        }
    }
    public void Home()
    {
        ButtonActionController.Click.SelectMap(PLayerInfo.MODE);
    }
    public void Next()
    {
        ButtonActionController.Click.SelectMap(1);
        if (PLayerInfo.MapPlayer.Level < 297)
            CameraMovement.StarPointMoveIndex = PLayerInfo.MapPlayer.Level;
        else
            CameraMovement.StarPointMoveIndex = -1;
    }

    public void Music(UnityEngine.UI.Button button)
    {
        ButtonActionController.Click.BMusic(button);
    }
    public void Sound(UnityEngine.UI.Button button)
    {

    }
    public void ClassicLvUp()
    {
        GameController.action.GameState = (int)GameState.WIN;
        NoSelect.SetActive(true);
        StartCoroutine(UpLevel());

    }
    IEnumerator DisableAll()
    {
        DisableJewel(false);
        yield return new WaitForSeconds(0.75f);
        LoseUI.SetActive(true);
    }
    IEnumerator IEWin()
    {
        DisableJewel(true);
        EffectSpawner.effect.StarWinEffect(GameController.action.JewelStar.gameObject.transform.position);
        SoundController.Sound.Win();
        GameController.action.JewelStar.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        WinUI.SetActive(true);
        showFullAds();
    }

    void showFullAds()
    {
        if (isAds)
        {
            GoogleMobileAdsScript.advertise.ShowInterstitial();
            isAds = false;
            isreq = false;
        }
    }

    IEnumerator UpLevel()
    {
        DisableJewel(true);
        showFullAds();
        yield return new WaitForSeconds(1f);
        ButtonActionController.Click.ClassicScene(PLayerInfo.MapPlayer.Level + 1);
    }

    public void DisableJewel(bool b)
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                if (!b)
                {
                    if (JewelSpawner.spawn.JewelGribScript[x, y] != null)
                        JewelSpawner.spawn.JewelGribScript[x, y].JewelDisable();
                }
                else
                {
                    if (JewelSpawner.spawn.JewelGribScript[x, y] != null && JewelSpawner.spawn.JewelGribScript[x, y] != GameController.action.JewelStar)
                        JewelSpawner.spawn.JewelGribScript[x, y].JewelDisable();
                }
            }
        }
    }

    IEnumerator AdsCd()
    {
        while (true)
        {
            yield return new WaitForSeconds(119f);
            isAds = true;
        }
    }
}
