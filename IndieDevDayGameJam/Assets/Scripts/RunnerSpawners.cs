using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSpawners : MonoBehaviour
{
    private bool canSpawnEnemies = true;
    public Transform[] spawners;
    public Transform[] decorationSpawners;
    public List<int> randomTransforms;
    public List<int> bonusTransforms;
    public List<GameObject> enemyPrefab;
    public List<GameObject> decorationPrefab;
    public GameObject bonusPrefab;
    public GameObject specialBonusPrefab;
    protected float enemiesVelocity;
    public float time;
    protected List<DifficultyLevel> levels;
    public int rounds;
    public RunnerController runnerController;
    public int freeSpaces;

    private bool canSpawnDecoration = true;

    public Level level = Level.Easy;

    public enum Level
    {
        Easy,
        Normal,
        High
    }

    private void Start()
    {
        SetDifficultyLevel(level);
    }

    public void SetDifficultyLevel(Level level)
    {
            switch (level)
        {
            case Level.Easy:
                DifficultyLevel easy = new DifficultyLevel();
                easy.enemiesVelocity = 3;
                enemiesVelocity = easy.enemiesVelocity;
                easy.playerRotationVelocity = 0.5f;
                runnerController.playerRotationVelocity = easy.playerRotationVelocity;
                easy.freeSpaces = 7;
                runnerController.textureMapFakeVelocity.scrollSpeed = 3;
                freeSpaces = easy.freeSpaces;
                runnerController.spawnCooldown = 1.5f;
                break;
        }
    }

    protected void GetRandomSpawnPoints()
    {
        randomTransforms.Clear();
        bonusTransforms.Clear();

        for(int i = 0; i<spawners.Length-runnerController.spawnRate; i++) //place how many things to spawn here
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
            }
            else
            {
                bonusInstance = Instantiate(specialBonusPrefab);
                regularbonusSpawned = false;
            }
            bonusInstance.GetComponent<RunnerEntity>().enemiesVelocity = enemiesVelocity;
            bonusInstance.transform.parent = targetTransform;
            bonusInstance.transform.position = targetTransform.position;
            bonusInstance.transform.rotation = targetTransform.rotation;
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
        yield return new WaitForSeconds(0.2f);
        canSpawnDecoration = true;
        //Check if player has lost;
        StartCoroutine(SpawnLimitDecoration());
    }

    protected void Update()
    {
        SpawnEnemiesAndBonus();
        if (canSpawnDecoration == true)
        {
            StartCoroutine(SpawnLimitDecoration());
        }
    }
}
