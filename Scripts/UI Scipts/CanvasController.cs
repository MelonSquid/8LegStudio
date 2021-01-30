using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> webMarkers;
    [SerializeField]
    private PlayerController player;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WebUsed()
    {
        int webRemaining = Mathf.Min(player.GetWebShotRemaining(), webMarkers.Count - 1);
        webMarkers[webRemaining].SetActive(false);
    }

    public void WebRefilled()
    {
        for (int i = 0; i < webMarkers.Count; i++)
        {
            webMarkers[i].SetActive(true);
        }
    }
}
