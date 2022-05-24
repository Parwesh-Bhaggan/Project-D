using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using TransitRealtime;
using UnityEngine;

public class TestOV : MonoBehaviour
{
    // Start is called before the first frame update
    List<TripDescriptor> vehicleTrips;
    string tramStartTime;
    DateTime tramStartTimeDateTime;
    public GameObject tram;
    public Transform checkPoint;

    bool isSpawnable = false;
    bool substractTime = true;
    int tijdVerschil = 0;
    void Start()
    {
        WebRequest req = HttpWebRequest.Create("http://gtfs.ovapi.nl/nl/vehiclePositions.pb");
        FeedMessage feed = Serializer.Deserialize<FeedMessage>(req.GetResponse().GetResponseStream());

        WebRequest req2 = HttpWebRequest.Create("http://gtfs.ovapi.nl/nl/tripUpdates.pb");
        FeedMessage feed2 = Serializer.Deserialize<FeedMessage>(req2.GetResponse().GetResponseStream());

        string route_id = "62286";

        var beverwaardToMarconiplein = feed.Entities.Select(t => t.Vehicle.Trip).
                              Where(t => t.RouteId == route_id).
                              Where(t => t.DirectionId == 0).
                              OrderBy(t => t.StartTime);

        var x = feed2.Entities.Select(t => t.TripUpdate.Trip).
                               Where(t => t.RouteId == route_id).
                               OrderBy(t => t.StartTime);

        vehicleTrips = beverwaardToMarconiplein.ToList();

         tramStartTime = vehicleTrips[8].StartTime;
         tramStartTimeDateTime = DateTime.ParseExact(tramStartTime, "HH:mm:ss", CultureInfo.InvariantCulture);

       
        Debug.Log($"Vertrek tijd: {tramStartTime}");
        int index = 0;
        foreach (var vehicle in vehicleTrips) 
        {
            Debug.Log($"Index, Vehicle Time: {index} : {vehicle.StartTime}");
            index++;
        }
      
    }

    // Update is called once per frame
    void Update()
    {
       

        if (substractTime) { 
        
         tijdVerschil = tramStartTimeDateTime.Subtract(DateTime.Now).Minutes;
            Debug.Log($"Tijdverschil: {tijdVerschil}");
        }
    
        
        if (tijdVerschil <= 1)
        {
            isSpawnable = true;
            substractTime = false;
        }

        if (isSpawnable) 
        {
            Instantiate(tram, checkPoint.position, Quaternion.identity);
            Debug.Log("Test time");
            tijdVerschil = 1000;
            isSpawnable = false;
            
        }

        //minutes

    }
}
