using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    void FixedUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
            }
        }
    }

    public IEnumerator CameraShakeCo(float _maxTime, float _amount)
    {
        Vector3 originalPos = transform.localPosition;
        float shakeTime = 0.0f;

        while(shakeTime < _maxTime)
        {
            float x = Random.Range(-1f, 1f) * _amount + originalPos.x;
            float y = Random.Range(-1f, 1f) * _amount + originalPos.y;
            // Debug.Log("shake!");
            transform.localPosition = new Vector3(x, y, originalPos.z);
            shakeTime += Time.deltaTime;

            yield return new WaitForSeconds(0f);
        }
    }
}
