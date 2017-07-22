using UnityEngine;
using System.Collections;

public class CardboardSwapper : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private GameObject[] cardboardObjects;
    [SerializeField]
    private GameObject[] monoObjects;
#pragma warning restore 649

    // Turn on or off VR mode
    private void ActivateVRMode(bool goToVR)
    {
        foreach (GameObject cardboardObj in cardboardObjects)
        {
            cardboardObj.SetActive(goToVR);
        }
        foreach (GameObject monoThing in monoObjects)
        {
            monoThing.SetActive(!goToVR);
        }
        GvrViewer.Instance.VRModeEnabled = goToVR;


        // Tell UI to redisplay itself
        GameMaster.Instance.RefreshUI();

    }

    private void Switch()
    {
        ActivateVRMode(!GvrViewer.Instance.VRModeEnabled);
    }

    private void Update()
    {
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Switch();
        }
    }

    private void Start()
    {
        ActivateVRMode(false);

    }
}