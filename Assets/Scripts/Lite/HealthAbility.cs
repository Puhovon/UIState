using Scripts.Player;
using UnityEngine;

namespace Lite
{
    public class HealthAbility : MonoBehaviour, IAbilityItem
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private int heal;
        public void Interact()
        {
            playerHealth.HealthPoint += heal;
        }
    }
}