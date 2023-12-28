using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/**
* @description:��Ϸҳ��
* @author: dazzi
* @time: 2023.07
*/
public class GamePanel : MonoBehaviour
{
    private Button btn_Pause;
    private Button btn_Play;
    private Text txt_Score;
    private Text txt_Diamond;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowGamePanel,Show);
        EventCenter.AddListener<int>(EventDefine.UpdateScoreText,UpdateScoreText);
        EventCenter.AddListener<int>(EventDefine.UpdateDiamondText, UpdateDiamondText);
        Init();
    }

    private void Init()
    {
        btn_Pause = transform.Find("btn_pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OnPauseButtonClick);
        btn_Play = transform.Find("btn_play").GetComponent<Button>();
        btn_Play.onClick.AddListener(OnPlayButtonClick);
        txt_Score = transform.Find("txt_score").GetComponent<Text>();
        txt_Diamond = transform.Find("Diamond/txt_diamond").GetComponent<Text>();
        btn_Play.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGamePanel,Show);
        EventCenter.RemoveListener<int>(EventDefine.UpdateScoreText, UpdateScoreText);
        EventCenter.RemoveListener<int>(EventDefine.UpdateDiamondText, UpdateDiamondText);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void UpdateScoreText(int score)
    {
        txt_Score.text = score.ToString();
    }
    private void UpdateDiamondText(int diamond) {
        txt_Diamond.text = diamond.ToString();
    }
    /// <summary>
    /// ��ͣ��ť���
    /// </summary>
    private void OnPauseButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayClikAudio);

        btn_Play.gameObject.SetActive(true);
        btn_Pause.gameObject.SetActive(false);
        //��Ϸ��ͣ
        Time.timeScale = 0;
        GameManager.Instance.IsPause = true;
    }
    /// <summary>
    /// ��ʼ��ť���
    /// </summary>
    private void OnPlayButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayClikAudio);

        btn_Play.gameObject.SetActive(false);
        btn_Pause.gameObject.SetActive(true);
        //������Ϸ
        Time.timeScale = 1;
        GameManager.Instance.IsPause = false;
    }
}
