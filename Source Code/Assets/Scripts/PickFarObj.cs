using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
public class PickFarObj : MonoBehaviour 
{
    [SerializeField] float lineWidth = 0.1f;
    [SerializeField] Color lineColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public static PickFarObj instance;
    [SerializeField] private LineRenderer line1;
    [SerializeField] private LineRenderer line2;

    public OVRGrabber[] hands = new OVRGrabber[2];

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        //line1 = GetComponent<LineRenderer>();
        //line2 = gameObject.AddComponent<LineRenderer>();
        //line2.material = line1.material;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < hands.Length; i++)
        {
            

            Ray ray = new Ray(hands[i].transform.position, hands[i].transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {

                if (hands[i].isClickingTheButton)
                    continue;

                LineRenderer line = (i == 0) ? line1 : line2;
                DrawLine(line, ray.origin, hit.point, lineWidth, lineColor);

                //treat if hit a grabbable
                OVRGrabbable grabbable = hit.transform.GetComponent<OVRGrabbable>();
                if (grabbable != null)
                {
                    if (i == 0)
                        //Debug.Log("seen");

                    if (grabbable == hands[i].lookingGrabbable)
                        continue;


                    hands[i].lookingGrabbable = grabbable;
                    hands[i].lookingGrabbable.GetComponent<MeshRenderer>().material.color = Color.red;
                    // Add the grabbable
                    int refCount = 0;
                    hands[i].m_grabCandidates.TryGetValue(grabbable, out refCount);
                    hands[i].m_grabCandidates[grabbable] = refCount + 1;

                    //Debug.Log("obj to pool");

                    continue;
                }

                //treat if hit UI
                Button3D button = hit.transform.GetComponent<Button3D>();
                if (button)
                {
                    hands[i].lookingButton = button;
                    hands[i].lookingButton.OnEnter();

                    continue;
                }

                StopLook(hands[i], i);
            }
            else
            {
                StopLook(hands[i], i);

                LineRenderer line = (i == 0) ? line1 : line2;
                DrawLine(line, Vector3.zero, Vector3.zero, 0, Vector4.zero);
            }

        }
    }

    private void StopLook(OVRGrabber hand, int i)
    {
        if (hand.lookingGrabbable)
        {
            hand.lookingGrabbable.GetComponent<MeshRenderer>().material.color = Color.white;
            Release(hand);
            hand.lookingGrabbable = null;
        }


        if (hand.lookingButton)
        {
            hand.lookingButton.OnExit();
            hand.lookingButton = null;
        }



    }

    public void Release(OVRGrabber grabber)
    {
        if (!grabber.lookingGrabbable)
            return;

        grabber.lookingGrabbable.GetComponent<MeshRenderer>().material.color = Color.white;

        // Remove the grabbable
        int refCount = 0;
        bool found = grabber.m_grabCandidates.TryGetValue(grabber.lookingGrabbable, out refCount);
        if (!found)
        {
            return;
        }

        if (refCount > 1)
        {
            grabber.m_grabCandidates[grabber.lookingGrabbable] = refCount - 1;
        }
        else
        {
            grabber.m_grabCandidates.Remove(grabber.lookingGrabbable);
        }

        grabber.lookingGrabbable = null;

        //Debug.Log("Obj released");
    }

    private void DrawLine(LineRenderer line ,Vector3 start, Vector3 end, float width, Color cor)
    {
        line.SetPositions(new Vector3[]{ start, end });
        line.startColor = (cor);
        line.endColor = (cor);
        line.startWidth = width;
        line.endWidth = width;
    }
}
