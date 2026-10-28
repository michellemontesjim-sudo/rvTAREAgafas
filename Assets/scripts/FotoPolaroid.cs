using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FotoPolaroid : MonoBehaviour
{
    [Header("Configuración del Revelado")]
    public MeshRenderer planoImagen;
    public float fuerzaSacudida = 1.5f;
    public float velocidadRevelado = 0.5f;

    private float nivelRevelado = 0f;
    private Vector3 posicionAnterior;
    private XRGrabInteractable agarre;
    private Material materialFoto;

    void Start()
    {
        posicionAnterior = transform.position;
        agarre = GetComponent<XRGrabInteractable>();

        if (planoImagen != null)
        {
            materialFoto = planoImagen.material;
            // URP usa _BaseColor para el tinte
            materialFoto.SetColor("_BaseColor", Color.black);
        }
    }

    void Update()
    {
        if (nivelRevelado >= 1f) return;

        if (agarre != null && agarre.isSelected)
        {
            float velocidad = (transform.position - posicionAnterior).magnitude / Time.deltaTime;

            if (velocidad > fuerzaSacudida)
            {
                nivelRevelado += velocidadRevelado * Time.deltaTime;
                nivelRevelado = Mathf.Clamp01(nivelRevelado);

                if (materialFoto != null)
                {
                    // Transición en URP
                    Color colorActual = Color.Lerp(Color.black, Color.white, nivelRevelado);
                    materialFoto.SetColor("_BaseColor", colorActual);
                }
            }
        }

        posicionAnterior = transform.position;
    }

    public void AsignarImagen(Texture2D texturaNueva)
    {
        if (planoImagen != null)
        {
            if (materialFoto == null) materialFoto = planoImagen.material;

            // URP usa _BaseMap para la textura principal
            materialFoto.SetTexture("_BaseMap", texturaNueva);
        }
    }
}