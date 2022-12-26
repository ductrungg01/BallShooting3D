using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _textValue;
    [SerializeField] string type;

    // Start is called before the first frame update
    void Start()
    {
        if (type == "bounce")
        {
            _slider.value = ConfigurationUtil.BulletBounce;
            _textValue.text = ConfigurationUtil.BulletBounce.ToString();
        } else if (type == "speed")
        {
            _slider.value = ConfigurationUtil.BulletSpeed;
            _textValue.text = ConfigurationUtil.BulletSpeed.ToString();
        }

        _slider.onValueChanged.AddListener((v) =>
        {
            int value = (int)v;
            _textValue.text = value.ToString();

            if (type == "bounce")
                ConfigurationUtil.BulletBounce = value;
            else if (type == "speed")
                ConfigurationUtil.BulletSpeed = value;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
