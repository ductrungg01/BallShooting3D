using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletByTouch : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float delayTime = 0.2f;
    private float delay = 0.0f;

    [SerializeField]
    private Animator anim;

    Rigidbody _mcRb;
    LightOfSight _lightOfSight;

    // Start is called before the first frame update
    void Start()
    {
        _mcRb = GetComponent<Rigidbody>();
        _lightOfSight = FindObjectOfType<LightOfSight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.Instance.isLoading) return;

        delay -= Time.deltaTime;

        // TODO: replace user clicking on phone screen instead of mouse click
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            var lookDir = hit.point - transform.position;
            
            float angle = Mathf.Atan2(lookDir.x, lookDir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, angle, transform.rotation.z));

            if (Input.GetButtonUp("Fire1") && delay <= 0)
            {
                delay = delayTime;

                SpawnBullet(lookDir);
            }

            if (Input.GetButton("Fire1"))
            {
                //_lightOfSight.SetMcDir(lookDir);
                _lightOfSight.SetIsShow(true);
                anim.SetInteger("state", 1);
            } else
            {
                _lightOfSight.SetIsShow(false);
                anim.SetInteger("state", 0);
            }
        }
    }

    void SpawnBullet(Vector3 lookDir)
    {
        Vector3 gunPos = new Vector3(0, 0.2f, 0);
        float bulletSpeed = 15f;

        // Spawn the bullet
        GameObject spawnedBullet = Instantiate(bullet, this.transform.position + gunPos, Quaternion.identity);

        Vector3 bulletDir = lookDir;

        // Get direction only, don't care about lenght of vector
        bulletDir.y = 0;
        float maxXZ = Mathf.Max(Mathf.Abs(bulletDir.x), Mathf.Abs(bulletDir.z));
        bulletDir.x /= maxXZ;
        bulletDir.z /= maxXZ;

        // Set bullet's velocity
        spawnedBullet.GetComponent<Rigidbody>().velocity = bulletDir * bulletSpeed;
    }
}
