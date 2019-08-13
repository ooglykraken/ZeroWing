using UnityEngine;
using System.Collections;

public class EnemyLaser : MonoBehaviour {
	
	private int lifeTime = 500;
	
	public Vector3 givenVelocity;
	
	public void Start(){
		givenVelocity = gameObject.GetComponent<Rigidbody>().velocity;
	}
	
	public void Update(){
		
		if(Gameplay.Instance().paused){
			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		} else {
			gameObject.GetComponent<Rigidbody>().velocity = givenVelocity;
			lifeTime--;
			if(lifeTime <= 0){
				Destroy(this.gameObject);
			}
		
		}
	}
	
	public void OnTriggerEnter(Collider c){
		// Debug.Log(c.gameObject.tag);
		if(c.transform.parent == null || c.transform.tag == "Laser"){
			return;
		}
		if(c.transform.parent.tag == "Player"){
			// c.transform.parent.gameObject.SetActive(false);
			Player.Instance().PlayerDamage();
			Destroy(this.gameObject);
			
			
		} 
	}
}
