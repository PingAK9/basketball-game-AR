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
        GameControl.Instance.OnThown();
        rigid.velocity = new Vector3(0, 1, 1) * _distance;

        rigid.isKinematic = false;
        transform.parent = null;
        Invoke("DestroyBall", timeLive);
        Invoke("CheckEnter", timeLive - 1);
    }
    bool isScore = false;
    void OnTriggerEnter(Collider collision)
    {
        if (isScore == false)
        {
            if (collision.gameObject.name == "Basket")
            {
                isScore = true;
                AudioManager.OnPlayWin();
                GameControl.Instance.OnScore();
            }
        }

    }
    void CheckEnter()
    {
        if (isScore == false)
        {
            AudioManager.OnPlayLose();
            GameControl.Instance.ResetGame();
        }
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
            }
#else
            if (Input.touchCount > 0)
            {
                posEnd = Input.touches[0].position;
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