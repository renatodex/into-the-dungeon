using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlegrid : MonoBehaviour
{
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    [SerializeField] private float _size;
    [SerializeField] private Battletile _battleTilePrefab;
    [SerializeField] private Vector3 _offset;

    private Dictionary<Vector2, Battletile> _tiles;

    // Update is called once per frame
    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid ()
    {
        GameObject[] battleTiles = GameObject.FindGameObjectsWithTag("Battletile");
        for(int i = 0; i < battleTiles.Length; i++)
        {
            if (Application.isPlaying)
            {
                Destroy(battleTiles[i]);
            } else
            {
                DestroyImmediate(battleTiles[i]);
            }
        }   

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                Battletile battleTile = Instantiate(
                    _battleTilePrefab,
                    new Vector3(x * _size + _offset.x, 0f + _offset.y, z * _size + _offset.z),
                    Quaternion.identity
                );
                battleTile.tag = "Battletile";

                battleTile.transform.parent = this.transform;
            }
        }
    }
}
