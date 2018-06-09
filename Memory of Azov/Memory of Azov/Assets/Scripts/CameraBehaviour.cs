﻿using UnityEngine;
using UnityEditor;

public class CameraBehaviour : MonoSingleton<CameraBehaviour> {

    public enum CameraState { Following, CrossDoor, FakeWall, ShowingItem, Cinematic}
    public enum CameraLookState { Normal, LookUp, LookDown }

    [System.Serializable]
    public struct CameraLook
    {
        [Tooltip("Distancia que mantiene la camara respecto al target actual")]
        [Range(5, 120)] public float cameraDistance;
        [Tooltip("Altura que mantiene la camara respecto al target actual")]
        [Range(0, 4)] public float cameraYOffset;
        [Tooltip("Angulo que mantiene la camara respecto al target actual")]
        [Range(-10, 90)] public float cameraAngle;
    }

    #region Public Variables
    [Header("\tGame Designers Variables")]
    [Header("Camera Variables")]
    [Tooltip("Velocidad de la camara al apuntar al target actual")]
    [Range(0,40)] public float cameraFollowSpeed = 15f;
    [Tooltip("Velocidad de rotacion de la camara al apuntar al target actual")]
    [Range(0,40)] public float cameraAngularSpeed = 15f;
    [Tooltip("La longitud en la cual el player parará de moverse al chochar una pared lateral (Linea Blanca)")]
    [Range(0,10)] public float sideDistance = 4.5f;
    [Tooltip("La longitud en la cual el player parará de moverse al chochar una trasera lateral (Linea Azul)")]
    [Range(0,10)] public float backwardsDistance = 4.5f;

    [Header("Camera Types")]
    public CameraLook normalCamera;
    public CameraLook lookUpCamera;
    public CameraLook lookDownCamera;
    public CameraLookState currentCameraLookState = CameraLookState.Normal;

    [Header("CrossDoor Variables")]
    [Tooltip("Cuantos segundos tardara la camara en moverse de la entrada de la puerta a la salida de la puerta")]
    [Range(0, 40)] public float crossDoorCameraSpeed = 0.2f;

    [Header("Cinematic Variables")]
    [Tooltip("Cuantos segundos tardara la camara en moverse de donde esta a la posicion cinematica")]
    [Range(0, 2)] public float cinematicCameraSpeed = 0.2f;
    [Range(0, 2)] public float cinematicShowingTime = 0.4f;

    [Header("Show Item Variables")]
    public Transform showingItemTransform;
    [Range(0, 4)] public float speedToShowItem = 1f;
    public AnimationCurve rotationShowItemCurve;

    [Header("\t    Own Script Variables")]
    [Tooltip("Target a seguir")]
    public Transform target;
    [Tooltip("Ray lateral para detectar paredes y por tanto clampear la camara")]
    [Range(50,150)] public float raySidesDistance = 100f;

    public Transform provisionalTarget;
    public Transform currentSurrogate;
    #endregion

    #region Private Variables
    private float xLeftWall;
    private float xRightWall;
    private float zBackWall;
    //private float currentRotation;
    private float backwardFirstDistance;
    private float cinematicTimer;
    private float cinematicFinishedTimer;
    private float showItemTimer;
    //private float cameraRotationLerpTimer;
    private bool wallsFound;
    private bool backwardDistanceSet;
    private bool dontFollow;
    private bool goingCinematic;
    private bool isCinematicFinished;
    private bool isShowingItem;

    private Vector3 moveAtPoint;

    private CameraState currentState;
    private CameraLook currentCameraLook;
    private Vector3 backwards = Vector3.zero;

    private TransparentObject currentWall;

    private Transform cinematicTarget;
    #endregion

