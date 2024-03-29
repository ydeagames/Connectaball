﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingCamera : MonoBehaviour
{
    Camera cameraObj;
    public BoxCollider2D box;
    public float boundingBoxPadding = 2f;
    public float minimumOrthographicSize = 8f;
    public float zoomSpeed = 20f;

    void Awake()
    {
        cameraObj = GetComponent<Camera>();
        cameraObj.orthographic = true;
    }

    private void LateUpdate()
    {
        if (box != null)
        {
            var rect = new Rect(box.bounds.center - box.bounds.extents, box.bounds.size);
            transform.position = CalculateCameraPosition(rect);
            cameraObj.orthographicSize = CalculateOrthographicSize(rect);
        }
    }

    /// <summary>
    /// Calculates a camera position given the a bounding box containing all the targets.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A Vector3 in the center of the bounding box.</returns>
    Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, -10f);
    }

    /// <summary>
    /// Calculates a new orthographic size for the camera based on the target bounding box.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A float for the orthographic size.</returns>
    float CalculateOrthographicSize(Rect boundingBox)
    {
        float orthographicSize = cameraObj.orthographicSize;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width / 2, boundingBox.y + boundingBox.height / 2, 0f);
        Vector3 topRightAsViewport = cameraObj.WorldToViewportPoint(topRight);

        //Debug.Log($"xy:{cameraObj.aspect}, wh:{boundingBox.width / boundingBox.height}");
        if (cameraObj.aspect < boundingBox.width / boundingBox.height)
            orthographicSize = Mathf.Abs(boundingBox.width) / cameraObj.aspect / 2f;
        else
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

        return Mathf.Clamp(Mathf.Lerp(cameraObj.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
    }
}
