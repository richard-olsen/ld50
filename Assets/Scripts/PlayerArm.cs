using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    [SerializeField]
    private Gun gun;

    float angle;

    public GameObject bulletSpawnTemplate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = transform.position;
        pos.z = 0;
        mouseWorldPos.z = 0;
        Vector3 direction = (mouseWorldPos - pos).normalized;

        angle = Vector3.Angle(Vector3.right, direction);
        if (direction.y < 0)
            angle *= -1.0f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        angle *= Mathf.Deg2Rad;

        if (Input.GetButtonDown("Shoot"))
        {
            if (!(gun is null))
            {
                gun.beginShooting(angle);
            }
        }
        if (Input.GetButtonUp("Shoot"))
        {
            if (!(gun is null))
            {
                gun.endShooting();
            }
        }
        if (Input.GetButtonDown("Reload"))
        {
            if (!(gun is null))
            {
                gun.reload();
            }
        }
    }

    public float getAngle()
    {
        return angle;
    }
}
