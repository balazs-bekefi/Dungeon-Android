using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;

    public void Call()
    {
        Generategrid();
    }

    void Generategrid()
    {
        var spawnedTile = Instantiate(_tilePrefab, new Vector3(3.744f, 0.163f, 0f), Quaternion.identity);
        spawnedTile.name = $"Tile";
    }
}
