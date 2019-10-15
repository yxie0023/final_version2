var view = new ol.View({
    center: [0, 0],
    zoom: 10
    });
                
var map = new ol.Map({
    loadTilesWhileAnimating: true,
    loadTilesWhileInteracting: true,
    renderer: 'canvas',
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
     ],
     target: document.getElementById('map'),
     view: view
});
        
        var geolocation = new ol.Geolocation({
                                // enableHighAccuracy must be set to true to have the heading value.
                                trackingOptions: {
                                enableHighAccuracy: true
                        },
                        projection: view.getProjection()
                    });
            
                    // handle geolocation error.
        geolocation.on('error', function (error) {
            var info = document.getElementById('info');
                            info.innerHTML = error.message;
                            info.style.display = '';
                        });
                
                        var accuracyFeature = new ol.Feature();
                
        geolocation.on('change:accuracyGeometry', function () {
                                accuracyFeature.setGeometry(geolocation.getAccuracyGeometry());
                            });
                    
                            var positionFeature = new ol.Feature();
        positionFeature.setStyle(new ol.style.Style({
                                image: new ol.style.Circle({
                                radius: 6,
                fill: new ol.style.Fill({
                                color: '#3399CC'
                        }),
                stroke: new ol.style.Stroke({
                                color: '#fff',
                            width: 2
                        })
                    })
                }));
        
        geolocation.on('change:position', function () {
            var coordinates = geolocation.getPosition();
                            positionFeature.setGeometry(coordinates ?
                                new ol.geom.Point(coordinates) : null);
                            centerMap(ol.proj.toLonLat(coordinates));
                        });
                
        new ol.layer.Vector({
                                map: map,
            source: new ol.source.Vector({
                                features: [accuracyFeature, positionFeature]
                        })
                    });
            
                    geolocation.setTracking(true);
            
                    //145.038,-37.876
        function centerMap(coordinates) {
                                map.getView().setCenter(ol.proj.transform([coordinates[0], coordinates[1]], 'EPSG:4326', 'EPSG:3857'));
                            map.getView().setZoom(15);
                        }
                
                        
                        var locations = [];
                        // The below is a simple jQuery selector
    $(".coordinates").each(function () {
    var longitude = $("Model.Longitude", this).text().trim();
                            var latitude = $("MOdel.Latitude", this).text().trim();
                            var description = $("Model.Description", this).text().trim();
                            var name = $("Model.Name", this).text().trim();
                            // Create a point data structure to hold the values.
    var point = {
                                "latitude": latitude,
                            "longitude": longitude,
                            "description": description,
                            "name": name
                        };
                        // Push them all into an array.
                        locations.push(point);
                    });
                    
                                var restaurantFeature = [];
                    
            var vectorSource = new ol.source.Vector({ // VectorSource({
                            });
                    
                            var data = [];
        for (var i = 0; i < locations.length; i++) {
            var feature = new ol.Feature({
                                name: locations[i].name,
                            geometry: new ol.geom.Point(ol.proj.transform([locations[i].longitude, locations[i].latitude])),
                        });
            
                        vectorSource.addFeature(feature);
                        data.push(feature)
                    }
            
        var vectorLayer = new ol.layer.Vector({ // VectorLayer({
                                source: vectorSource
                        });
                
        var rasterLayer = new ol.layer.Tile({ // TileLayer({
                                source: new ol.source.TileJSON({
                                url: 'https://api.tiles.mapbox.com/v3/mapbox.geography-class.json?secure',
                            crossOrigin: ''
                        })
                    });
            
                    map.addLayer(vectorLayer)
            
                    var container = document.getElementById('popup');
                    var content = document.getElementById('popup-content');
                var closer = document.getElementById('popup-closer');
            
    var overlay = new ol.Overlay({
                                element: container,
                        positioning: 'bottom-center',
                        stopEvent: false,
                        offset: [0, -25]
                        });
                        map.addOverlay(overlay);
                
        closer.onclick = function () {
                                overlay.setPosition(undefined);
                            closer.blur();
                            return false;
                        };
                    
    map.on('click', function (evt) {
        var coordinate = evt.coordinate;
                    
                    
                            var feature = map.forEachFeatureAtPixel(evt.pixel,
            function (feature) {
            return feature;
                        });
                
        if (feature) {
            var coordinates = map.getCoordinateFromPixel(evt.pixel);
                            overlay.setPosition(coordinates);
            for (i; i < locations.length; i++) {
                                content.innerHTML = '<p>' + feature.get(locations[i].name) + '</p><code>longitude:</code>' + locations[i].longitude;
                            }
        } else {
                                overlay.setPosition(undefined);
                            closer.blur();
                            return false;
                        }
                    });
                
                        // change mouse cursor when over marker
        map.on('pointermove', function (e) {
        if (e.dragging) {
                                overlay.setPosition(undefined);
                            closer.blur();
                            return;
                        }
                        var pixel = map.getEventPixel(e.originalEvent);
                        var hit = map.hasFeatureAtPixel(pixel);
                        map.getTarget().style.cursor = hit ? 'pointer' : '';
                    });
                