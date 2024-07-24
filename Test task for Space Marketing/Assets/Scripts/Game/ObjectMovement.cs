using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private float _destroyZonePosZ = -10f;

    private void Update()
    {
        transform.Translate(Vector3.back * GameManager.GetCurrentDifficulty() * Time.deltaTime, Space.World);

        if (transform.position.z <= _destroyZonePosZ)
        {
            Destroy(gameObject);
        }
    }
}
