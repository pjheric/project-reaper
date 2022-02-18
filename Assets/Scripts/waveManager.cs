using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    [SerializeField]
    GameObject locus;
    public GameObject player1;
    public GameObject player2;
    [SerializeField]
    float mapRadius;

    [System.Serializable]
    public struct waveConfiguration
    {
        public int basicEnemySpawnCount;
        public int fastEnemySpawnCount;
        public int tankEnemySpawnCount;
        public int buffEnemySpawnCount;
    }
    [SerializeField]
    public waveConfiguration[] waves = new waveConfiguration[5];

    float t = 3.0f;

    public static int basicEnemyCount;
    public static int fastEnemyCount;
    public static int tankEnemyCount;
    public static int buffEnemyCount;
    public static int enemyCount;
    public static int currentWaveNum = 0;//starting at 0   
    waveConfiguration currentWave;
    
    public bool waveRunning = false;
    bool beginOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mainManager.gameStart && beginOnce)
        {
            beginOnce = false;
            currentWaveNum = 0;
            startWave();
        }
        runWave();
    }
    void waveEnd()
    {
        waveRunning = false;
        currentWaveNum += 1;
        locus.GetComponent<Entity>().addHealth(50);
        player1.GetComponent<Entity>().addHealth(1000);//heal to full
        player2.GetComponent<Entity>().addHealth(1000);//heal to full
        Invoke("startWave",5);
    }
    void startWave()
    {
        waveRunning = true;
        currentWave = waves[currentWaveNum];
    }
    void runWave()
    {
        t += Time.deltaTime;
        if(t > 0.1f && currentWave.basicEnemySpawnCount > 0 && waveRunning)
        {
            Vector2 pos = new Vector2(Random.Range(-(mapRadius + 20), (mapRadius + 20)),Random.Range(-(mapRadius + 20), (mapRadius + 20)));
            while(Vector2.Distance(locus.transform.position, pos) < mapRadius || Vector2.Distance(locus.transform.position, pos) > (mapRadius+20))
            {
                pos = new Vector2(Random.Range(-(mapRadius + 20), (mapRadius + 20)),Random.Range(-(mapRadius + 20), (mapRadius + 20)));//get a new pos until it is within the 2 circles
            }
            GameObject newEnemy = Instantiate(basicEnemyPrefab, pos, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().locus = locus;
            newEnemy.GetComponent<Enemy>().player1 = player1;
            newEnemy.GetComponent<Enemy>().player2 = player2;
            t = 0.0f;
            enemyCount += 1;
            currentWave.basicEnemySpawnCount -= 1;
        }
        if (enemyCount <= 0 && waveRunning)
        {
            waveEnd();
        }
    }
}
