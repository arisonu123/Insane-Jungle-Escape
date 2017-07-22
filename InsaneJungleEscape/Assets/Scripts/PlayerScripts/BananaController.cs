using UnityEngine;
using System.Collections;

public class BananaController : MonoBehaviour {

	float bananaSpeed = 9.0f;
	float rotationSpeed = 8.0f;
	
	void Start () {
		this.GetComponent<Rigidbody>().velocity = this.transform.up * bananaSpeed;
		this.GetComponent<Rigidbody>().angularVelocity = this.transform.forward * rotationSpeed;
    }
	
	void OnCollisionEnter (Collision collision) {
		if (collision.collider.tag == "Enemy") {
            collision.collider.gameObject.GetComponent<MonkeyHealth>().HP -= 1;
            if (collision.collider.gameObject.GetComponent<MonkeyHealth>().HP <= 0)
            {
                collision.collider.GetComponent<MonkeyMovement>().DieSoon();
            }
            Destroy(this.gameObject);
		}
	}
	
}
