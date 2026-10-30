using UnityEngine;

public class AparicionTrigger : MonoBehaviour
{
    [Tooltip("El modelo 3D del ente/monstruo que va a aparecer")]
    public GameObject enteAparicion;

    private bool yaActivado = false;

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró es el jugador y si no se ha activado antes
        if (other.CompareTag("Player") && !yaActivado)
        {
            enteAparicion.SetActive(true);
            yaActivado = true;
        }
    }
}