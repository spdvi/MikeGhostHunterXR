using System;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class OrbsSpawner : MonoBehaviour
{
    public int numerOfOrbsToSpawn = 5;
    public GameObject orbPrefab;
    public float height = 0.3f;

    public MRUKAnchor.SceneLabels labelFlags;
    
    public List<GameObject> spawnedOrbs;

    public int maxNumberOfTry = 100;
    public int currentNumberOfTry = 0;
    
    public GameManager gameManager;
    
    public static OrbsSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);    
    }

    private void SpawnOrbs()
    {
        for (int i = 0; i < numerOfOrbsToSpawn; i++)
        {
            Vector3 randomPosition = Vector3.zero;
            MRUKRoom room = MRUK.Instance.GetCurrentRoom();
            while (currentNumberOfTry < maxNumberOfTry)
            {
                bool hasFound = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP,
                    0.1f, new LabelFilter(labelFlags),
                    out randomPosition, out Vector3 n);
                if (hasFound) break;
                currentNumberOfTry++;
            }
            currentNumberOfTry = 0;
            randomPosition.y = height;
            GameObject spawnedOrb = Instantiate(orbPrefab, randomPosition, Quaternion.identity);
            spawnedOrbs.Add(spawnedOrb);
        }
        gameManager.orbsNumber = spawnedOrbs.Count;
        gameManager.UpdateUI();
    }

    public void DestroyOrb(GameObject orb)
    {
        spawnedOrbs.Remove(orb);
        Destroy(orb);
        gameManager.orbsNumber = spawnedOrbs.Count;
        gameManager.UpdateUI();
    }
    
}
