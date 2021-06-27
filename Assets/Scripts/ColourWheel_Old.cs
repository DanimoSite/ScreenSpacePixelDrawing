using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteAlways]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(MeshFilter), typeof(MeshCollider), typeof(MeshRenderer))]
public class ColourWheel_Old : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField]
    private float wheelThickness = 10;

    [SerializeField]
    private float padding = 10f;

    private float wheelRadius = 50;

    private LineRenderer m_lineRenderer;

    private MeshCollider m_meshCollider;

    private MeshRenderer m_meshRenderer;

    private MeshFilter m_meshFilter;

    private Mesh m_bakedMesh;

    private bool isMouseOver = false;

    private void OnValidate()
    {
        DrawWheel();
    }

    private void OnRectTransformDimensionsChange()
    {
        DrawWheel();
    }

    private void Update()
    {
        
    }

    private void DrawWheel()
    {
        if (m_lineRenderer == null)
            m_lineRenderer = transform.GetComponent<LineRenderer>();

        wheelRadius = CalculateWheelRadius();

        var positions = new List<Vector3>();

        for (int i = 0; i < 360; i++)        
            positions.Add(new Vector3(wheelRadius * Mathf.Cos(Mathf.Deg2Rad * i), wheelRadius * Mathf.Sin(Mathf.Deg2Rad * i), 0f));

        m_lineRenderer.positionCount = positions.Count;
        m_lineRenderer.SetPositions(positions.ToArray());

        m_lineRenderer.startWidth = wheelThickness;
        m_lineRenderer.endWidth = wheelThickness;
        m_lineRenderer.loop = true;

        m_bakedMesh = new Mesh();
        m_lineRenderer.BakeMesh(m_bakedMesh, Camera.main, false);

        if (m_meshCollider == null)
            m_meshCollider = transform.GetComponent<MeshCollider>();

        m_meshCollider.sharedMesh = m_bakedMesh;
        m_meshCollider.convex = true;

        if (m_meshRenderer == null)
            m_meshRenderer = transform.GetComponent<MeshRenderer>();

        if (m_meshFilter == null)
            m_meshFilter = transform.GetComponent<MeshFilter>();

        m_meshFilter.sharedMesh = m_bakedMesh;
    }

    private float CalculateWheelRadius()
    {
        var rect = transform.GetComponent<RectTransform>().rect;
        var sizeDelta = new Vector2(rect.width, rect.height);
        var minDim = sizeDelta.x >= sizeDelta.y ? sizeDelta.y : sizeDelta.x;

        return (minDim - wheelThickness) / 2 - padding;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        Debug.Log(isMouseOver);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        Debug.Log(isMouseOver);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isMouseOver || eventData.button != PointerEventData.InputButton.Left)
            return;

        if (eventData.pointerCurrentRaycast.isValid)
            Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);

        //if (Physics.Raycast())
    }
}
