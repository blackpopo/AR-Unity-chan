using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class SetUnitychan : MonoBehaviour
{   
    [SerializeField] GameObject UnityChan;

    public bool isUnitychan;
    
    public List<GameObject> UnityChanReals;

    public List<Animator> UnityChanAnimators;

    private ARRaycastManager aRRaycastManager;

    private List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();

    public Vector2 hitPosition;

    public Quaternion hitRotation;

    public bool isHit;
    
    // Start is called before the first frame update
    void Start()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        UnityChanReals = new List<GameObject>();
        UnityChanAnimators = new List<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.touchCount > 0 && !isHit)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)　　//画面に指が触れた時に処理する
            {
                if (aRRaycastManager.Raycast(touch.position, aRRaycastHits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = aRRaycastHits[0].pose;　　//RayとARPlaneが衝突しところのPose
                    // Debug.Log("Point : " + hitPose.position);
                    // Debug.Log("Create Unitychan!");
                    isUnitychan = true;
                    var unitychan = Instantiate(UnityChan, hitPose.position, Quaternion.Euler(Vector3.zero));
                    UnityChanReals.Add(unitychan);
                    UnityChanAnimators.Add(unitychan.GetComponent<Animator>());
                    hitPosition = hitPose.position;
                    isHit = true;
                }
            }
        }
    }

    // Start is called before the first frame update

}
