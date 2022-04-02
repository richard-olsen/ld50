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

    bool shootGun = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public IEnumerator shootFullAuto()
    //{
    //    while (shootGun)
    //    {
    //        GameObject bulletObject = GameObject.Instantiate(bulletSpawnTemplate);
    //        bulletObject.transform.position = bulletSpawn.position;
    //        Bullet bullet = bulletObject.GetComponent<Bullet>();
    //        bullet.setDirection(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //    yield return null;
    //}
    //public IEnumerator shootSemiAuto()
    //{
    //    while (shootGun)
    //    {
    //        for (int i = 0; i < 3; i++)
    //        {
                
    //            yield return new WaitForSeconds(0.1f);
    //        }
    //        yield return new WaitForSeconds(0.4f);
    //    }
    //    yield return null;
    //}

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

        if (Input.GetMouseButtonDown(1))
        {
            if (!(gun is null))
            {
                gun.beginShooting(angle);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
        }
    }
}
