using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rounds : MonoBehaviour
{
    const int MAX_GRUNTS = 15;

    [SerializeField]
    private AudioSource sfxRoundStart;

    private int currentRound = 0;
    public int CurrentRound { get => currentRound; }

    private int baseGruntCount = 8; // 8 spawn on round 1, this is also used to variably increase
    private int baseGruntHP = 2;

    private int gruntTargetSpawn = 0;
    private int gruntHP = 0;
    private int totalGruntsSpawned = 0;
    private bool roundOver = false;

    // After round 5, they'll target a random silo
    private int roundThreshold1 = 5;

    List<Grunts> grunts = new List<Grunts>();

    [SerializeField]
    private Text roundText;
    [SerializeField]
    private Text gruntsLeft;

    [SerializeField]
    private Transform spawnLocation;

    IEnumerator playRoundStart()
    {
        for (int i = 0; i < 3; i++)
        {
            sfxRoundStart.Play();
            yield return new WaitForSeconds(0.4f);
        }
        yield return null;
    }
    IEnumerator spawnGrunts()
    {
        yield return new WaitForSeconds(10.0f); // Wait ten seconds before spawning an enemy

        for (int i = 0; i < gruntTargetSpawn; i++)
        {
            bool waited = false;
            while (grunts.Count >= MAX_GRUNTS)
            {
                waited = true;
                removeDeadGrunts();
                yield return new WaitForSeconds(10.0f); // Wait ten seconds before sweeping again to check for dead grunts
            }
            if (!waited)
            {
                removeDeadGrunts();
            }

            Grunts grunt = Instantiate(Resources.Load<GameObject>("Grunt")).GetComponent<Grunts>();

            float rad = Random.Range(0, Mathf.PI * 2.0f);
            float dist = Random.Range(0, 10);

            grunt.transform.position = spawnLocation.position + new Vector3(Mathf.Cos(rad) * dist, Mathf.Sin(rad) * dist, 0);
            grunt.setHP(gruntHP);
            grunts.Add(grunt);
            totalGruntsSpawned++;

            if (currentRound > roundThreshold1)
            {
                int range = Random.Range(0, 4);

                if (range > 3)
                    grunt.GetComponent<GruntAI>().chasePlayer();
                else
                    grunt.GetComponent<GruntAI>().chaseSilo();
            }
            else
                grunt.GetComponent<GruntAI>().chasePlayer();

            yield return new WaitForSeconds(getTimeBetweenSpawns());
        }

        while (grunts.Count > 0)
        {
            removeDeadGrunts();
            yield return new WaitForSeconds(2.0f);
        }

        grunts.Clear();
        roundOver = true;

        yield return null;
    }

    private float getTimeBetweenSpawns()
    {
        if (currentRound > 15)
        {
            return 0.2f;
        }
        else if (currentRound > 10)
        {
            return Random.Range(.5f, 2);
        }
        else if (currentRound > 0)
        {
            return Random.Range(1.0f, 2.0f);
        }
        return 1.0f;
    }
    private void removeDeadGrunts()
    {
        if (grunts.Count == 0)
            return;

        for (int i = grunts.Count - 1; i >= 0; i--)
        {
            if (grunts[i].IsDead)
                grunts.RemoveAt(i);
        }
    }

    public void endRound()
    {
        StopAllCoroutines();
        foreach (Grunts grunt in grunts)
        {
            grunt.damage(int.MaxValue, false);
        }
        grunts.Clear();
    }
    public void startNewRound()
    {
        endRound();

        currentRound++;
        if (currentRound == 1)
        {
            gruntHP = baseGruntHP;
            gruntTargetSpawn = baseGruntCount;
        }
        else
        {
            gruntHP = (int)(baseGruntHP * currentRound);
            gruntTargetSpawn = (int)(baseGruntCount * (Mathf.Log10(currentRound * 15) * 1.2f));
        }
        totalGruntsSpawned = 0;
        roundOver = false;

        StartCoroutine(playRoundStart());
        StartCoroutine(spawnGrunts());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            startNewRound();
    
        if (roundOver)
        {
            startNewRound();
        }

        roundText.text = currentRound.ToString();
        gruntsLeft.text = ""; // Try to find a better calculation. This is just total left to spawn
        //gruntsLeft.text = (gruntTargetSpawn - totalGruntsSpawned).ToString();
    }
}
