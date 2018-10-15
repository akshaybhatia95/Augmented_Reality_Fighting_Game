using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WalkBackController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        FighterController.mvBack = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        FighterController.mvBack = false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
