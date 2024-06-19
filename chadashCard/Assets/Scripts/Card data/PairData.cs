using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pair Data", menuName = "Pair Data", order = 51)]


public class PairData : ScriptableObject
{
    [SerializeField] private string pairName;
    [SerializeField] private int group;
    [SerializeField] private int indexPair;

    [SerializeField] private Sprite sprite;

    [SerializeField] private Sprite[] sprite2;

    public string PairName => pairName;
    public int Group => group;

    public int IndexPair => indexPair;

    public Sprite[] Sprite2 => sprite2;

    public Sprite Sprite
    {
        get { return sprite; }
        set
        {
            sprite = value;
        }
    }
}
