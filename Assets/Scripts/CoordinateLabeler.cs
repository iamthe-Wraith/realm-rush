using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.2f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        if (label != null)
        {
            label.enabled = false;
        }

        DisplayCoordinates();
    }

    void Update()
    {
        // only allow application to execute in edit mode
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        else
        {
            SetLabelColor();
        }

        ToggleLabels();
    }

    private void SetLabelColor()
    {
        if (gridManager == null | label == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    private void DisplayCoordinates()
    {
        if (label != null && gridManager != null)
        {
            coordinates.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
            coordinates.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);

            label.text = $"{coordinates.x},{coordinates.y}";
        }
    }

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C) && label != null)
        {
            label.enabled = !label.IsActive();
        }
    }

    private void UpdateObjectName()
    {
        gameObject.name = coordinates.ToString();
    }
    
}
