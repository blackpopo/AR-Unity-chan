using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollistionSoundMotion : MonoBehaviour
{
    // ぶつかった時の音
    [SerializeField] AudioClip se;

    // ぶつかった時に音を鳴らす
    void OnCollisionEnter(Collision col)
    {
        AudioSource.PlayClipAtPoint(se, transform.position);
    }

}
