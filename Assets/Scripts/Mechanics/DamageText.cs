using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class DamageText : MonoBehaviour
{
    public TextMeshPro text;

    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _duration = 1f;

    private Vector2 _startLocalPosition;
    private float _timer;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        _startLocalPosition = transform.localPosition;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localPosition += new Vector3(-_speed * 0.5f, _speed, 0f) * Time.deltaTime;
        
        _timer -= Time.deltaTime;
        if (_timer <= 0f )
        {
            gameObject.SetActive(false);
        }
    }

    public void Display()
    {
        transform.localPosition = _startLocalPosition;

        _timer = _duration;
        gameObject.SetActive(true);
    }
}
