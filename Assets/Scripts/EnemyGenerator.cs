using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING}; 

    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        public float rate;
        public int count;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float difficultyFactor = 1f;
    private float searchCountdown = 1f;
    public SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            // TERMINARAM TODAS AS WAVES
            difficultyFactor++;

            for (int i = 0; i < waves.Length; i++)
            {
                Enemy currentEnemy = waves[i].enemy.GetComponent<Enemy>();
                currentEnemy.setDifficulty(difficultyFactor);
                
            }

            nextWave = 1;

        }
        else
        {
            nextWave++;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0f)
            {
                return false;
            }
        }

        return true;
    }


    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
         
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;

    }

    void SpawnEnemy(GameObject _enemy)
    {
        Instantiate(_enemy, new Vector3(Random.Range(-5, 5),transform.position.y, 0f), Quaternion.identity);
    }

}
