using System;
using System.Collections;
using Meta.XR.MRUtilityKit;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas hudWSCanvasPrefab;
    public int orbsNumber = 0;
    public int ghostsNumber = 0;
    private TextMeshProUGUI txtOrbsNumber;
    private TextMeshProUGUI txtGhostsNumber;
    public float normalOffset = 0.1f;
    private void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(BuildHUD);
    }

    private void BuildHUD()
    {
        StartCoroutine(BuildHUDCoroutine());
    }

    public IEnumerator BuildHUDCoroutine()
    {
        yield return new WaitForEndOfFrame();
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        Vector3 position = room.CeilingAnchor.GetAnchorCenter();
        Rect ceilingRect = room.CeilingAnchor.PlaneRect.Value;
        Vector2 bottomLeft = new Vector2(ceilingRect.xMin, ceilingRect.yMin);
        Vector2 bottomRight = new Vector2(ceilingRect.xMax, ceilingRect.yMin);
        Vector2 topRight = new Vector2(ceilingRect.xMax, ceilingRect.yMax);
        Vector2 topLeft = new Vector2(ceilingRect.xMin, ceilingRect.yMax);
        
        // Calculate two edges
        Vector3 edge1 = bottomRight - bottomLeft; // Edge from vertex1 to vertex2
        Vector3 edge2 = topLeft - bottomLeft; // Edge from vertex1 to vertex4

        // Calculate the normal vector (cross product)
        Vector3 normal = Vector3.Cross(edge1, edge2).normalized;
        
        Vector3 spawnPosition = position + normal * normalOffset;
        Canvas hudCanvas = Instantiate(hudWSCanvasPrefab, spawnPosition, Quaternion.identity);
        
        txtOrbsNumber = hudCanvas.transform.Find("TxtOrbs Number").GetComponent<TextMeshProUGUI>();
        txtGhostsNumber = hudCanvas.transform.Find("TxtGhosts Number").GetComponent<TextMeshProUGUI>();
        
        UpdateUI();
    }
    
    public void UpdateUI()
    {
        if (txtGhostsNumber != null && txtOrbsNumber != null)
        {
            txtOrbsNumber.text = orbsNumber.ToString();
            txtGhostsNumber.text = ghostsNumber.ToString();
        }
    }
}
