using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _textValue;

    // Start is called before the first frame update
    void Start()
    {
        _slider.value = ConfigurationUtil.BulletBounce;

        _slider.onValueChanged.AddListener((v) =>
        {
            int value = (int)v;
            _textValue.text = value.ToString();
            ConfigurationUtil.BulletBounce = value;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
