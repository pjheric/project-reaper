using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum enemyType
{
    basic, erratic, tank, buff
}

public class waveManager : MonoBehaviour
{

    public DialogueManager dm;
    [SerializeField] GameObject dmParent;
    [SerializeField] public GameObject[] prefabs = new GameObject[4];
    [SerializeField] GameObject locus;
    public GameObject player1;
    public GameObject player2;
    [SerializeField] float mapRadius;
    [SerializeField] float waveSpawnsPerSecond;

    [System.Serializable]
    public struct waveConfiguration
    {
        public int[] enemyCountsWave;//acces using enums
    }

    [SerializeField]
    public waveConfiguration[] waves = new waveConfiguration[5];

    float t = 3.0f;

    public static int[] enemyCounts = new int[4];//replaces next 4 lines using enums
                                                 // public static int basicEnemyCount;
                                                 // public static int erraticEnemyCount;
                                                 // public static int tankEnemyCount;
                                                 // public static int buffEnemyCount;
    public static int enemyCount;
    public static int currentWaveNum = 0;//starting at 0   
    waveConfiguration currentWave;

    public bool waveRunning = false;
    bool beginOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        dm = dmParent.GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (/*mainManager.gameStart*/ PersistentData.isGameStarted && beginOnce)
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
        Invoke("startWave", 5);
    }
    void startWave()
    {
        waveRunning = true;
        currentWave = waves[currentWaveNum];
        dm.StartDialogue(); 
    }
    void runWave()
    {
        t += Time.deltaTime;
        //every 0.2 seconds while the wave is running do a spawn tick
        if (t > (1.0f/waveSpawnsPerSecond) && waveRunning)
        {
            //go through the 4 enemy types
            for (int i = 0; i < prefabs.Length; i += 1)
            {
                //spawn the enemy
                SpawnEnemy((enemyType)i);
            }
            t = 0.0f;

        }
        if (enemyCount <= 0 && waveRunning)
        {
            waveEnd();
        }
    }

    void SpawnEnemy(enemyType enemy)
    {
        if (currentWave.enemyCountsWave[(int)enemy] > 0)
        {
            Vector2 pos = new Vector2(Random.Range(-(mapRadius + 20), (mapRadius + 20)), Random.Range(-(mapRadius + 20), (mapRadius + 20)));
            while (Vector2.Distance(locus.transform.position, pos) < mapRadius || Vector2.Distance(locus.transform.position, pos) > (mapRadius + 20))
            {
                pos = new Vector2(Random.Range(-(mapRadius + 20), (mapRadius + 20)), Random.Range(-(mapRadius + 20), (mapRadius + 20)));//get a new pos until it is within the 2 circles
            }
            GameObject newEnemy = Instantiate(prefabs[(int)enemy], pos, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().locus = locus;
            newEnemy.GetComponent<Enemy>().player1 = player1;
            newEnemy.GetComponent<Enemy>().player2 = player2;
            enemyCount += 1;
            enemyCounts[(int)enemy] += 1;
            currentWave.enemyCountsWave[(int)enemy] -= 1;
        }
    }
}
