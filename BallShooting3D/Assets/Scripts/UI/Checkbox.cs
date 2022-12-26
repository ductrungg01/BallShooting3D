using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkbox : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] GameObject handle;
    [SerializeField] Sprite _checked;
    [SerializeField] Sprite _uncheck;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(OnClick);

        if (toggle.isOn)
            OnClick(true);
    }

    void OnClick(bool on)
    {
        handle.GetComponent<Image>().sprite = on ? _checked : _uncheck;
    }
}
