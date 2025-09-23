using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject spherePrefab;

    [Header("Settings")]
    [SerializeField] private float gridSize;
    [SerializeField] private float gridScale;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector3 startPosition = Vector2.left * (gridScale * gridSize / 2) + Vector2.down * (gridScale * gridSize / 2);
        startPosition.x += gridScale / 2;
        startPosition.y += gridScale / 2;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                Vector3 spawnPosition = startPosition + new Vector3(x, y, 0) * gridScale;
                GameObject sphereInstance = Instantiate(spherePrefab, spawnPosition, Quaternion.identity, transform);

                sphereInstance.transform.localScale = Vector3.one * gridScale;
            }
        }
    }
}