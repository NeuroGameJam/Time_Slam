using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTS : MonoBehaviour
{
    Button3D lookingButton;
    public Camera cam;

    void Update()
    {
        Point();

        if (lookingButton)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lookingButton.OnClick();
            }

            if (Input.GetMouseButtonUp(0))
            {
                lookingButton.OnRelease();
            }
        }

    }


    void Point()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //treat if hit UI
            Button3D button = hit.transform.GetComponent<Button3D>();
            if (button && button != lookingButton)
            {
                lookingButton = button;
                lookingButton.OnEnter();
            }
        }
        else
        {
            if (lookingButton)
            {
                lookingButton.OnExit();
                lookingButton = null;
            }
        }
    }
}
