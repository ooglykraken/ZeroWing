using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject downTarget;
	
	public string downArgument;
	public string downFunction;
	
	public void Update(){
		
		bool mobilePlatform = false;
		
		if(Application.loadedLevel == 0){
			if(Menu.Instance().platformIsMobile)
				mobilePlatform = true;
		} else if(Application.loadedLevel == 1){
			if(Gameplay.Instance().platformIsMobile)
				mobilePlatform = true;
		} else {
			mobilePlatform = false;
		}
	
		if(mobilePlatform){
			if(Input.touchCount >= 1){
				Touch playerTouch = Input.GetTouch(0);
				
				Bounds buttonBounds = gameObject.GetComponent<Collider>().bounds;
				
				Vector3 min = Camera.main.WorldToScreenPoint(buttonBounds.min);
				Vector3 max = Camera.main.WorldToScreenPoint(buttonBounds.max);
		
				if(playerTouch.position.x >= min.x && playerTouch.position.x <= max.x && playerTouch.position.y <= max.y && playerTouch.position.y >= min.y){
					if (downTarget) {
						if (downFunction.Length > 0) {
							if (downArgument.Length > 0)
								downTarget.SendMessage(downFunction, downArgument, SendMessageOptions.DontRequireReceiver);
							else
								downTarget.SendMessage(downFunction, SendMessageOptions.DontRequireReceiver);
						}
					}
				} 
			}
		}
	}
	
	public void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if (downTarget) {
				if (downFunction.Length > 0) {
					if (downArgument.Length > 0)
						downTarget.SendMessage(downFunction, downArgument, SendMessageOptions.DontRequireReceiver);
					else
						downTarget.SendMessage(downFunction, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
	
}
