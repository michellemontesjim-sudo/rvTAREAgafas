using UnityEngine;

public class CaminarYDesaparecer : MonoBehaviour
{
    [Tooltip("El punto hacia donde caminará (detrás de la pared)")]
    public Transform puntoDestino;
    [Tooltip("Velocidad al caminar")]
    public float velocidad = 2f;

    void Update()
    {
        if (puntoDestino == null) return;

        // 1. Mueve el objeto hacia el punto de destino
        transform.position = Vector3.MoveTowards(transform.position, puntoDestino.position, velocidad * Time.deltaTime);

        // 2. Lo hace rotar para que siempre mire hacia donde camina
        transform.LookAt(puntoDestino);

        // 3. Calcula la distancia restante. Si está muy cerca del destino, desaparece.
        if (Vector3.Distance(transform.position, puntoDestino.position) < 0.2f)
        {
            gameObject.SetActive(false); // Lo apaga
            // Si prefieres borrarlo por completo de la memoria RAM, usa: Destroy(gameObject);
        }
    }
}