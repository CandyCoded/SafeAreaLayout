using UnityEngine;

namespace CandyCoded
{

    [RequireComponent(typeof(Canvas))]
    public class SafeAreaLayout : MonoBehaviour
    {

        private Canvas _canvas;

        private Rect _lastSafeArea;

        private RectTransform _wrapperTransform;

        private void Awake()
        {

            _canvas = gameObject.GetComponent<Canvas>();

        }

        private void Update()
        {

            if (_canvas != null && _canvas.enabled && _wrapperTransform == null)
            {
                CreateLayoutWrapper();
            }

            if (_lastSafeArea.Equals(Screen.safeArea))
            {
                return;
            }

            UpdateSafeArea();

        }

        private void CreateLayoutWrapper()
        {

            if (_wrapperTransform != null)
            {
                return;
            }

            _wrapperTransform =
                new GameObject("SafeAreaLayout (Spawned)", typeof(RectTransform)).transform as RectTransform;

            if (_wrapperTransform == null)
            {
                return;
            }

            for (var i = 0; i < gameObject.transform.childCount; i += 1)
            {
                var child = gameObject.transform.GetChild(i);

                child.transform.SetParent(_wrapperTransform, false);
            }

            _wrapperTransform.SetParent(gameObject.transform);

            _wrapperTransform.offsetMin = Vector2.zero;
            _wrapperTransform.offsetMax = Vector2.zero;

            UpdateSafeArea();

        }

        private void UpdateSafeArea()
        {

            if (_wrapperTransform == null)
            {
                return;
            }

            var safeArea = Screen.safeArea;

            var canvasPixelSize = _canvas.pixelRect.size;

            _wrapperTransform.anchorMin = safeArea.position / canvasPixelSize;
            _wrapperTransform.anchorMax = (safeArea.position + safeArea.size) / canvasPixelSize;

            _lastSafeArea = safeArea;

        }

    }

}
