using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float FieldOfViewMax = 50; 
    [SerializeField] private float FieldOfViewMin = 10 ; 
    [SerializeField] float MoveSpeed = 5f;

    private float TargetFieldOfView = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 InputMoveDirection = new Vector3(0,0,0);

        if(Input.GetKey(KeyCode.W)) InputMoveDirection.z = +1f;
        if(Input.GetKey(KeyCode.S)) InputMoveDirection.z = -1f;
        if(Input.GetKey(KeyCode.A)) InputMoveDirection.x = -1f;
        if(Input.GetKey(KeyCode.D)) InputMoveDirection.x = +1f;

        Vector3 MoveDirection = transform.forward * InputMoveDirection.z + transform.right * InputMoveDirection.x ;

        
        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;


        float rotatedir = 0f;
        if(Input.GetKey(KeyCode.Q)){
            rotatedir = +1f;
        }

        if(Input.GetKey(KeyCode.E)){
            rotatedir = -1f;
        }
        float RotateSpeed = 100f;
        transform.eulerAngles += new Vector3(0,rotatedir * RotateSpeed* Time.deltaTime,0);

        // HandleCameraZoom

        if(Input.mouseScrollDelta.y > 0){
            TargetFieldOfView -= 5;           
        }

         if(Input.mouseScrollDelta.y < 0){
            TargetFieldOfView += 5;           
        }
        TargetFieldOfView = Mathf.Clamp(TargetFieldOfView, FieldOfViewMin, FieldOfViewMax);

        float ZoomSpeed = 10f;
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, TargetFieldOfView, Time.deltaTime+ ZoomSpeed);

        
        
    }

}