    private void Start()
    {
        if (target == null)
        {
            target = GameManager.Instance.GetPlayer();
        }

        //currentRotation = transform.eulerAngles.x;
        currentCameraLook = normalCamera;
    }

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeCameraBehaviourState(CameraState.ShowingItem);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ChangeCameraBehaviourState(CameraState.Following);
        }

        if (Time.timeScale == 0)
            return;
        if (currentState == CameraState.Following)
            CameraMovement();
        else if (currentState == CameraState.CrossDoor)
            CrossDoorMovement();
        else if (currentState == CameraState.Cinematic)
            CinematicMovement();
        else if (currentState == CameraState.FakeWall)
            FakeWallMovement();
        else if (currentState == CameraState.ShowingItem)
            ShowItem();
    }

    #region Following Methods
    private void CameraMovement()
    {
        float yPosition = (target.position + (Vector3.up * currentCameraLook.cameraYOffset) - (transform.forward * currentCameraLook.cameraDistance)).y;

        //transform.LookAt(target.position + Vector3.up * currentCameraLook.cameraYOffset);

        if (currentCameraLook.cameraAngle != transform.eulerAngles.x) //Provisional en desuso
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.right * currentCameraLook.cameraAngle), Time.deltaTime * cameraAngularSpeed);
            //cameraRotationLerpTimer += Time.deltaTime * cameraAngularSpeed;
        }

        if (dontFollow)
        {
            return;
        }

        if (!wallsFound) //Wall detector
        {
            RaycastHit hit;

            Ray ray = new Ray(target.transform.position, -transform.right);
            Physics.Raycast(ray, out hit, raySidesDistance, LayerMask.GetMask("WallLayer"));
            //Set left wall
            xLeftWall = hit.point.x;
            //Debug.Log("Left wall name: "+hit.transform.name+" de: " +hit.transform.parent.parent.name);

            ray = new Ray(target.transform.position, transform.right);
            Physics.Raycast(ray, out hit, raySidesDistance, LayerMask.GetMask("WallLayer"));
            //Set right wall
            xRightWall = hit.point.x;
            //Debug.Log("Right wall name: "+hit.transform.name + " de: " + hit.transform.parent.parent.name);

            ray = new Ray(target.transform.position, -transform.forward);
            Physics.Raycast(ray, out hit, raySidesDistance, LayerMask.GetMask("WallLayer"));
            //Set back wall
            try
            {
                zBackWall = hit.point.z;
            }
            catch
            {
                Debug.LogWarning("Error no back wall");
                EditorApplication.isPaused = true;
            }
            //Debug.Log("Back wall name: "+hit.transform.name + " de: " + hit.transform.parent.parent.name);

            wallsFound = true;
        }

        bool rightCollision = Mathf.Abs(target.transform.position.x - xRightWall) < sideDistance;
        bool leftCollision = Mathf.Abs(target.transform.position.x - xLeftWall) < sideDistance;
        bool backCollision = Mathf.Abs(target.transform.position.z - zBackWall) < backwardsDistance;

        if (!rightCollision && !leftCollision && !backCollision)
        {
            //TotalLerp
            transform.position = Vector3.Lerp(transform.position, target.position + (Vector3.up * currentCameraLook.cameraYOffset) - (transform.forward * currentCameraLook.cameraDistance), cameraFollowSpeed * Time.deltaTime);
        }
        else if (backCollision && (leftCollision || rightCollision))
        {
            if (leftCollision)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(xLeftWall + sideDistance, yPosition, zBackWall - (transform.forward * currentCameraLook.cameraDistance).z + backwardsDistance), cameraFollowSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(xRightWall - sideDistance, yPosition, zBackWall - (transform.forward * currentCameraLook.cameraDistance).z + backwardsDistance), cameraFollowSpeed * Time.deltaTime);
            }
            //Clamp to corner position
        }
        else if (leftCollision || rightCollision)
        {
            if (leftCollision)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(xLeftWall + sideDistance, yPosition, (target.position - (transform.forward * currentCameraLook.cameraDistance)).z), cameraFollowSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(xRightWall - sideDistance, yPosition, (target.position - (transform.forward * currentCameraLook.cameraDistance)).z), cameraFollowSpeed * Time.deltaTime);
            }
        }
        else //Backwards hit
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, yPosition, zBackWall - (transform.forward * currentCameraLook.cameraDistance).z + backwardsDistance), cameraFollowSpeed * Time.deltaTime);
        }

        currentSurrogate.position = transform.position;
        currentSurrogate.rotation = transform.rotation;
    }
    #endregion

    #region Cross Door Methods
    private void CrossDoorMovement()
    {
        transform.position = Vector3.Lerp(transform.position, moveAtPoint + (Vector3.up * currentCameraLook.cameraYOffset) - (transform.forward * currentCameraLook.cameraDistance), Time.deltaTime / crossDoorCameraSpeed);
    }

    public void MoveAtPoint(Vector3 referencePoint, bool isRightSide)
    {
        moveAtPoint = referencePoint + (Vector3.down * referencePoint.y) + (Vector3.up * GameManager.Instance.GetPlayer().position.y) + ((isRightSide ? -1 : 1) * Vector3.right * sideDistance);
    }

    public void EndCrossDoorMovement()
    {
        ChangeCameraBehaviourState(CameraState.Following);
        ResetWallDetection();
    }
    #endregion

    #region Cinematic Methods
    public void CinematicMovement()
    {
        if (isCinematicFinished)
            if (Time.time >= cinematicShowingTime + cinematicFinishedTimer)
                goingCinematic = false;

        if (goingCinematic)
        {
            cinematicTimer += Time.deltaTime / cinematicCameraSpeed;
            if (cinematicTimer > 1 && !isCinematicFinished)
            {
                cinematicTimer = 1;
                cinematicFinishedTimer = Time.time;
                isCinematicFinished = true;
            }
        }
        else
        {
            cinematicTimer -= Time.deltaTime / cinematicCameraSpeed;
            if (cinematicTimer < 0)
            {
                cinematicTimer = 0;
                isCinematicFinished = false;
                GameManager.Instance.player.MoveAgainAfterMegaStop();
                ChangeCameraBehaviourState(CameraState.Following);
            }
        }

        transform.position = Vector3.Lerp(currentSurrogate.position, cinematicTarget.position, cinematicTimer);
        transform.rotation = Quaternion.Lerp(currentSurrogate.rotation, cinematicTarget.rotation, cinematicTimer);
    }

    public void TargetCinematic(Transform t)
    {
        cinematicTarget = t;
    }
    #endregion

    #region Fake Wall Methods
    private void FakeWallMovement()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + (Vector3.up * currentCameraLook.cameraYOffset) - (transform.forward * currentCameraLook.cameraDistance), cameraFollowSpeed * Time.deltaTime);
    }
    #endregion

    #region Show Item Methods
    private void ShowItem()
    {
        if (isShowingItem)
        {
            showItemTimer += Time.deltaTime / speedToShowItem;

            if (showItemTimer > 1)
                showItemTimer = 1;
        }
        else
        {
            showItemTimer -= Time.deltaTime / speedToShowItem;

            if (showItemTimer < 0)
            {
                showItemTimer = 0;
                ChangeCameraBehaviourState(CameraState.Following);
            }
        }

        transform.position = Vector3.Lerp(currentSurrogate.position, showingItemTransform.position, showItemTimer);
        transform.rotation = Quaternion.Lerp(currentSurrogate.rotation, showingItemTransform.rotation, rotationShowItemCurve.Evaluate(showItemTimer));
    }

    public void StopShowingItem()
    {
        isShowingItem = false;
    }
    #endregion

    #region Public Methods
    public void ChangeCameraBehaviourState(CameraState newState)
    {
        if (currentState == CameraState.ShowingItem)
        {
            transform.position = currentSurrogate.position;
            transform.rotation = currentSurrogate.rotation;
        }

        currentState = newState;

        if (newState == CameraState.ShowingItem)
        {
            showItemTimer = 0;
            isShowingItem = true;
        }

        if (newState == CameraState.FakeWall)
            dontFollow = !dontFollow;

        if (newState == CameraState.Cinematic)
        {
            cinematicTimer = 0;
            isCinematicFinished = false;
            goingCinematic = true;
        }
    }

    public void ChangeCameraLookState(CameraLookState newLookState)
    {
        currentCameraLookState = newLookState;
        //cameraRotationLerpTimer = 0;

        switch (currentCameraLookState)
        {
            case CameraLookState.Normal:
                currentCameraLook = normalCamera;
                break;
            case CameraLookState.LookUp:
                currentCameraLook = lookUpCamera;
                break;
            case CameraLookState.LookDown:
                currentCameraLook = lookDownCamera;
                break;
        }
    }

    public void ResetWallDetection()
    {
        wallsFound = false;
    }
    #endregion

    #region Unity Inspector/Gizmos Methods
    //Private Unity Methods
    private void OnValidate()
    {
        if (target == null)
            return;

        switch (currentCameraLookState)
        {
            case CameraLookState.Normal:
                currentCameraLook = normalCamera;
                break;
            case CameraLookState.LookUp:
                currentCameraLook = lookUpCamera;
                break;
            case CameraLookState.LookDown:
                currentCameraLook = lookDownCamera;
                break;
        }

        if (currentCameraLook.cameraAngle != transform.eulerAngles.x)
        {
            transform.rotation = Quaternion.Euler(Vector3.right * currentCameraLook.cameraAngle);
        }
        if (currentCameraLook.cameraDistance != (target.position - transform.position).magnitude)
        {
            transform.position = target.position + (Vector3.up * currentCameraLook.cameraYOffset) - (transform.forward * currentCameraLook.cameraDistance);
        }
        if (currentCameraLook.cameraYOffset != (transform.position - target.position + (transform.forward * currentCameraLook.cameraDistance)).y)
        {
            transform.position = target.position + (Vector3.up * currentCameraLook.cameraYOffset) - (transform.forward * currentCameraLook.cameraDistance);
        }
    }

    private void OnDrawGizmosSelected()
    {
        backwards = transform.position - target.position;
        backwards.y = 0;
        backwards.Normalize();

        Gizmos.color = Color.white;
        Gizmos.DrawLine(target.transform.position, target.transform.position + (transform.right * sideDistance));
        Gizmos.DrawLine(target.transform.position, target.transform.position - (transform.right * sideDistance));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(target.transform.position, target.transform.position + (backwards * backwardsDistance));
    }
    #endregion
}
