using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    //allows for the enemy to change states and what the states to derive form
    public abstract EnemyState RunCurrentState(EnemyStateManager Enemy);

}
