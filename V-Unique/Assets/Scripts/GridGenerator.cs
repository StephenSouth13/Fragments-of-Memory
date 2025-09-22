using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject spherePrefab;

    [Header("Settings")]
    [SerializeField] private float gridSize; // Đổi từ GameObject sang float

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector3 startPosition = Vector2.left * gridSize / 2 + Vector2.down * gridSize / 2;
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                Vector3 spawnPosition = startPosition + Vector3.right * x + Vector3.up * y;

                Instantiate(spherePrefab, spawnPosition, Quaternion.identity, transform);
            }
          
        }
      
    }
}