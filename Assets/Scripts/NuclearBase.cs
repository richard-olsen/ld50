using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearBase : MonoBehaviour
{
    private int hp = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 getRandomPoint()
    {
        float angle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
        return (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 4.5f) + (Vector2)transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.right * transform.localScale.x * .66f, Vector3.up);
        Debug.DrawRay(getRandomPoint(), Vector3.up);
    }
}
