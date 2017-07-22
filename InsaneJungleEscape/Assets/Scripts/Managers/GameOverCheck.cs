using UnityEngine;
using System.Collections;

public class GameOverCheck : MonoBehaviour {
	private void OnTriggerEnter (Collider other) 
	{
		// End the game if too many enemies not in the dying state get past the player
		if (other.tag == "Enemy") {
			MonkeyMovement  enemy = other.gameObject.GetComponent<MonkeyMovement>();
			if (!enemy.IsDying()) {
                GameMaster.Instance.MnksPassed++;
                Destroy(enemy.gameObject,0.1f);
                if (GameMaster.Instance.MnksPassed >= GameMaster.Instance.MissedMnksReqLoss)
                {
                    GameMaster.Instance.GameOver(false);
                    
                }
			}
		}
	}	

}
