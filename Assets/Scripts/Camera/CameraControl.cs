using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
  public VoidEventSO sceneLoadedEvent;
  private CinemachineConfiner2D _confiner;

  private void Awake()
  {
    _confiner = GetComponent<CinemachineConfiner2D>();
  }

  private void OnEnable()
  {
    sceneLoadedEvent.OnEventRaised += OnSceneLoaded;
  }

  private void OnDisable()
  {
    sceneLoadedEvent.OnEventRaised -= OnSceneLoaded;
  }

  private void OnSceneLoaded()
  {
    GetNewBoundary();
  }

  private void GetNewBoundary()
  {
    var boundary = GameObject.FindGameObjectWithTag("Boundary");

    if (boundary != null)
      _confiner.m_BoundingShape2D = boundary.GetComponent<PolygonCollider2D>();
  }
}