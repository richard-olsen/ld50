using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{    
    [SerializeField] private GameObject enemySkin;
    [SerializeField] private int spawnInterval = 10;


    void Start()
    {

        StartCoroutine(spawnInterval, enemySkin);
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy) {
      yield return new WaitForSeconds(interval);
      GameObject newEnemy = Instantiate( enemy, new Vector(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quanternion.identity );
      StartCoroutine(spawnEnemy(interval, enemy));
    }
}
