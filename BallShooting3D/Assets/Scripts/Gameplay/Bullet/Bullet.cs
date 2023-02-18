using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _bounce;
    public float _maxLifeTime = 10f;

    Rigidbody _rb;

    void Start()
    {
        _bounce = ConfigurationUtil.BulletBounce;
        _rb = GetComponent<Rigidbody>();
        
        PoolManager.Instance.bulletPooler.OnReturnToPool(this.gameObject, _maxLifeTime);
    }

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
