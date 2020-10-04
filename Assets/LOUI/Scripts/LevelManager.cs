using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelState levelState;

    public bool endless;

    public static int highScore;
    public int score;
    public Text scoreText;
    public Text hiScoreText;

    public Transform planetTf;

    public Hp levelHp;
    public SpawnData[] spawns;
    public int spawnIndex;

    public Enemy[] rockPool;
    public int rockIndex;

    public Enemy[] colorPool;
    public int colorIndex;

    public Transform[] spawnPoints;

    public Pickup[] pickupPool;
    public int pickupIndex;


    //

    public float randDelayMin;
    public float randDelayMax;
    public float randSpeedMin;
    public float randSpeedMax;

    readonly float speedMinIncrement = 0.001f;
    readonly float speedMaxIncrement = 0.0011f;
    readonly float delayMinMultiplier = 0.999f;
    readonly float delayMaxMultiplier = 0.998f;

    [Header("Title Animations")]
    [SerializeField] Animator titleAnim;
    [SerializeField] Animator tutorialAnim;
    [SerializeField] Animator hudAnim;
    [SerializeField] Animator buttonAnim;
    [SerializeField] Animator gameOverAnim;


    public Fader fader;
    public PlayerCtrl player;

    private void Start()
    {

        StartCoroutine(StartRoutine());

        //levelState = LevelState.Playing;

        //if (endless)
        //    StartCoroutine(EndlessRoutine());
        //else
        //    StartCoroutine(LevelRoutine());

        //StartCoroutine(ScoreRoutine());
    }

    private void Update()
    {
        if(levelState == LevelState.Playing && levelHp.hp <= 0)
        {
            Debug.Log("LOSE");
            StopAllCoroutines();
            StartCoroutine(ReturnRoutine());
        }
    }

    public IEnumerator StartRoutine()
    {
        hiScoreText.enabled = false;
        scoreText.enabled = false;

        yield return new WaitForSeconds(1.2f);

        StartCoroutine(fader.FadeIn());

        yield return new WaitForSeconds(2f);

        tutorialAnim.SetTrigger("Show");
        
        yield return new WaitForSeconds(2.2f);

        buttonAnim.SetTrigger("Show");
    }

    public IEnumerator ReturnRoutine()
    {
        foreach (Enemy e in rockPool)
            e.Kill(0);

        foreach (Enemy e in colorPool)
            e.Kill(0);

        levelState = LevelState.Lose;

        gameOverAnim.SetTrigger("Show");
        hudAnim.SetTrigger("Hide");

        yield return new WaitForSeconds(3.6f);

        titleAnim.SetTrigger("Show");
        scoreText.enabled = false;
        hiScoreText.enabled = true;
        hiScoreText.text = "High Score: " + highScore.ToString();

        yield return new WaitForSeconds(1f);

        tutorialAnim.SetTrigger("Show");

        yield return new WaitForSeconds(1f);

        player.collectComponent.ResetCollected();
        player.planetHp.SetFullHealth();
        buttonAnim.SetTrigger("Show");
        levelState = LevelState.None;
    }

    public IEnumerator PlayRoutine()
    {
        scoreText.enabled = true;
        titleAnim.SetTrigger("Hide");
        buttonAnim.SetTrigger("Hide");
        tutorialAnim.SetTrigger("Hide");

        yield return new WaitForSeconds(2f);

        hudAnim.SetTrigger("Show");
        levelState = LevelState.Playing;

        if (endless)
            StartCoroutine(EndlessRoutine());
        else
            StartCoroutine(LevelRoutine());

        StartCoroutine(ScoreRoutine());
    }

    public IEnumerator QuitRoutine()
    {
        StartCoroutine(fader.FadeOut());

        yield return new WaitForSeconds(4.5f);

        Application.Quit();
    }

    public IEnumerator EndlessRoutine()
    {
        SpawnData spawn = new SpawnData();

        spawn.spawnDelay = Random.Range(randDelayMin, randDelayMax);
        randDelayMin *= delayMinMultiplier;
        randDelayMax *= delayMaxMultiplier;

        spawn.spawnPointIndex = Random.Range(0, spawnPoints.Length);

        spawn.startSpeed = Random.Range(randSpeedMin, randSpeedMax);
        randSpeedMin += speedMinIncrement;
        randSpeedMax += speedMaxIncrement;


        int rType = Random.Range(0, 6);
        int rColor = Random.Range(0, 6);
        
        if (rType < 4)
            spawn.enemyType = EnemyType.Rock;
        else
            spawn.enemyType = EnemyType.Color;

        if (rColor < 4)
            spawn.color = ShotType.Red;
        else
            spawn.color = ShotType.Blue;
        
        yield return new WaitForSeconds(spawn.spawnDelay);

        switch (spawn.enemyType)
        {
            case EnemyType.Rock:

                rockPool[rockIndex].Activate(
                    spawnPoints[spawn.spawnPointIndex].position,
                    Vector3.zero,
                    spawn.startSpeed * (planetTf.position - spawnPoints[spawn.spawnPointIndex].position));

                rockIndex = rockIndex >= rockPool.Length - 1 ? 0 : rockIndex + 1;

                break;

            case EnemyType.Color:

                colorPool[colorIndex].Activate(
                    spawnPoints[spawn.spawnPointIndex].position,
                    Vector3.zero,
                    spawn.startSpeed * (planetTf.position - spawnPoints[spawn.spawnPointIndex].position),
                    spawn.color);

                colorIndex = colorIndex >= colorPool.Length - 1 ? 0 : colorIndex + 1;

                break;
        }

        StartCoroutine(EndlessRoutine());
    }

    public IEnumerator LevelRoutine()
    {
        Debug.Log("LevelRoutine: Spawn No " + spawnIndex.ToString());

        yield return new WaitForSeconds(spawns[spawnIndex].spawnDelay);

        switch (spawns[spawnIndex].enemyType)
        {
            case EnemyType.Rock:

                rockPool[rockIndex].Activate(
                    spawnPoints[spawns[spawnIndex].spawnPointIndex].position,
                    Quaternion.LookRotation(spawns[spawnIndex].startSpeed * (planetTf.position - spawnPoints[spawns[spawnIndex].spawnPointIndex].position)).eulerAngles,
                    spawns[spawnIndex].startSpeed *(planetTf.position - spawnPoints[spawns[spawnIndex].spawnPointIndex].position));

                rockIndex = rockIndex >= rockIndex - 1 ? 0 : rockIndex + 1;

                break;
            case EnemyType.Color:

                colorPool[colorIndex].Activate(
                    spawnPoints[spawns[spawnIndex].spawnPointIndex].position,
                    Quaternion.LookRotation(spawns[spawnIndex].startSpeed * (planetTf.position - spawnPoints[spawns[spawnIndex].spawnPointIndex].position)).eulerAngles,
                    spawns[spawnIndex].startSpeed * (planetTf.position - spawnPoints[spawns[spawnIndex].spawnPointIndex].position));

                colorIndex = colorIndex >= colorIndex - 1 ? 0 : colorIndex + 1;

                break;
        }

        if(spawnIndex >= spawns.Length - 1)
        {
            levelState = LevelState.Win;
            StartCoroutine(WinRoutine());
        }
        else
        {
            spawnIndex++;
            StartCoroutine(LevelRoutine());
        }
    }

    public IEnumerator WinRoutine()
    {
        Debug.Log("WIN");

        yield return new WaitForSeconds(1f);


    }

    public IEnumerator ScoreRoutine()
    {
        yield return new WaitForSeconds(1);
        score++;
        scoreText.text = score.ToString();

        if (score > highScore)
            highScore = score;

        StartCoroutine(ScoreRoutine());
    }

    public void SpawnPickup(Vector3 v)
    {
        pickupPool[pickupIndex].Activate(v);
        pickupIndex = pickupIndex >= pickupPool.Length - 1 ? 0 : pickupIndex + 1;
    }
}

public enum LevelState
{
    None,
    Playing,
    Win,
    Lose
}

public enum EnemyType
{
    Rock,
    Color
}

[System.Serializable]
public struct SpawnData
{
    public EnemyType enemyType;
    public ShotType color;
    public int spawnPointIndex;
    public float startSpeed;
    public float spawnDelay;
}
