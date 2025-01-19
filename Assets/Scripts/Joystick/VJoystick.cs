using UnityEngine;
using UnityEngine.EventSystems;

namespace Mindshift
{
    public enum JoystickType { Fixed, Floating, Dynamic }

    public class VJoystick : Joystick
    {
        [SerializeField] private float moveThreshold = 1f;
        [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

        private Vector2 fixedPosition = Vector2.zero;

        protected override void Start()
        {
            base.Start();
            fixedPosition = background.anchoredPosition;
            SetMode(joystickType);
        }

        public void SetMode(JoystickType joystickType)
        {
            this.joystickType = joystickType;
            if (joystickType == JoystickType.Fixed)
            {
                background.anchoredPosition = fixedPosition;
                background.gameObject.SetActive(true);
            }
            else
            {
                background.gameObject.SetActive(false);
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (joystickType != JoystickType.Fixed)
            {
                background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
                background.gameObject.SetActive(true);
            }
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (joystickType != JoystickType.Fixed)
            {
                background.gameObject.SetActive(false);
            }
            base.OnPointerUp(eventData);
        }

        protected override void HandleInput(float magnitude, Vector2 normalized, Vector2 radius, Camera cam)
        {
            if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
            {
                Vector2 difference = normalized * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;
            }

            // Call the base HandleInput to ensure normalization and clamping
            base.HandleInput(magnitude, normalized, radius, cam);
        }
    }
}
