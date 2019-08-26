using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{

    private static GameObject player;
    public static GameObject Player
    {
        set
        {
            player = value;
        }
        get
        {
            return player;
        }
    }
    private static GameObject boss;

    public static GameObject Boss
    {
        set
        {
            boss = value;
        }
        get
        {
            return boss;
        }
    }

    private static GameObject car;
    public static GameObject Car
    {
        set
        {
            car = value;
        }
        get
        {
            return car;
        }
    }

    private static GameObject boundaries;
    public static GameObject Boundaries
    {
        set
        {
            boundaries = value;
        }
        get
        {
            return boundaries;
        }
    }

    private static g_ObjectManager objectManager;
    public static g_ObjectManager ObjectManager
    {
        set
        {
            objectManager = value;
        }

        get
        {
            return objectManager;
        }
    }

    void Awake()
    {
        Player = GameObject.Find("Player");
        Boss = GameObject.Find("Boss");
        Car = GameObject.Find("Ghost");
    }

    public static List<T> OrderList<T>(List<T> ListToOrder) where T : Object
    {
        for (int i = 0; i < ListToOrder.Count; i++)
        {
            if (ListToOrder[i] == null)
            {
                ListToOrder.Remove(ListToOrder[i]);
                i--;
            }
        }
        return ListToOrder;
    }

    public static bool GetInRange(Vector3 one, Vector3 two, float distance)
    {
        if (Vector3.Distance(one, two) < distance)
        {
            return true;
        }
        return false;
    }

    public static void AddToListOnce<T>(ref List<T> list, T objectToAdd) where T : class
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == objectToAdd)
            {
                return;
            }
        }

        list.Add(objectToAdd);
    }

    //public static void SetDelegate<T>(, Dictionary functions, string func) where T : 
    //{
    //
    //}

}
