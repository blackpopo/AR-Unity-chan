using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitychanController : MonoBehaviour
{

    [SerializeField] float gain = 0.01f;
    private SetUnitychan setUnitychan;
    // Start is called before the first frame update

    private Vector3 preHitPosition;

    private List<Animator> UnitychanAnimators;

    void Start(){
        setUnitychan = GetComponent<SetUnitychan>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)//画面に指が触れた時に処理する
            {
            if (setUnitychan.isUnitychan){
                if (Vector3.Distance(preHitPosition, setUnitychan.hitPosition) > 0.01f){ //別の場所にタッチ
                    for(var i = 0 ; i < setUnitychan.UnityChanReals.Count; i++){
                        setUnitychan.UnityChanReals[i].transform.LookAt(setUnitychan.hitPosition);
                    }
                preHitPosition = setUnitychan.hitPosition;
                }
            }
        }
            if(setUnitychan.isUnitychan){
                for(var i = 0 ; i < setUnitychan.UnityChanReals.Count; i++){
                    setUnitychan.UnityChanReals[i].transform.position = Vector3.MoveTowards(setUnitychan.UnityChanReals[i].transform.position, preHitPosition, Time.deltaTime * gain);
                    var distance = Vector3.Distance(setUnitychan.UnityChanReals[i].transform.position, preHitPosition);
                    var state = 0;
                    if (distance > 0.01f){
                        state = 2;
                    }else if (distance > 0.001f){
                        state = 1;
                    }else{
                        state = 0;
                    }
                    // Debug.Log("state : " + state + " distance : " + distance);
                    setUnitychan.UnityChanAnimators[i].SetInteger("state", state);
                }
            }
        }
    }
}