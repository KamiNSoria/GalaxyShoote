using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Calculamos el desplazamiento actual respecto al origen
        float currentDisplacement = transform.position.x - startPosition.x;

        // 2. Comprobamos si nos pasamos del límite
        if (Mathf.Abs(currentDisplacement) >= moveDistance)
        {
            // Cambiamos dirección
            direction *= -1;

            // "Corregimos" la posición para que no se desfase con el tiempo
            float correctedX = startPosition.x + (Mathf.Sign(currentDisplacement) * moveDistance);
            transform.position = new Vector3(correctedX, transform.position.y, transform.position.z);
        }

        // 3. Aplicamos el movimiento
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }
}