using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using TransitRealtime;
using System.Net;
using System.Linq;

public class GetTram23StartTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebRequest req = HttpWebRequest.Create("http://gtfs.ovapi.nl/nl/vehiclePositions.pb");
        FeedMessage feed = Serializer.Deserialize<FeedMessage>(req.GetResponse().GetResponseStream());

        WebRequest req2 = HttpWebRequest.Create("http://gtfs.ovapi.nl/nl/tripUpdates.pb");
        FeedMessage feed2 = Serializer.Deserialize<FeedMessage>(req2.GetResponse().GetResponseStream());

        string route_id = "62286";

        var x = feed.Entities.Select(t => t.Vehicle.Trip).
                              Where(t => t.RouteId == route_id).
                              OrderBy(t => t.StartTime);

        var y = feed2.Entities.Select(t => t.TripUpdate.Trip).
                               Where(t => t.RouteId == route_id).
                               OrderBy(t => t.StartTime);

        List<TripDescriptor> vehicleTrips = y.ToList();

        Debug.Log(x);

        foreach (var i in vehicleTrips)
        {
            Debug.Log(i.StartTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
