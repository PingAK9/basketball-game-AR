using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BallControl : MonoBehaviour
{
    Rigidbody rigid;
    public float timeLive = 6;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = true;
    }

    // Throw ball
    void OnThrow()
    {
        float _distance = Vector3.Distance(posEnd, posBegin);
        _distance = 1.5f + _distance / 400;
        GameControl.Instance.txtDistance.text = "Distance: " + _distance.ToString();
        GameControl.Instance.OnThown();
        rigid.velocity = new Vector3(0, 1, 1) * _distance;

        rigid.isKinematic = false;
        transform.parent = null;
        Invoke("DestroyBall", timeLive);
    }
    void DestroyBall()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (isTouch)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                posEnd = Input.mousePosition;
                GameControl.Instance.txtEnd.text = "Pos End: " + posEnd.ToString();
            }
#else
            if (Input.touchCount > 0)
            {
                posEnd = Input.touches[0].position;
                GameControl.Instance.txtEnd.text = "Pos End: " + posEnd.ToString();
            }
#endif
        }
    }
    #region Swipe
    bool isTouch;
    Vector3 posBegin;
    Vector3 posEnd;
    void OnMouseDown()
    {
        if (isTouch == false)
        {
            isTouch = true;
            posBegin = GetPosTouch();
            GameControl.Instance.txtBegin.text = "Pos Begin: " + posBegin.ToString();
        }
    }

    void OnMouseUp()
    {
        if (isTouch == true)
        {
            isTouch = false;
            OnThrow();
        }
    }
    Vector3 GetPosTouch()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            return pos;
        }
#else
        if (Input.touchCount > 0)
        {
            Vector3 pos = Input.touches[0].position;
            return pos;
        }
#endif
        return Vector3.zero;
    }
    #endregion

}