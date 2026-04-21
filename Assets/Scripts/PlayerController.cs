using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 10f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Camera mainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        mainCamera = Camera.main;

        // Calculamos los límites de la pantalla en coordenadas del mundo
        CalculateBounds();
    }

    void CalculateBounds()
    {
        // Obtiene la altura y anchura que la cámara alcanza a ver
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        screenBounds = new Vector2(camWidth, camHeight);

        // Obtenemos el tamaño del sprite para que la nave no se salga a la mitad
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            objectWidth = sprite.bounds.extents.x; // Mitad del ancho
            objectHeight = sprite.bounds.extents.y; // Mitad del alto
        }
    }

    void Update()
    {
        // Lectura del Input System (como configuramos antes)
        Vector2 keyboardInput = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) keyboardInput.y = 1;
        if (Keyboard.current.sKey.isPressed) keyboardInput.y = -1;
        if (Keyboard.current.aKey.isPressed) keyboardInput.x = -1;
        if (Keyboard.current.dKey.isPressed) keyboardInput.x = 1;

        moveInput = keyboardInput.normalized;
    }

    void FixedUpdate()
    {
        // 1. Aplicamos el movimiento físico
        rb.linearVelocity = moveInput * moveSpeed;

        // 2. Restringimos la posición (Clamp) para que no salga de la cámara
        // Sumamos y restamos objectWidth/Height para que el borde sea la punta de la nave
        float clampedX = Mathf.Clamp(rb.position.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        float clampedY = Mathf.Clamp(rb.position.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        rb.position = new Vector2(clampedX, clampedY);
    }
}