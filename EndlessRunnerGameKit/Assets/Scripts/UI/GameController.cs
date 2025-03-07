using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Leaderboards;
using System.Threading.Tasks;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private GameObject timeoutMenu;
    [SerializeField] private GameObject audioTrigger;
    private bool flag = false;

    // Update is called once per frame
    void Update()
    {
        if(NewPlayer.Instance.firstLanded)
        {
            audioTrigger.SetActive(true);
        }

        if(NewPlayer.Instance.health <= 0 && NewPlayer.Instance.currentTime > 0 && NewPlayer.Instance.coins < NewPlayer.Instance.max_coins)
        {
            NewPlayer.Instance.frozen = true;
            deathMenu.SetActive(true);
        }

        if(NewPlayer.Instance.coins == NewPlayer.Instance.max_coins && NewPlayer.Instance.currentTime > 0 && !flag)
        {   
            VictoryAsync();
            flag = true;
        }

        if(NewPlayer.Instance.currentTime <= 0 && NewPlayer.Instance.coins < NewPlayer.Instance.max_coins)
        {
            NewPlayer.Instance.runRightSpeed = 0;
            timeoutMenu.SetActive(true);
        }
    }

    public async void VictoryAsync()
    {
        try
        {
            NewPlayer.Instance.runRightSpeed = 0;
            NewPlayer.Instance.stopTime = true;
            victoryMenu.SetActive(true);
            float newTime = NewPlayer.Instance.startTime - NewPlayer.Instance.currentTime;
            int newScore = CalculateScore(newTime);
            AddScoreAsync(0);
            await Task.Delay(300);
            var playerscore = await LeaderboardsService.Instance.GetPlayerScoreAsync("dreamrunner2025");
            int oldScore = (int)(playerscore.Score);
            if(newScore > oldScore)
            {
                AddScoreAsync(newScore - oldScore);
                oldScore = newScore;
            }
            GameObject.Find("VictoryMenu/Victory").GetComponent<Text>().text = "本局用时：" + newTime.ToString("F2") + "s";
            GameObject.Find("VictoryMenu/ScoreShow").GetComponent<Text>().text = "本局得分：" + newScore.ToString();
            GameObject.Find("VictoryMenu/ScoreShowHistory").GetComponent<Text>().text = "历史得分：" + oldScore.ToString(); 
        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }

    }

    public async void AddScoreAsync(int score)
    {
        try
        {
            var playerEntry = await LeaderboardsService.Instance.AddPlayerScoreAsync("dreamrunner2025", score);
        }
        catch (Exception exception)
        {
            Debug.Log(exception.Message);
        }
    }

    public int CalculateScore(float time)
    {
        return 30000 - Mathf.FloorToInt(100 * time);
    }

}
