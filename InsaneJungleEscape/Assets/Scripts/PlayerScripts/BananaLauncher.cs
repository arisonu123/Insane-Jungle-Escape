using UnityEngine;
using System.Collections;

public class BananaLauncher : MonoBehaviour {
#pragma warning disable 649
    [SerializeField]
	private GameObject player;
    [SerializeField]
	private BananaController bananna;
    [SerializeField]
	private AudioSource whooshSound;
#pragma warning restore 649

    private Vector3 shooterOffset;
	private Vector3 vrShooterOffset;

	private void Start () {
		shooterOffset = new Vector3(0.0f, 0.8f, 1.0f);
		vrShooterOffset = new Vector3(0.0f, -0.4f, 1.0f);

	}



    private void Update()
    {
        if (GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered && !GameMaster.Instance.getIsGameOver&&GameMaster.Instance.didFirstGameStart)
        {
            GameObject vrLauncher =
                     GvrViewer.Instance.GetComponentInChildren<GvrHead>().gameObject;
            // 2
            LaunchBananaFrom(vrLauncher, vrShooterOffset);
        }
        else if (!GvrViewer.Instance.VRModeEnabled && Input.GetButtonDown("Fire1") && !GameMaster.Instance.getIsGameOver && GameMaster.Instance.didFirstGameStart)
        {
            // This is the same code as before
            Vector3 mouseLoc = Input.mousePosition;
            Vector3 worldMouseLoc = Camera.main.ScreenToWorldPoint(mouseLoc);
            worldMouseLoc.y = player.transform.position.y;
            player.transform.LookAt(worldMouseLoc);
            LaunchBananaFrom(player, shooterOffset);
        }
    }

	private void LaunchBananaFrom(GameObject origin, Vector3 shooterOffset) {
		
		// This will toss a banana slightly in front of the origin object.
		// We also have to rotate our model 90 degrees in the x-coordinate.
		Vector3 bananaRotation = origin.transform.rotation.eulerAngles;
		bananaRotation.x = 90.0f;
		Vector3 transformedOffset = origin.transform.rotation * shooterOffset;
		Instantiate(bananna, origin.transform.position + transformedOffset, Quaternion.Euler(bananaRotation));
		
		// Play a sound effect!
		whooshSound.Play();
		
	}
	
}
