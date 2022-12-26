using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Threading.Tasks;
using TMPro;

public class Boost : MonoBehaviour
{
    [SerializeField]
    private int _boostValue = 20;
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    private int _boostRemain = 0;

    Random random = new Random();

    [SerializeField]
    private GameObject _bullet;

    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro.text = _boostValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (_boostRemain > 0)
        {
            _boostRemain--;
            SpawnBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            SpawnBullet();
            Destroy(gameObject);
        }
    }

    float GetRandomNumber(float minimum = -1, float maximum = 1) 
    {
        return (float)(random.NextDouble() * (maximum - minimum) + minimum);
    }
   
    Vector3 GetRandomDir()
    {
        float x = GetRandomNumber(-1, 1);
        float z = GetRandomNumber(-1, 1);
        return new Vector3(x, 0, z);
    }

    async void SpawnBullet()
    {
        float bulletSpeed = 15f;
        Vector3 position = this.gameObject.transform.position;

        for (int i = 0; i < _boostValue; i++)
        {
            GameObject spawnedBullet = Instantiate(_bullet, position, Quaternion.identity);
            Vector3 bulletDir = GetRandomDir();

            spawnedBullet.GetComponent<Rigidbody>().velocity = bulletDir * bulletSpeed;
            spawnedBullet.GetComponent<Bullet>().SetBound(0);

            await Task.Delay(50);
        }
        
    }
}
