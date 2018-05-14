using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    public event Action<int> OnScore;

    public bool isTutorial = false;
    public int ScoreCounter { get; private set; }
    public float TimeCounter { get; private set; }
    public int SessionTime { get; private set; }
    public int TimeAsked { get; private set; }

    private bool isCounting = false;
    private bool levelDone = false;

    private int[] sessionTimes = new int[] { };

    public Level thisLevel = new Level();

    private void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    private void Start()
    {
        SetLevel();
        if (LoadSaveManager.atualGame.levels != null)
        {
            Game g = LoadSaveManager.LoadGame();
            if(g != null)
            {
                int index = 1;
                foreach (Level level in g.levels)
                {
                    string log = "";
                    log += "level index - " + level.levelParams.levelIndex + "\n";
                    log += "time played - " + level.levelParams.timePlayed + "\n";
                    log += "time guess - " + level.levelParams.timeGuess + "\n";
                    log += "score - " + level.point.Count;

                    Debug.Log(log);
                    index++;
                }
            }
        }
    }

    private void Update()
    {

        //APAGAR ##############################
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UI.instance.ShowFinalButtons();
            AddTimeAsked(5);
            LoadSaveManager.LoadGame();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
        //APAGAR ##############################

        ManageTime();
    }

    public void Score()
    {
        ScoreCounter++;
        OnScore.Invoke(ScoreCounter);
        thisLevel.point.Add(TimeCounter);
    }

    public void AddTimeAsked(int time)
    {
        int finalTime = TimeAsked + time;
        finalTime = Mathf.Clamp(finalTime, 0, 300);
        TimeAsked = finalTime;

        if (UI.instance)
        {
            UI.instance.AtualizeFinalCounterText(TimeAsked);
        }
    }

    private void SetLevel()
    {
        int timeIndex = UnityEngine.Random.Range(0, sessionTimes.Length -1);
        SessionTime = UnityEngine.Random.Range(30, 60);

        thisLevel.levelParams = new LevelParams();
        thisLevel.levelParams.timePlayed = SessionTime;
        thisLevel.levelParams.levelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        isCounting = true;
    }

    private void ManageTime()
    {
        if (isCounting && !isTutorial)
        {
            TimeCounter += Time.deltaTime;

            if(TimeCounter >= SessionTime)
            {
                EndLevel();
            }
        }
    }

    private void EndLevel()
    {
        isCounting = false;
        UI.instance.ShowFinalButtons();
        levelDone = true;
    }

    public void NextLevel()
    {
        if (levelDone)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
                nextSceneIndex = 0;
            
            thisLevel.levelParams.timeGuess = TimeAsked;
            LoadSaveManager.atualGame.levels.Add(thisLevel);
            LoadSaveManager.SaveGame(LoadSaveManager.atualGame);

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
