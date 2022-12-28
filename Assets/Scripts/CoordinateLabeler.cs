using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    Waypoint waypoint;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        label = GetComponentInChildren<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates();
    }

    void Start()
    {
        waypoint = gameObject.GetComponent<Waypoint>();
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
            ColorCoordinates();
        }

        ToggleLabels();
    }

    private void ColorCoordinates()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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

    private void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    

    private void UpdateObjectName()
    {
        gameObject.name = coordinates.ToString();
    }
    
}
