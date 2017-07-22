using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MonkeyMovement : MonoBehaviour {

#pragma warning disable 649
    [SerializeField]
	private float speedMin;
    [SerializeField]
	private float speedMax;
 
#pragma warning restore 649
    private enum EnemyState
    {
        Normal,
        Dying
    };
	
	private EnemyState state;

	// Use this for initialization.
	private void Start () {
		state = EnemyState.Normal;
		float ranSpeed = Random.Range(speedMin, speedMax);
		if (GvrViewer.Instance.VRModeEnabled) { 
 		 ranSpeed *= 0.85f; 
		}
        this.GetComponent<NavMeshAgent>().SetDestination(new Vector3(-8.38f, 0.76f, 0));
        this.GetComponent<NavMeshAgent>().speed = ranSpeed;
 
    }
	
	
	/// <summary>
	/// Check and see if our enemy is in a dying state. We need this because occasionally
	/// momentum drives a "dead" enemy through the end zone.
	/// </summary>
	/// <returns><c>true</c> if this enemy is dying; otherwise, <c>false</c>.</returns>
	public bool IsDying() {
		return (state == EnemyState.Dying);
	}
	
	
	/// <summary>
	/// Remove the game object after a short moment so we can watch it get knocked around.
	/// </summary>
	public void DieSoon() {
		if (state == EnemyState.Normal) {
			// Let's let the enemy get knocked back a bit.
			state = EnemyState.Dying;
            GameMaster.Instance.UpdateScore();
			this.GetComponent<AudioSource>().Play();
			Destroy(gameObject, 0.4f);
		}
	}

	
	
}

