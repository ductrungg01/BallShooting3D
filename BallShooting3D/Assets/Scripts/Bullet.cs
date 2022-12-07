using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _bounce = 5;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySoundEffect("billiard_collision");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velo = GetComponent<Rigidbody>().velocity;
        //Debug.Log(velo);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.PlaySoundEffect("billiard_collision");

        if (collision.gameObject.CompareTag("Wall"))
        {
            this._bounce--;
            if (this._bounce < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetBound(int value)
    {
        this._bounce = value;
    }
}
