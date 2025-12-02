using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class pedestrian : MonoBehaviour
{
    public GameObject particulas;
    public SpriteRenderer sprite;
    public SpriteRenderer spriteShadow;


    private void Start()
    {
        particulas.SetActive(false);
    }


    void Update()
    {
        transform.LookAt(Camera.main.transform);

        // 2️⃣ Mantener el eje X fijo en 90 grados
        Vector3 euler = transform.eulerAngles;
        euler.x = 0f; // Fija el eje X
        transform.eulerAngles = euler;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider>().enabled = false;
            sprite.enabled = false;
            spriteShadow.enabled = false;   
            Debug.Log("hola");
            particulas.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
}
