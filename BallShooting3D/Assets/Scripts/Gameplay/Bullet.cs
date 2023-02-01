using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _bounce;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.bulletList.Add(this.gameObject);
        _bounce = ConfigurationUtil.BulletBounce;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velo = _rb.velocity;
        if (velo.magnitude < ConfigurationUtil.BulletSpeed)
        {
            velo *= ConfigurationUtil.BulletSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.PlaySoundEffect("billiard_collision");

        if (collision.gameObject.CompareTag("Wall"))
        {
            this._bounce--;
            if (this._bounce < 0)
            {
                PoolManager.Instance.bulletPooler.OnReturnToPool(gameObject);
                //Destroy(gameObject);
            }
        }
    }

    public void SetBound(int value)
    {
        this._bounce = value;
    }
}
