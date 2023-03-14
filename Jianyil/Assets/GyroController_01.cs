using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroController_01 : MonoBehaviour
{
   
    public float moveSpeed = 1;//movement speed 
    public  GameObject target;
 
    private Vector2 oldPosition;
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;
 
 
    public float distance = 50;
    private bool flag = false;
    //Camera location
    private float x = 0f;
    private float y = 0f;
    //speed of left and right
    public float xSpeed = 250f;
    public float ySpeed = 120f;
    //zoom
    public float yMinLimit = -360;
    public float yMaxLimit = 360;
    //rotate
    private bool isRotate = true;
    //counter
    private float count = 0;

    public static GyroController_01 _instance;
    //initial location
    void Start()
    {
        _instance = this;
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        if (GetComponent< Rigidbody >())
            GetComponent< Rigidbody >().freezeRotation = true;
    }
 
    
 
    // Update is called once per frame  
    void Update()
    {
 
        if (isRotate)
        {
 
            target.transform.Rotate(Vector3.down, Time.deltaTime * moveSpeed,Space.World);
            
        }
        if (!isRotate)
        {
            count += Time.deltaTime;
            if (count > 5)
            {
                count = 0;
                isRotate = true;
            }
        }
 
            //mouse
            if (Input.GetMouseButton(0))
            {
                //X and Y
                x += Input.GetAxis("Mouse X") * xSpeed *Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            isRotate = false;
            }
            //
        float temp = Input.GetAxis("Mouse ScrollWheel");
        if (temp!=0)
        {
            if (temp>0)
            {
                
                if (distance > 1)
                {
                    distance -= 1;
                }
            }
            if (temp<0)
            {
                
                if (distance < 10
                    )
                {
                    distance += 1;
                }
            }
        }
 
    }
 
    //count dictand ，zoom+:true，zoom- :false  
    bool IsEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //old distance  
        float oldDistance = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        //new distance  
        float newDistance = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
 
        if (oldDistance < newDistance)
        {
            //zoom+  
            return true;
        }
        else
        {
            //zoom-  
            return false;
        }
    }
 
    //after Update
    void LateUpdate()
    {
        if (target)
        {
            //reset camera
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation *(new Vector3(0.0f, 0.0f, -distance)) + target.transform.position;
 
            transform.rotation = rotation;
            transform.position = position;
        }
    }
    float ClampAngle(float angle,float min,float max) {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
 
    }
   
}
