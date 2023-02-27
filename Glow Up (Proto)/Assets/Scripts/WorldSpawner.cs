using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : Singleton<WorldSpawner>
{
    public GameObject levelPrefab;
    public Transform background;

    public float spawnPositionYOffset = 50;

    private GameObject lastSpawnedLevel;

    private void Start()
    {
        GameManager.instance.GameStart();
        lastSpawnedLevel = FindObjectOfType<GlowBox>().transform.parent.gameObject;
        GameManager.instance.ChangeCameraTarget(lastSpawnedLevel.transform);
    }
    public void InstantiateLevel()
    {
        lastSpawnedLevel = Instantiate(levelPrefab, new Vector3(lastSpawnedLevel.transform.position.x, lastSpawnedLevel.transform.position.y + spawnPositionYOffset, 0f), Quaternion.identity);
        GameManager.instance.currentGlowBox = lastSpawnedLevel.GetComponentInChildren<GlowBox>();

        GameManager.instance.ChangeCameraTarget(lastSpawnedLevel.transform);
        background.position = lastSpawnedLevel.transform.position;
    }
}
