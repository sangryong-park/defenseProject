using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveCam : MonoBehaviour
{
    Vector2?[] touchPrevPos = { null, null };
    Vector2 touchPrevVector;
    float touchPrevDist;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void ExitDrag()
    {
        touchPrevPos[0] = null;
        touchPrevPos[1] = null;
        touchPrevVector = Vector2.zero;
        touchPrevDist = 0f;

    }

    public void OnDrag()
    {
        CameraLimit();
        if (Input.touchCount == 0 && !DragManager.Instance.isDraging)
        {
            touchPrevPos[0] = null;
            touchPrevPos[1] = null;
        }
        else if (Input.touchCount == 1 && !DragManager.Instance.isDraging)
        {

            if (touchPrevPos[0] == null || touchPrevPos[1] != null)
            {
                touchPrevPos[0] = Input.GetTouch(0).position;
                touchPrevPos[1] = null;
            }
            else
            {
                Vector2 touchNewPos = Input.GetTouch(0).position;
                transform.position += transform.TransformDirection((Vector3)((touchPrevPos[0] - touchNewPos) * Camera.main.orthographicSize / Camera.main.pixelHeight * 2f));


                touchPrevPos[0] = touchNewPos;
            }
        }
        else if (Input.touchCount == 2 && !DragManager.Instance.isDraging)
        {

            if (touchPrevPos[1] == null)
            {
                touchPrevPos[0] = Input.GetTouch(0).position;
                touchPrevPos[1] = Input.GetTouch(1).position;
                touchPrevVector = (Vector2)(touchPrevPos[0] - touchPrevPos[1]);
                touchPrevDist = touchPrevVector.magnitude;

            }
            else
            {
                Vector2 screen = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

                Vector2[] touchNewPos = { Input.GetTouch(0).position, Input.GetTouch(1).position };
                Vector2 touchNewVector = touchNewPos[0] - touchNewPos[1];
                float touchNewDist = touchPrevVector.magnitude;

                //transform.position += transform.TransformDirection((Vector3)((touchPrevPos[0] + touchPrevPos[1] - screen) * Camera.main.orthographicSize / screen.y));

                //Camera.main.orthographicSize += touchPrevDist / touchNewDist;
                if (touchNewDist > touchPrevDist)
                {
                    transform.position -= new Vector3(0, (touchPrevDist / touchNewDist) * 5.0f * Time.deltaTime, 0);
                }
                else if(touchNewDist < touchPrevDist)
                {
                    transform.position += new Vector3(0, (touchPrevDist / touchNewDist) * 5.0f * Time.deltaTime, 0);
                }


                //transform.position -= transform.TransformDirection((touchNewPos[0] + touchNewPos[1] - screen) * Camera.main.orthographicSize / screen.y);


                // Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 3f);
                // Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize, 5f);




                touchPrevPos[0] = touchNewPos[0];
                touchPrevPos[1] = touchNewPos[1];
                touchPrevVector = touchNewVector;
                touchPrevDist = touchNewDist;

            }
        }
        else
        {
            return;
        }
    }
    

    void MoveLimit()
    {
        Vector3 temp;
        temp.x = Mathf.Clamp(transform.position.x, -2, 2);
        temp.y = Mathf.Clamp(transform.position.y, transform.position.y, transform.position.y);
        temp.z = Mathf.Clamp(transform.position.z, -8, -2);
    }

    void CameraLimit()
    {
        Vector3 temp;
        temp.x = Mathf.Clamp(transform.position.x, -2, 2);
        temp.y = Mathf.Clamp(transform.position.y, 10, 15);
        temp.z = Mathf.Clamp(transform.position.z, -8, -2);

        transform.position = temp;
    }
}
