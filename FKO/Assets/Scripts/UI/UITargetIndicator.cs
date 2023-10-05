using UnityEngine;

public class UITargetIndicator : MonoBehaviour
{
    public Transform target; // The off-screen target
    public Camera mainCamera;
    public RectTransform indicatorRectTransform; // Reference to the indicator's RectTransform

    void Update()
    {
        if (target == null || mainCamera == null) return;

        Vector3 targetPosition = target.position;
        Vector3 screenPos = mainCamera.WorldToScreenPoint(targetPosition);


        if (IsOffScreen(screenPos))
        {
            // Update indicator position
            Vector2 indicatorPosition = Vector2.zero;
            // Calculate and set indicatorPosition based on screenPos
            indicatorRectTransform.anchoredPosition = indicatorPosition;

            // Show the indicator
            indicatorRectTransform.gameObject.SetActive(true);
        }
        else
        {
            // Hide the indicator
            indicatorRectTransform.gameObject.SetActive(false);
        }
    }

    bool IsOffScreen(Vector3 screenPosition)
    {
        // Check if the screenPosition is off-screen
        // You may need to define your criteria for this check
        return screenPosition.x < 0 || screenPosition.x > Screen.width ||
               screenPosition.y < 0 || screenPosition.y > Screen.height;
    }
}
