using UnityEngine;
using System.Collections;
using Assets.Scripts.ConcreteStrategies.Action;
using System.Linq;

namespace Assets.Scripts.ConcreteStrategies.Character
{
    public class TerrestrialCharacter : CharacterStrategy
    {
        private void Start()
        {
            body = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            MovementStrategy = GetComponent<IActionStrategy>();
            SubscribeToMovementEvents();
        }
    }
}