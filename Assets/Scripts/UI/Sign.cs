using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
  public GameObject image;
  private bool _canPress;
  private IInterract _targetItem;
  private PlayerInputControl _inputControl;

  private void Awake()
  {
    _inputControl = new PlayerInputControl();
    _inputControl.Enable();
  }

  private void OnEnable()
  {
    _inputControl.Gameplay.Confirm.started += OnConfirmPressed;
  }

  private void OnDisable()
  {
    _inputControl.Gameplay.Confirm.started -= OnConfirmPressed;
  }


  private void Update()
  {
    transform.localScale = transform.parent.transform.localScale.x < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Interactable"))
    {
      image.SetActive(true);
      _canPress = true;
      _targetItem = other.GetComponent<IInterract>();
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    image.SetActive(false);
    _canPress = false;
  }

  private void OnConfirmPressed(InputAction.CallbackContext obj)
  {
    if (_canPress)
    {
      _targetItem.Interact();
      image.SetActive(false);
      _canPress = false;
    }
  }
}