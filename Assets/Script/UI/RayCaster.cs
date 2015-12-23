using UnityEngine;
using System.Collections;

public class RayCaster : MonoBehaviour {

	[SerializeField] MBasicButton tempButton = null;

    bool isWork = true;

    void OnEnable()
    {
    }
     void OnDisable()
    {
    }


	// Update is called once per frame
	void Update () {

        if (isWork)
        {
    		//for 3d
    		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    		RaycastHit hit3d;
    		MBasicButton button = null;
            // if (Physics.Raycast(, -Vector3.up, out hit, 100f)
            if (Physics.Raycast(ray, out hit3d))
            {
                button = hit3d.collider.GetComponent<MBasicButton>();
               
            } 

            //for 2d

            RaycastHit2D hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit2d != null && hit2d.collider != null)
            {
            	button = hit2d.collider.GetComponent<MBasicButton>();
            }


            if ( button != null ) {
    	            if ( Input.GetMouseButtonUp(0))
    	            {
    	            	button.OnClick();	
    	            } else if (Input.GetMouseButton(0)) {
    	            	button.OnPress();
    	            } else {
    	            	button.OnHover();
    	            }
                }

        	if (button != tempButton)
            {
            	if (tempButton != null)
            		tempButton.OnExit();
            	tempButton = button;
            }
        }
	}
}