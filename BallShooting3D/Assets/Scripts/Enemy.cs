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
            this.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            this.GetComponent<NavMeshAgent>().speed = 1.5f;
        } else
        {
            this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            this.GetComponent<NavMeshAgent>().speed = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            if (!_isBoss)
            {
                Destroy(gameObject);
            } else
            {
                _healthRemain--;
                this.transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
                if (_healthRemain == 0)
                {
                    Destroy(gameObject);
                }
            }
            
        } else if (collision.gameObject.CompareTag("MainCharacter"))
        {
            // TODO: Replace this to "REAL" GAMEOVER state
            Debug.Log("GAMEOVER");
        }
    }
}
