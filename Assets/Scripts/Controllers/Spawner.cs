using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{    
    [SerializeField] private Grunts enemySkin;
    [SerializeField] private int spawnInterval = 10;


    void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, enemySkin));
    }

    private IEnumerator spawnEnemy(int interval, Grunts enemy) {
      yield return new WaitForSeconds(interval);

      Vector3 position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
      Grunts newEnemy = Instantiate( enemy,  position, Quaternion.identity );

      StartCoroutine(spawnEnemy(interval, enemy));
    }
}
