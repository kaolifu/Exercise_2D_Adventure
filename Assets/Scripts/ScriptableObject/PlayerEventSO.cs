using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerEvent", menuName = "SO/PlayerEvent")]
public class PlayerEventSO : ScriptableObject
{
  public UnityAction<Player> OnEventRaised;

  public void RaiseEvent(Player player)
  {
    OnEventRaised?.Invoke(player);
  }
}