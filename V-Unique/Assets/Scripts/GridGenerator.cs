using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject spherePrefab;
    [Header("Settings")]
    [SerializeField] private GameObject gridSize;
   

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
       Vector3 startPosition = Vertor2.left *(float)gridSize/2  + Vector2.down * (float)gridSize/2;

        Instantiate(spherePrefab, startPosition, Quaternion.identity,transform );
    }
}