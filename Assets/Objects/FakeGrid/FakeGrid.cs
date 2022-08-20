using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGrid : MonoBehaviour
{
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
            GetComponent<Renderer>().material.mainTextureScale = new Vector2(_maxX, _x);
            //_x = Time.deltaTime * Time.deltaTime * (3.0f - 2.0f * Time.deltaTime); 
            if (_x <= 8f)
            {
                _x += 6.5f * Time.deltaTime;
            }
            else
            {
                _x = 8f;
                yield break;
            }

            yield return null;
        }
    }
}
