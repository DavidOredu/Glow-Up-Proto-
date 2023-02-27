using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MoreMountains.Feedbacks;

public class ScoreUI : MonoBehaviour
{
    [Header("IN-GAME UI")]
    public TextMeshProUGUI mainScoreText;
    public TextMeshProUGUI distanceCoveredText;
    //  public TextMeshProUGUI currentScoreMultiplierText;

  //  [Header("GAME OVER POPUP")]


    public MMFeedbacks scoreChangeFeedback;
    public MMFeedbacks multiplierChangeFeedback;
    public MMFeedbacks newHighScoreFeedback;
    public MMFeedbacks newMaxKillsFeedback;
    public void PlayCollsionFeedback()
    {
        scoreChangeFeedback.PlayFeedbacks();
        multiplierChangeFeedback.PlayFeedbacks();
    }

    public void PlayDamageFeedback()
    {
       
    }
    void Awake()
    {
    //    GameManager.OnGameStart += SetMultiplierTextColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameEnd += SetGameOverStats;
    }
    private void SetMultiplierTextColor()
    {
      //  currentScoreMultiplierText.color = Resources.Load<PlayerData>("PlayerData").color;
    }
    // Update is called once per frame
    void Update()
    {
        mainScoreText.text = ScoreSystem.GameScore.scores[ConstNames.MAIN_SCORE].ToString("N0");
     //   distanceCoveredText.text = ScoreSystem.GameScore.scores[ConstNames.DISTANCE_COVERED].ToString("N0");

        //currentScoreMultiplierText.gameObject.SetActive(ScoreSystem.GameScore.currentScoreMultiplier > 1);
        //currentScoreMultiplierText.text = "X" + ScoreSystem.GameScore.currentScoreMultiplier.ToString();
    }
    private void SetGameOverStats()
    {

    }

    
}
