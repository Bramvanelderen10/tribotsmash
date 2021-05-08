using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public interface Hitpoints
{
    bool IsAlive { get; }
    float Hitpoints { get; set; }
    bool AddDamage(float damage);
}

