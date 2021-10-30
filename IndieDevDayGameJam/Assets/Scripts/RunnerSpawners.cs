using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawners : MonoBehaviour
{
    private bool canSpawnEnemies = true;
    public Transform[] spawners;
    public Transform[] decorationSpawners;
    public Transform[] cloudSpawners;
    public List<int> randomTransforms;
    public List<int> bonusTransforms;
    public List<GameObject> enemyPrefab;
    public List<GameObject> decorationPrefab;
    public List<GameObject> cloudPrefab;
    public GameObject bonusPrefab;
    public GameObject specialBonusPrefab;
    protected float enemiesVelocity;
    public float time;
    protected List<DifficultyLevel> levels;
    public int rounds;
    public RunnerController runnerController;
    public int freeSpaces;

    private bool canSpawnDecoration = true;
    private bool canSpawnClouds = true;

    public Level level = Level.Easy;

    public enum Level
    {
        Easy,
        Normal,
        Hard,
        Nightmare,
        FindGapEasy,
        FindGapNormal,
        FindGapHard,
        FindGapNightmare,
    }

    private void Start()
    {
        SetDifficultyLevel(level);
    }

    public void SetDifficultyLevel(Level level)
    {
        //superslow = 0.0009f;
        //slow = 0.0007f;
        //normal = 0.001f;
        //fast = 0.002f;

        switch (level)
        {
        case Level.Easy:
            DifficultyLevel easy = new DifficultyLevel();
            easy.enemiesVelocity = 3;
            enemiesVelocity = easy.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 1.5f;
            runnerController.spawnRate = 4;
            runnerController.unfillingTime = 0.0007f;
            break;
        case Level.Normal:
            DifficultyLevel normal = new DifficultyLevel();
            normal.enemiesVelocity = 3.8f;
            enemiesVelocity = normal.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 1.2f;
            runnerController.spawnRate = 4;
            runnerController.unfillingTime = 0.0005f;
            break;
        case Level.Hard:
            DifficultyLevel hard = new DifficultyLevel();
            hard.enemiesVelocity = 4.5f;
            enemiesVelocity = hard.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 1f;
            runnerController.spawnRate = 4;
            runnerController.unfillingTime = 0.0003f;
            break;
        case Level.Nightmare:
            DifficultyLevel nightmare = new DifficultyLevel();
            nightmare.enemiesVelocity = 5.5f;
            enemiesVelocity = nightmare.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 0.5f;
            runnerController.spawnRate = 4;
            runnerController.unfillingTime = 0.0001f;
            break;
        case Level.FindGapEasy:
            DifficultyLevel findGapEasy = new DifficultyLevel();
            findGapEasy.enemiesVelocity = 4;
            enemiesVelocity = findGapEasy.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 1.5f;
            runnerController.spawnRate = 4;
            runnerController.unfillingTime = 0.001f;
            break;
        case Level.FindGapNormal:
            DifficultyLevel findGapNormal = new DifficultyLevel();
            findGapNormal.enemiesVelocity = 4.3f;
            enemiesVelocity = findGapNormal.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 1.4f;
            runnerController.spawnRate = 4;
            runnerController.unfillingTime = 0.0008f;
            break;
        case Level.FindGapHard:
            DifficultyLevel findGapHard = new DifficultyLevel();
            findGapHard.enemiesVelocity = 4.6f;
            enemiesVelocity = findGapHard.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 1.5f;
            runnerController.spawnRate = 5;
            runnerController.unfillingTime = 0.0007f;
            break;
        case Level.FindGapNightmare:
            DifficultyLevel findGapNightmare = new DifficultyLevel();
            findGapNightmare.enemiesVelocity = 5.5f;
            enemiesVelocity = findGapNightmare.enemiesVelocity;
            runnerController.textureMapFakeVelocity.scrollSpeed = 3;
            runnerController.spawnCooldown = 1.3f;
            runnerController.spawnRate = 6;
            runnerController.unfillingTime = 0.0006f;
            break;
        }
    }

    protected void GetRandomSpawnPoints()
    {
        randomTransforms.Clear();
        bonusTransforms.Clear();

        for(int i = 0; i < runnerController.spawnRate; i++)
        {
            GetRandomInt();
        }
    }

    protected void GetRandomInt()
    {
        bool existsInList = false;
        int rand = Random.Range(0, spawners.Length);
        foreach(int i in randomTransforms)
        {
            if(i == rand)
            {
                existsInList = true;
            }
        }

        if(existsInList == true)
        {
            GetRandomInt();
        }
        else
        {
            randomTransforms.Add(rand);
        }
    }

    protected void GetBonusSpawns()
    {
        bool existsInList = false;
        int rand = Random.Range(0, spawners.Length);
        foreach (int i in randomTransforms)
        {
            if (i == rand)
            {
                existsInList = true;
            }
        }

        if (existsInList == true)
        {
            GetBonusSpawns();
        }
        else
        {
            bonusTransforms.Add(rand);
        }
    }

    protected void SpawnEnemiesAndBonus()
    {
        if (canSpawnEnemies == true)
        {
            StartCoroutine(SpawnWithCooldown());
        }
    }

    protected IEnumerator SpawnWithCooldown()
    {
        canSpawnEnemies = false;
        GetRandomSpawnPoints();

        int randBonus = Random.Range(0, 3);
        for(int i = 0; i < randBonus; i++)
        { 
            GetBonusSpawns();
        }

        for (int i = 0; i < randomTransforms.Count; i++)
        {
            Transform targetTransform = spawners[randomTransforms[i]];
            int randEnemy = Random.Range(0, enemyPrefab.Count);
            GameObject enemyInstance = Instantiate(enemyPrefab[randEnemy]);
            enemyInstance.GetComponent<RunnerEntity>().enemiesVelocity = enemiesVelocity;
            enemyInstance.transform.parent = targetTransform;
            enemyInstance.transform.position = targetTransform.position;
            enemyInstance.transform.rotation = targetTransform.rotation;
        }

        bool regularbonusSpawned = false;
        for (int i = 0; i < bonusTransforms.Count; i++)
        {
            Transform targetTransform = spawners[bonusTransforms[i]];
            GameObject bonusInstance = null;
            if (regularbonusSpawned == false)
            { 
                bonusInstance = Instantiate(bonusPrefab);
                regularbonusSpawned = true;
                bonusInstance.GetComponent<RunnerEntity>().enemiesVelocity = enemiesVelocity;
                bonusInstance.transform.parent = targetTransform;
                bonusInstance.transform.position = targetTransform.position;
                bonusInstance.transform.rotation = targetTransform.rotation;
            }/*
            else
            {
                bonusInstance = Instantiate(specialBonusPrefab);
                regularbonusSpawned = false;
            }
            */
            
        }

        yield return new WaitForSeconds(runnerController.spawnCooldown);
        canSpawnEnemies = true;
    }

    protected IEnumerator SpawnLimitDecoration()
    {
        canSpawnDecoration = false;
        for (int i = 0; i < decorationSpawners.Length; i++)
        {
            Transform targetTransform = decorationSpawners[i];
            int rand = Random.Range(0, decorationPrefab.Count);
            GameObject decorationInstance = Instantiate(decorationPrefab[rand]);
            decorationInstance.GetComponent<RunnerEntity>().enemiesVelocity = enemiesVelocity;
            decorationInstance.transform.parent = targetTransform;
            decorationInstance.transform.position = targetTransform.position;
            decorationInstance.transform.rotation = targetTransform.rotation;
        }
        yield return new WaitForSeconds(0.15f);
        canSpawnDecoration = true;
    }

    protected IEnumerator SpawnClouds()
    {
        canSpawnClouds = false;
        int randCloudAmount = Random.Range(0, cloudSpawners.Length);
        for (int i = 0; i < randCloudAmount; i++)
        {
            Transform targetTransform = cloudSpawners[i];
            int rand = Random.Range(0, cloudPrefab.Count);
            GameObject cloudInstance = Instantiate(cloudPrefab[rand]);
            cloudInstance.GetComponent<RunnerEntity>().enemiesVelocity = enemiesVelocity;
            cloudInstance.transform.parent = targetTransform;
            cloudInstance.transform.position = targetTransform.position;
            cloudInstance.transform.rotation = targetTransform.rotation;
        }
        yield return new WaitForSeconds(randCloudAmount);
        canSpawnClouds = true;
    }

    protected void Update()
    {
        SpawnEnemiesAndBonus();

        if (canSpawnDecoration == true)
        {
            StartCoroutine(SpawnLimitDecoration());
        }

        if (canSpawnClouds == true)
        {
            StartCoroutine(SpawnClouds());
        }
    }
}
