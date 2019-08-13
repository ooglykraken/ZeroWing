using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private float movementSpeed = 7.5f;
	
	private float spaceFromSide = 1/8;
	
	public Transform rightBound;
	public Transform leftBound;
	public Transform topBound;
	public Transform botBound;
	
	private Rigidbody rigidbody;
	
	public void Awake(){
		// rightBound = 1;
		// leftBound = 0;
		// topBound = 1;
		// botBound = 0;
		
		rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	public void LateUpdate(){
		Move();
	}
	
	private void Move(){
		if(Player.Instance() == null)
			return; 
			
		// transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
		Vector3 playerPosition = Player.Instance().transform.position;
		
		Vector2 playerPosition2d = new Vector2(playerPosition.x, playerPosition.y);
		
		Vector2 playerScreenPosition = Camera.main.WorldToViewportPoint(playerPosition);
		
		Vector2 thisPosition = new Vector2(transform.position.x, transform.position.y);
		Vector2 rightBoundPos = new Vector2(rightBound.position.x, rightBound.position.y);
		Vector2 leftBoundPos = new Vector2(leftBound.position.x, leftBound.position.y);
		Vector2 topBoundPos = new Vector2(topBound.position.x, topBound.position.y);
		Vector2 botBoundPos = new Vector2(botBound.position.x, botBound.position.y);
		
		float distanceFromEdge = 9f;
		
		
		float verticalAdjustment = 1f;
		float verticalModifier = 0f;
		
		float horizontalAdjustment = 1f;
		float horizontalModifier = 0f;
		
		
		// Vector3 lerpedCameraPosition = Vector3.Lerp(transform.position, new Vector3(playerPosition.x, playerPosition.y, transform.position.z), 3f * Time.deltaTime);
		
		// Vector3 newCameraPosition = lerpedCameraPosition;
		
		// if(Vector2.Distance(playerPosition2d, rightBoundPos) < distanceFromEdge || Vector2.Distance(playerPosition2d, leftBoundPos) < distanceFromEdge){
				// newCameraPosition = new Vector3(transform.position.x, lerpedCameraPosition.y, transform.position.z);
		// }
		
		// if(Vector2.Distance(playerPosition2d, topBoundPos) < distanceFromEdge || Vector2.Distance(playerPosition2d, botBoundPos) < distanceFromEdge) {
				// if(newCameraPosition != lerpedCameraPosition){
					// newCameraPosition = transform.position;
				// } else {
					// newCameraPosition = new Vector3(lerpedCameraPosition.x, transform.position.y, transform.position.z);
				// }
		// }
		
		// transform.position = newCameraPosition;
		
	}
	
	// private void OnCollisionEnter(Collision c){
		// if(c.gameObject.tag == "Player"){
			// Movement.Instance().transform.position = new Vector3(c.contacts[0].point.x, Movement.Instance().transform.position.y, Movement.Instance().transform.position.z);
		// }
	// }
	
	private static CameraMovement instance = null;
	
	public static CameraMovement Instance(){
		if(instance == null){
			instance = (new GameObject("CameraMovement")).AddComponent<CameraMovement>();
		}
		return instance;
	}
}
