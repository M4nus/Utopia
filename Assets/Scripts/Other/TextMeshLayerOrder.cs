using UnityEngine;
using System.Collections;

public class TextMeshLayerOrder : MonoBehaviour
{
    public string layerToPushTo;

    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = layerToPushTo;    
    }
}
