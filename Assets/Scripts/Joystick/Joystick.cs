using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindshift
{
    public enum AxisOptions { Both, Horizontal, Vertical }

    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Header("Joystick Settings")]
        [SerializeField] private float handleRange = 1f;
        [SerializeField] private float deadZone = 0.1f;
        [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;

        [Header("Joystick Components")]
        [SerializeField] protected RectTransform background;
        [SerializeField] protected RectTransform handle;

        protected Vector2 input = Vector2.zero;
        protected Camera cam;
        protected RectTransform baseRect;

        public float Vertical => axisOptions == AxisOptions.Vertical ? 0f : input.x;
        public float Horizontal => axisOptions == AxisOptions.Horizontal ? 0f : input.y;
        public Vector2 Direction => new Vector2(Horizontal, Vertical);

        protected virtual void Start()
        {
            baseRect = GetComponent<RectTransform>();
            cam = GetComponentInParent<Canvas>().worldCamera;
            handle.anchoredPosition = Vector2.zero;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
            Vector2 radius = background.sizeDelta / 2;

            Vector2 rawInput = (eventData.position - position) / (radius * CanvasScaleFactor());
            rawInput = Vector2.ClampMagnitude(rawInput, 1f);

            // Apply axis restriction
            if (axisOptions == AxisOptions.Horizontal) rawInput.y = 0f;
            if (axisOptions == AxisOptions.Vertical) rawInput.x = 0f;

            input = rawInput.magnitude > deadZone ? rawInput : Vector2.zero;

            handle.anchoredPosition = input * radius * handleRange;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }

        private float CanvasScaleFactor()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            return canvas.scaleFactor != 0 ? canvas.scaleFactor : 1f;
        }
        protected virtual void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                input = (magnitude > 1) ? normalized : input;
            }
            else
            {
                input = Vector2.zero; // Ignore minor input
            }
        }
        /// <summary>
        /// Converts a screen point to an anchored position relative to the joystick.
        /// </summary>
        protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            Vector2 localPoint = Vector2.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    baseRect,
                    screenPosition,
                    cam,
                    out localPoint))
            {
                Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
                return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
            }
            return Vector2.zero;
        }

    }
}
