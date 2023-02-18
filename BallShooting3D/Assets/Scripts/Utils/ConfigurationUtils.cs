using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ConfigurationUtil
{
    private static float _bulletSpeed = 10.0f;
    private static int _bulletBounce = 5;

    public static float BulletSpeed
    {
        get { return _bulletSpeed; }
        set { _bulletSpeed = value; }
    }

    public static int BulletBounce
    {
        get { return _bulletBounce; }
        set { _bulletBounce = value; }
    }
}
