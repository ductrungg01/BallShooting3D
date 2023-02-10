using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBulletByTouch : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float delayTime = 0.1f;
    private float delay = 0.0f;

    [SerializeField]
    private Animator anim;

    LightOfSight _lightOfSight;

    void Start()
    {
        _lightOfSight = FindObjectOfType<LightOfSight>();
    }

    void Update()
    {
        //if (UIManager.Instance.isLoading) return;
        //if (!UIManager.Instance.isPlaying) return;

        delay -= Time.deltaTime;

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
                _lightOfSight.SetIsShow(false);
                anim.SetInteger("state", 1);
            } else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    _lightOfSight.SetIsShow(true);
                }
                anim.SetInteger("state", 0);
            }
        }
    }

    void SpawnBullet(Vector3 bulletDir)
    {
        Vector3 bulletStartHeight = new Vector3(0, 0.8f, 0);
        bulletDir.y = 0;

        GameObject bullet = PoolManager.Instance.bulletPooler.OnTakeFromPool(
                                                    this.transform.position + bulletStartHeight,
                                                    Quaternion.identity);

        // Set bullet's velocity
        bullet.GetComponent<Rigidbody>().velocity = bulletDir.normalized * ConfigurationUtil.BulletSpeed;
    }
}
