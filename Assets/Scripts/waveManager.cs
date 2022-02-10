using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveManager : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    [SerializeField]
    GameObject locus;
    [SerializeField]
    GameObject player1;
    [SerializeField]
    GameObject player2;
    [SerializeField]
    float mapRadius;
    float t = 0.0f;

    public static int basicEnemyCount;
    public static int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > 0.05f && enemyCount < 100)
        {
            Vector2 pos = new Vector2(Random.Range(-(mapRadius + 20), (mapRadius + 20)),Random.Range(-(mapRadius + 20), (mapRadius + 20)));
            while(Vector2.Distance(locus.transform.position, pos) < mapRadius || Vector2.Distance(locus.transform.position, pos) > (mapRadius+20))
            {
                pos = new Vector2(Random.Range(-90,90),Random.Range(-90,90));//get a new pos until it is within the 2 circles
            }
            GameObject newEnemy = Instantiate(basicEnemyPrefab, pos, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().locus = locus;
            newEnemy.GetComponent<Enemy>().player1 = player1;
            newEnemy.GetComponent<Enemy>().player2 = player2;
            t = 0.0f;
            enemyCount += 1;
        }
    }
}
