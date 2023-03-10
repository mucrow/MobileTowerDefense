using System;
using Mtd.Utils;
using UnityEngine;

namespace Mtd.UI {
  [RequireComponent(typeof(RectTransform))]
  public class ShowHideOffscreen: MonoBehaviour {
    [SerializeField] Direction _edge;

    RectTransform _rectTransform;
    Vector2 _initialPosition;
    Vector2 _size = new Vector2(16, 16);

    public bool IsHidden { get; private set; }

    void Awake() {
      _rectTransform = GetComponent<RectTransform>();
    }

    void Start() {
      // trying to check initial position and width in Awake() result in inaccurate values. i
      // believe this is because Unity UI components are not guaranteed to be finished resizing
      // when Awake() is called. it makes sense - the layout needs to be dynamically calculated and
      // it has to happen somewhere. probably best that these objects don't get special treatment
      // just for being part of Unity's main library.
      _initialPosition = _rectTransform.anchoredPosition;
      _size = _rectTransform.rect.size;
      Hide();
    }

    public void Show() {
      _rectTransform.anchoredPosition = _initialPosition;
      IsHidden = false;
    }

    public void Hide() {
      var direction = _edge.ToVector2();
      var relevantDimension = GetRelevantDimension();
      _rectTransform.anchoredPosition = _initialPosition + direction * relevantDimension;
      IsHidden = true;
    }

    public float GetRelevantDimension() {
      if (_edge == Direction.Left || _edge == Direction.Right) {
        return _size.x;
      }
      return _size.y;
    }
  }
}
