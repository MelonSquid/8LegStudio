using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> webMarkers;
    [SerializeField]
    private Text frendsFoundText;
    [SerializeField]
    private Text friendsTotalText;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int friendCount;

    private int friendsFound;

    void Start()
    {
        friendsFound = 0;
        friendsTotalText.text = friendCount.ToString();
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
    
    public void FriendFound()
    {
        friendsFound++;
        frendsFoundText.text = friendsFound.ToString();
    }

}
