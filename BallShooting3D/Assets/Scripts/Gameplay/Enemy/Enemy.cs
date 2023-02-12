using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool _isBoss = false;
    public Animator _anim;
    private int _healthRemain = 10; // for boss only

    public float speedForBoss = 1f;
    public float speedForNormal = 0.5f;

    void Start()
    {
        if (_isBoss == true)
        {
            this.GetComponent<NavMeshAgent>().speed = speedForBoss;
        } else
        {
            this.GetComponent<NavMeshAgent>().speed = speedForNormal;
        }

        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            _anim.SetInteger("state", 1);
            Debug.Log("Game Over");
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Bullet"))
        {
            if (!_isBoss)
            {
                Dead();
            }
            else
            {
                _healthRemain--;
                this.transform.localScale -= new Vector3(0.05f, 0.05f, 0.05f);
                if (_healthRemain == 0)
                {
                    Dead();
                }
            }

        }
    }

    void Dead()
    {
        AudioManager.Instance.PlaySoundEffect("enemy_scream");
        this.gameObject.SetActive(false);
    }
}
