using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private Rigidbody _rb;
    private float _movementSpeed = 500f;
    private float _deadZonePosX = 2f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (GameManager.isGameOver || GameManager.isGamePaused) return;
        ControlPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Coin":
                _gameManager.AddCoinScore();
                Destroy(other.gameObject);
                break;
            case "Obstacle":
                GameManager.isGameOver = true;
                break;
            default:
                break;
        }
    }

    private void ControlPlayer()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            
            if (currentMousePos.x > _deadZonePosX)
            {
                _rb.velocity = Vector3.right * _movementSpeed * Time.deltaTime;
            } 
            else if (currentMousePos.x < -_deadZonePosX)
            {
                _rb.velocity = Vector3.left * _movementSpeed * Time.deltaTime;
            }
        } 
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }
}
