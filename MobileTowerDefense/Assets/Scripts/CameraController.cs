using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Mtd {
  [RequireComponent(typeof(Camera))]
  public class CameraController: MonoBehaviour {
    [SerializeField] float _closestZoomSize = 2.5f;
    [SerializeField] float _farthestZoomSize = 10f;
    [SerializeField] float _zoomLevelCurve = 1.5f;

    float _zoomLevel;

    Camera _camera;

    void Awake() {
      _camera = GetComponent<Camera>();
      var zoomSizeRange = _farthestZoomSize - _closestZoomSize;
      var pointInZoomSizeRange = (_camera.orthographicSize - _closestZoomSize) / zoomSizeRange;
      var clampedPointInZoomSizeRange = Mathf.Clamp01(pointInZoomSizeRange);

      var zoomLevel = 1f - Mathf.Pow(clampedPointInZoomSizeRange, 1f / _zoomLevelCurve);
      SetZoomLevel(zoomLevel);
    }

    public void ChangeZoomLevel(float amount) {
      SetZoomLevel(_zoomLevel + amount);
    }

    public void SetZoomLevel(float zoomLevel) {
      _zoomLevel = Mathf.Clamp01(zoomLevel);
      UpdateZoomSize();
    }

    void UpdateZoomSize() {
      _camera.orthographicSize = Utils.Utils.MapRange(
        0f,
        1f,
        _closestZoomSize,
        _farthestZoomSize,
        1f - _zoomLevel,
        _zoomLevelCurve
      );
    }
  }
}
