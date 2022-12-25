using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        label = GetComponentInChildren<TextMeshPro>();
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
    }

    private void DisplayCoordinates()
    {
        if (label != null)
        {
            coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
            coordinates.y = Mathf.RoundToInt(transform.position.z / UnityEditor.EditorSnapSettings.move.z);

            label.text = $"{coordinates.x},{coordinates.y}";
        }
    }

    private void UpdateObjectName()
    {
        gameObject.name = coordinates.ToString();
    }
    
}
