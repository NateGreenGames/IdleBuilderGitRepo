using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
	[Header("General Settings:")]
	public Camera mainCamera;

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private Transform camTransform;
	private Vector3 originalPos;


	[Header("Camera Shake Settings:")]
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseRate = 1.0f;

	// How long the object should shake for.
	private float shakeDuration = 0f;

	void Awake()
	{
		if (camTransform == null)
		{
			mainCamera = Camera.main;
			camTransform = mainCamera.transform;
			originalPos = camTransform.localPosition;
		}
	}
    void Update()
	{
		if (shakeDuration > 0)
		{
			camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseRate;
		}
		else
		{
			shakeDuration = 0f;
			camTransform.localPosition = originalPos;
		}
	}


	public void ScreenShake(float _duration)
	{
		shakeDuration += _duration;
	}
}
