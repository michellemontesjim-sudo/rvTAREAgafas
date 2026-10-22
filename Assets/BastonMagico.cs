using UnityEngine;
public class BastonMagico:MonoBehaviour
{
    [Header("Configuración del Hechizo")]
    [Tooltip("El prefab de la partícula o bola de magia a disparar")]
    public GameObject hechizoPrefab;

    [Tooltip("El lugar exacto desde donde sale la magia (ej. la punta del bastón)")]
    public Transform puntoDeDisparo;

    [Tooltip("La fuerza con la que sale disparado el hechizo")]
    public float fuerzaDisparo = 20f;

    // Esta es la función que conectaste al evento "Activated"
    public void DispararMagia()
    {
        // 1. Verificamos que el Inspector esté bien configurado
        if (hechizoPrefab == null || puntoDeDisparo == null)
        {
            Debug.LogWarning("Falta asignar el prefab del hechizo o el punto de disparo en el script.");
            return;
        }

        // 2. Instanciamos el hechizo en la posición y rotación de la punta del bastón
        GameObject nuevoHechizo = Instantiate(hechizoPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);

        // 3. Aplicamos fuerza física para que salga volando hacia adelante
        Rigidbody rb = nuevoHechizo.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Impulsa el objeto hacia donde esté apuntando el punto de disparo
            rb.AddForce(puntoDeDisparo.forward * fuerzaDisparo, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("El prefab del hechizo no tiene un componente Rigidbody. No se moverá físicamente.");
        }

        // 4. Limpieza: Destruimos el hechizo después de 3 segundos para liberar memoria
        Destroy(nuevoHechizo, 3f);
    }
}
