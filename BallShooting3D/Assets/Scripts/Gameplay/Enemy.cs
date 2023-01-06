using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private bool _isBoss = false;
    private int _healthRemain = 10; // for boss only

    // Start is called before the first frame update
    void Start()
    {
        if (_isBoss == true)
        {
            this.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            this.GetComponent<NavMeshAgent>().speed = 1.5f;
        } else
        {
            this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            this.GetComponent<NavMeshAgent>().speed = 0.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            UIManager.Instance.ShowLosePanel();
            Debug.Log("Game Over");
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
                this.transform.localScale -= new Vector3(0.06f, 0.06f, 0.06f);
                if (_healthRemain == 0)
                {
                    Dead();
                }
            }

        }
    }

    void Dead()
    {
        LevelManager.Instance.KillEnemy();
        AudioManager.Instance.PlaySoundEffect("enemy_scream");
        Destroy(gameObject);
    }
}
