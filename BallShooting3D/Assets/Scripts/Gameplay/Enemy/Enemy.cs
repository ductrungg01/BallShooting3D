using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public bool _isBoss = false;
    public Animator _anim;
    private int _healthRemain = 10; // for boss only

    public bool isDead = false;

    void Start()
    {
        RandomBoss();

        if (_isBoss)
        {
            this.transform.localScale = new Vector3(2, 2, 2);
        }
    }

    void RandomBoss()
    {
        float tmp = Random.Range(0, 100);
        
        if (tmp < 15)
        {
            _isBoss = true;
        }
        
        Debug.Log(tmp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }
        
        if (other.CompareTag("MainCharacter"))
        {
            _anim.SetInteger("state", 3);
            Debug.Log("Game Over");
            
            // Kill the player
            other.gameObject.GetComponent<Player>().Dead();
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
                this.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                if (_healthRemain == 0)
                {
                    Dead();
                }
            }

        }
    }

    private async UniTask Dead()
    {
        isDead = true;

        AudioManager.Instance.PlaySoundEffect("enemy_scream");
        
        _anim.SetInteger("state", 2);
        
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        
        this.gameObject.SetActive(false);
    }
}
