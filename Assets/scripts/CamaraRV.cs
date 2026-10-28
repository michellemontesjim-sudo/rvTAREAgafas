using UnityEngine;

public class CamaraRV : MonoBehaviour
{
    [Header("Configuración de la Cámara")]
    public GameObject fotoPrefab;
    public Transform ranuraSalida;
    [Tooltip("La cámara de Unity que funcionará como lente")]
    public Camera camaraLente;

    [Header("Calidad de la Foto")]
    public int anchoFoto = 512;
    public int altoFoto = 512;

    public void TomarFoto()
    {
        if (fotoPrefab == null || ranuraSalida == null || camaraLente == null)
        {
            Debug.LogWarning("Faltan referencias en la cámara.");
            return;
        }

        // 1. Creamos un RenderTexture temporal para alojar la imagen
        RenderTexture rt = new RenderTexture(anchoFoto, altoFoto, 24);
        camaraLente.targetTexture = rt;

        // 2. Obligamos a la cámara a renderizar un solo frame
        camaraLente.Render();

        // 3. Leemos los píxeles capturados y los guardamos en una Textura2D definitiva
        RenderTexture.active = rt;
        Texture2D texturaCapturada = new Texture2D(anchoFoto, altoFoto, TextureFormat.RGB24, false);
        texturaCapturada.ReadPixels(new Rect(0, 0, anchoFoto, altoFoto), 0, 0);
        texturaCapturada.Apply();

        // 4. Limpiamos la memoria para evitar errores de saturación
        camaraLente.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // 5. Instanciamos el prefab físico de la foto
        GameObject nuevaFoto = Instantiate(fotoPrefab, ranuraSalida.position, ranuraSalida.rotation);

        // 6. Buscamos el script en la nueva foto y le pasamos la textura capturada
        FotoPolaroid scriptFoto = nuevaFoto.GetComponent<FotoPolaroid>();
        if (scriptFoto != null)
        {
            scriptFoto.AsignarImagen(texturaCapturada);
        }
    }
}