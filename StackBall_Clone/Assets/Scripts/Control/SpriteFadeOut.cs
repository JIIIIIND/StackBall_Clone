using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeOut : MonoBehaviour
{
    [SerializeField]
    private float _transparent;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.color = new Color(
            _renderer.color.r,
            _renderer.color.g,
            _renderer.color.b,
            _renderer.color.a - _transparent
            );
        if (_renderer.color.a <= 0)
            Destroy(this.gameObject);
    }
}
