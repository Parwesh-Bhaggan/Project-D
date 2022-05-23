using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.Linq;
using System.Net;
using TransitRealtime;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebRequest req = HttpWebRequest.Create("http://gtfs.ovapi.nl/nl/vehiclePositions.pb");
        FeedMessage feed = Serializer.Deserialize<FeedMessage>(req.GetResponse().GetResponseStream());

        WebRequest req2 = HttpWebRequest.Create("http://gtfs.ovapi.nl/nl/tripUpdates.pb");
        FeedMessage feed2 = Serializer.Deserialize<FeedMessage>(req2.GetResponse().GetResponseStream());

        string route_id = "32543";

        var x = feed.Entities.Select(t => t.Vehicle.Trip).
                              Where(t => t.RouteId == route_id).
                              OrderBy(t => t.StartTime);

        var y = feed2.Entities.Select(t => t.TripUpdate.Trip).
                               Where(t => t.RouteId == route_id).
                               OrderBy(t => t.StartTime);

        /* Console.WriteLine(feed2.Entities[100].Vehicle.Position.Latitude);
         Console.WriteLine(feed2.Entities[100].Vehicle.Position.Longitude);*/
        Debug.Log(x);
        Debug.Log(y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
