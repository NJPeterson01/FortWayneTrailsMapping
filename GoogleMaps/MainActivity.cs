using Android.App;
using Android.OS;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using Android.Locations;
using Plugin.Geolocator;
using Plugin.Permissions;
using System;

namespace GoogleMaps
{
    [Activity(Label = "GoogleMaps", MainLauncher = true)]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        private GoogleMap GMap;
        //private LocationManager _locationManager;
        //private Location _location;
        //private string _locationProvider;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);
            SetUpMap();
        }
       
      
        private void SetUpMap()
        {
            if (GMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.GMap = googleMap;

            //Create Map Marker
            GMap.UiSettings.ZoomControlsEnabled = true;

            //Get Location
            var locator = CrossGeolocator.Current;
            //Accuracy (Meters)
            locator.DesiredAccuracy = 100;

            //Access Position
            var position = await locator.GetPositionAsync;

            //Get Current Longitude and Latitude
            var lat = position.Latitude;
            var log = position.Longitude;

            //Generate Map Markers
            LatLng latlng = new LatLng(Convert.ToDouble(lat), Convert.ToDouble(log));
           // LatLng latlng2 = new LatLng(Convert.ToDouble(13.0350), Convert.ToDouble(80.3050));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 25);
            GMap.MoveCamera(camera);
            MarkerOptions options = new MarkerOptions().SetPosition(latlng).SetTitle("Chennai");
          //  MarkerOptions options2 = new MarkerOptions().SetPosition(latlng2).SetTitle("ChennaiYo");
            GMap.AddMarker(options);
          //  GMap.AddMarker(options2);
        }




    }
    public class Activity1 : Activity, ILocationListener
    {
        // removed code for clarity

        public void OnLocationChanged(Location location) { }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
    }
}

