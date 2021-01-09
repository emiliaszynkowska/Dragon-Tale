using UnityEngine;

public class MinimapMarker : MonoBehaviour
{
    public Transform player;
    public float diff;

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, -player.eulerAngles.y);
    }
}