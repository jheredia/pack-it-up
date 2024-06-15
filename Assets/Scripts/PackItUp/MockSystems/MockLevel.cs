using System;
using System.Collections.Generic;
using UnityEngine;

public class MockLevel : MonoBehaviour
{
    private MockEndZone _endZone;
    private List<object> _items;

    public MockEndZone GetEndZone() => _endZone;
}
