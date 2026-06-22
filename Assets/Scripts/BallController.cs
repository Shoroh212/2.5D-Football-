using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [Header("Основные параметры")]
    [Tooltip("Сила отскока (чем выше — тем энергичнее отскакивает)")]
    [Range(0.5f, 2.5f)]
    public float bounceMultiplier = 1.1f;

    [Tooltip("Дополнительная сила при отскоке от игроков (для более живого ощущения)")]
    [Range(0f, 15f)]
    public float playerKickBoost = 8f;

    [Tooltip("Насколько сильно мяч теряет скорость при отскоке (1 = почти нет потерь)")]
    [Range(0.7f, 1f)]
    public float velocityConservation = 0.92f;

    [Header("Гравитация")]
    [Tooltip("Множитель гравитации для мяча (1 = стандартная гравитация Unity)")]
    [Range(0.2f, 2f)]
    public float gravityMultiplier = 1f;

    [Header("Визуал и размер")]
    [Tooltip("Размер мяча")]
    [Range(0.3f, 2f)]
    public float ballScale = 1f;

    [Tooltip("Добавить лёгкое вращение для живости")]
    public bool addSpin = true;

    private Rigidbody rb;
    private Vector3 lastVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate; // плавное движение
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // чтобы не пролезал сквозь объекты

        // Применяем размер
        transform.localScale = Vector3.one * ballScale;
    }

    void FixedUpdate()
    {
        // Применяем кастомную гравитацию
        if (rb.useGravity)
            rb.AddForce(Physics.gravity * (gravityMultiplier - 1f), ForceMode.Acceleration);

        lastVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Основная логика отскока
        if (collision.contacts.Length == 0) return;

        ContactPoint contact = collision.contacts[0];
        Vector3 normal = contact.normal;

        // Отражаем скорость
        Vector3 reflectedVelocity = Vector3.Reflect(lastVelocity, normal);

        // Применяем множители
        float speed = lastVelocity.magnitude;
        rb.linearVelocity = reflectedVelocity.normalized * speed * bounceMultiplier * velocityConservation;

        // Дополнительный импульс при отскоке от игроков
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 kickDirection = (reflectedVelocity + contact.normal * 0.5f).normalized;
            rb.AddForce(kickDirection * playerKickBoost, ForceMode.Impulse);
        }

        // Лёгкое вращение для "живого" ощущения
        if (addSpin && speed > 2f)
        {
            Vector3 torque = Vector3.Cross(normal, lastVelocity) * (speed * 0.8f);
            rb.AddTorque(torque, ForceMode.Impulse);
        }
    }

    // Метод для сброса мяча (удобно вызывать из других скриптов)
    public void ResetBall(Vector3 position, Vector3 velocity = default)
    {
        rb.linearVelocity = velocity;
        rb.angularVelocity = Vector3.zero;
        transform.position = position;
    }
}