using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Spawner : MonoBehaviour
{    
    [SerializeField] private Tilemap spawnZone;
    [SerializeField] private Grunts enemySkin;
    [SerializeField] private int spawnInterval = 10;

    void Start() {

      List<Vector3> tilePostions = GetTilePositions();

      StartCoroutine(spawnEnemy(spawnInterval, enemySkin, tilePostions));
    }

    private List<Vector3> GetTilePositions() {

      List<Vector3> tilePos = new List<Vector3>();
      foreach (var pos in spawnZone.cellBounds.allPositionsWithin) {
        if(spawnZone.GetTile(pos)) {
          Vector3 localPlace = new Vector3(pos.x, pos.y, pos.z);
          tilePos.Add(localPlace);
        }
      }

      return tilePos;
    }

    private IEnumerator spawnEnemy(int interval, Grunts enemy, List<Vector3> tilePostions) {
      yield return new WaitForSeconds(interval);

      int ranNum = Random.Range(0, tilePostions.Count);
      
      Debug.Log(tilePostions[ranNum]);

      Vector3 position = tilePostions[ranNum];
      Grunts newEnemy = Instantiate( enemy,  position, Quaternion.identity );

      StartCoroutine(spawnEnemy(interval, enemy, tilePostions));
    }
}
