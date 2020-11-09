using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public virtual string Name { get; }

    public virtual byte AllowableAmount { get; }

    public static void Method()
    {

    }

}

public class TawnHall : Building
{
    public override string Name => "TawnHall";
    public override byte AllowableAmount => 1;
}