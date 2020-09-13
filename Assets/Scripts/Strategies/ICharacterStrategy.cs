using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterStrategy
{
    float Life { get; set; }
    float BasicDamage { get; set; }
    float HeavyDamage { get; set; }

    float BasicCooldown { get; set; }
    float HeavyCooldown { get; set; }
}
