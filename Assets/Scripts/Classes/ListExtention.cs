using System.Collections.Generic;
using UnityEngine;

// I'm using this to ease the debug with the AndroidDeviceMonitor
public static class ListExtension
{
   public static List<GameObject> GetOutOfSight(this List<GameObject> list, GameObject otherObject)
   {
        List<GameObject> ret = new List<GameObject>(list.Count);
        foreach(GameObject element in list)
        {
            RaycastHit hit;
            if(Physics.Raycast(element.transform.position, otherObject.transform.position - element.transform.position, out hit))
            {
                if(hit.collider.gameObject != otherObject)
                {
                    ret.Add(element);
                }
            }
        }
        return ret;
   }
}
