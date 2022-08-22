using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGrid : MonoBehaviour
{
    [SerializeField] private float _width;
    [SerializeField] private float _height;
    [SerializeField] private float _size;
    [SerializeField] private BattleTile _battleTilePrefab;
    [SerializeField] private Vector3 _offset;

    private Dictionary<Vector2, BattleTile> _tiles;

    public static BattleGrid Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid ()
    {
        _tiles = new Dictionary<Vector2, BattleTile>();
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
                Vector2 position = new Vector2(x, z);

                BattleTile battleTile = Instantiate(
                    _battleTilePrefab,
                    new Vector3(x * _size + _offset.x, _offset.y, z * _size + _offset.z),
                    Quaternion.identity
                );
                battleTile.tag = "Battletile";

                battleTile.transform.parent = this.transform;

                battleTile.SetPosition(position);

                _tiles[position] = battleTile;
            }
        }
    }

    public BattleTile GetTileAtPosition (Vector2 position)
    {
        if (_tiles.TryGetValue(position, out BattleTile tile))
        {
            return tile;
        }

        return null;
    }
}
