using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGrid : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _speed = 0.8f;

    private float _x = 0f;
    private float _maxX = 8f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateGrid());
    }

    IEnumerator AnimateGrid()
    {
        while (true)
        {
            _x = Mathf.MoveTowards(_x, _maxX, _speed * Time.deltaTime);
            GetComponent<Renderer>().material.mainTextureScale = new Vector2(_maxX, _curve.Evaluate(_x) * _maxX);

            if (_x >= 8)
            {
                yield break;
            }

            yield return null;
        }
    }
}
