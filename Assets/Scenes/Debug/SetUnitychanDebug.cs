using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SetUnitychanDebug : MonoBehaviour
{   
    [SerializeField] GameObject UnityChan;

    public bool isUnitychan;
    
    public List<GameObject> UnityChanReals;

    public List<Animator> UnityChanAnimators;

    public Vector3 hitPosition;

    // Start is called before the first frame update

    void Start(){
        UnityChanReals = new List<GameObject>();
        UnityChanAnimators = new List<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit_info = new RaycastHit ();

            bool is_hit = Physics.Raycast(ray, out hit_info); 

            if (is_hit)
            {
                Debug.Log("Point : " + hit_info.point);
                Debug.Log("Create Unitychan!");
                isUnitychan = true;
                var unitychan = Instantiate(UnityChan, hit_info.point, hit_info.transform.rotation);
                UnityChanReals.Add(unitychan);
                UnityChanAnimators.Add(unitychan.GetComponent<Animator>());
                hitPosition = hit_info.point;
            }   
        }
    }
}
