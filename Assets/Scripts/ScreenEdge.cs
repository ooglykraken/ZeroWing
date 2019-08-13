using UnityEngine;
using System.Collections;

public class ScreenEdge : MonoBehaviour {

	public void OnTriggerEnter(Collider c){
		
		Transform cTransform = c.transform;
		
		if(cTransform.parent == null){
			return;
		}
		
		Transform cParent = cTransform.parent;
		
		// Debug.Log(cParent.tag);
		
		if(cParent.tag == "Player"){
			if(cTransform.tag == "Laser"){
				// Debug.Log("Laser!");
				Destroy(c.gameObject);
			} else {
				if(gameObject.name == "LeftEdge"){
					Player.Instance().transform.position = new Vector3(transform.position.x + 2.711f, Player.Instance().transform.position.y, Player.Instance().transform.position.z);
				} else {
					Player.Instance().transform.position = new Vector3(transform.position.x - 2.711f, Player.Instance().transform.position.y, Player.Instance().transform.position.z);
				}
			}
		}
		
		// if(cParent.tag == "Enemy"){
			// Debug.Log("new enemy firing");
			// cParent.gameObject.GetComponent<Enemy>().canFire = !cParent.gameObject.GetComponent<Enemy>().canFire;
		// }
	}
	
	public void OnCollisionEnter(Collision c){
		Debug.Log("OnCollider works");
		if(c.transform.tag == "Laser" && c.transform.parent.tag == "Enemy"){
			Debug.Log("Laser!");
			Destroy(c.gameObject);
		}
		
	}
}
