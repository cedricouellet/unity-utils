using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Helper methods and properties.
/// 
/// <para>
/// The main goal of these helper methods is to 
/// optimize garbage collection by caching whenever possible.
/// </para>
/// <para>
/// Others include quality-of-life features that are often needed in most projects.
/// </para>
/// </summary>
public static class Helpers
{
    /// <summary>
    /// The conserved instance of the current scene's main camera.
    /// </summary>
    private static Camera _camera;

    /// <summary>
    /// A cost-efficient way of retrieving the main camera in a scene (for garbage collection).
    /// <para>
    /// This is because instead of doing the Unity processing each time, 
    /// it caches the main camera of the scene the first time it is called.
    /// </para>
    /// </summary>
    public static Camera Camera
    {
        get 
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            return _camera;
        }
    }

    /// <summary>
    /// A dictionary that caches <see cref="WaitForSeconds"/>, in order to save performance (garbage collection).
    /// </summary>
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    /// <summary>
    /// A more cost-efficient method of creating a <see cref="WaitForSeconds"/> object (for the garbage collector).
    /// <para>
    /// It will cache the wait time as key and <c>WaitForSeconds</c> as value,
    /// in order to reuse already declared <c>WaitForSeconds</c>.
    /// </para>
    /// <para>
    /// If there is no <c>WaitForSeconds</c> associated to the key, 
    /// it will simply create it and store it for the next use.
    /// </para>
    /// </summary>
    /// <param name="time">The wait time</param>
    /// <returns>The <c>WaitForSeconds</c> associated to the wait time.</returns>
    public static WaitForSeconds GetWait(this float time)
    {
        if (WaitDictionary.TryGetValue(time, out WaitForSeconds waitTime))
        {
            return waitTime;
        }

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    /// <summary>
    /// If the pointer is hovering over a UI (canvas).
    /// <para>
    /// Useful when screen clicks trigger events, but we do not wish to trigger those events when
    /// clicking on a UI element.
    /// </para>
    /// </summary>
    /// <returns>True if the pointer is hovering over a UI (canvas), otherwise false.</returns>
    public static bool IsOverUI()
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();

        PointerEventData currentPonterEventData = new PointerEventData(EventSystem.current) 
        {
            position = Input.mousePosition 
        };

        EventSystem.current.RaycastAll(currentPonterEventData, raycastResults);

        return raycastResults.Count > 0;
    }

    /// <summary>
    /// Get the X/Y position of a canvas element in world space.
    /// 
    /// <para>
    /// Useful when interacting with objects that depend on the in-world position of canvas elements.
    /// </para>
    /// <para>
    /// Example: binding the position of a <see cref="GameObject"/> to that of a canvas element.
    /// </para>
    /// </summary>
    /// <param name="canvasElement">The canvas element for which to get in-world position.</param>
    /// <returns>The in-world position of the given canvas element.</returns>
    public static Vector2 GetWorldPositionOfCanvasElement(this RectTransform canvasElement)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasElement, canvasElement.position, Camera, out Vector3 worldPosition);
        return worldPosition;
    }

    /// <summary>
    /// Delete all children of a transform.
    /// </summary>
    /// <param name="transform">The transform for which to delete all children.</param>
    public static void DeleteChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
}