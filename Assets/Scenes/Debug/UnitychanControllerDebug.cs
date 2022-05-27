using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitychanControllerDebug : MonoBehaviour
{

    [SerializeField] float gain = 100.0f;
    private SetUnitychanDebug setUnitychan;
    // Start is called before the first frame update

    private Vector3 preHitPosition;

    private List<Animator> UnitychanAnimators;

    void Start(){
        setUnitychan = GetComponent<SetUnitychanDebug>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setUnitychan.isUnitychan){
            if (Vector3.Distance(preHitPosition, setUnitychan.hitPosition) > 0.01f){
                Debug.Log("Destination Change!");
                for(var i = 0 ; i < setUnitychan.UnityChanReals.Count; i++){
                    setUnitychan.UnityChanReals[i].transform.LookAt(setUnitychan.hitPosition);
                }
                preHitPosition = setUnitychan.hitPosition;
            }
            for(var i = 0 ; i < setUnitychan.UnityChanReals.Count; i++){
                setUnitychan.UnityChanReals[i].transform.position = Vector3.MoveTowards(setUnitychan.UnityChanReals[i].transform.position, preHitPosition, Time.deltaTime * gain);
                var distance = Vector3.Distance(setUnitychan.UnityChanReals[i].transform.position, preHitPosition);
                var state = 0;
                if (distance > 1.0f){
                    state = 2;
                }else if (distance > 0.1f){
                    state = 1;
                }else{
                    state = 0;
                }
                Debug.Log("state : " + state + " distance : " + distance);
                setUnitychan.UnityChanAnimators[i].SetInteger("state", state);
            }
        }
    }
}