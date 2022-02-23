using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LightBillbordBlink : MonoBehaviour
{
    [SerializeField] private float _rate = 2f;
    private MeshRenderer _lightBillbord;
    private float _elapsedTime;

    private void Awake()
    {
        _lightBillbord = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        _elapsedTime = _elapsedTime + (Time.deltaTime * _rate);
        _lightBillbord.enabled = Mathf.PingPong(_elapsedTime, 1f) < 0.5f;
    }
}
