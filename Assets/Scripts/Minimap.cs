using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public Transform playerMarker;
    public Camera cam;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        playerMarker.transform.rotation = Quaternion.Euler(0f, 0f, -player.eulerAngles.y + 180);

        if (Input.GetKey(KeyCode.X))
        {
            cam.orthographicSize = 25;
        } 
        else if (Input.GetKey(KeyCode.C))
        {
            cam.orthographicSize = 100;
        }
        else
        {
            cam.orthographicSize = 50;
        }
    }
}