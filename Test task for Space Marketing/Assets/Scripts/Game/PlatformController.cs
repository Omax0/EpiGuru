using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private Vector3 _platformStartPos = new Vector3(0f, 0f, 140f);
    private Vector3 _platformEndPos = new Vector3(0f, 0f, 0f);

    private float _platformSpeed = 20f;

    private void Update()
    {
        if (transform.position.z <= _platformEndPos.z)
        {
            transform.position = _platformStartPos;
        }

        transform.Translate(_platformSpeed * Time.deltaTime * Vector3.back);
    }
}
