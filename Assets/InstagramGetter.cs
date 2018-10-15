using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;
using Mapbox.Unity.Map;

public class InstagramGetter : MonoBehaviour {

    public AbstractMap _map;
	// Use this for initialization
	IEnumerator Start () {
        string url = "https://api.instagram.com/v1/media/search?lat=6.7972974&lng=79.9007093&distance=1000&access_token=3520634578.87c5045.ee79199cd1164e7fb8b991a3616ada2b";
        WWW www = new WWW(url);
        yield return www;

        string api_response = www.text;
        Debug.Log(api_response);

        IDictionary apiParse = (IDictionary)Json.Deserialize(api_response);
        IList instagramPicturesList = (IList)apiParse["data"];

        foreach(IDictionary instagramPicture in instagramPicturesList)
        {
            IDictionary images = (IDictionary)instagramPicture["images"];
            IDictionary standardResolution = (IDictionary)images["standard_resolution"];
            string mainPic_url = (string)standardResolution["url"];
            Debug.Log(mainPic_url);

            WWW mainPic = new WWW(mainPic_url);
            yield return mainPic;

            IDictionary location = (IDictionary)instagramPicture["location"];
            double lat = (double)location["latitude"];
            double lon = (double)location["longitude"];

            GameObject instaPost = Instantiate(Resources.Load("ARBanner")) as GameObject;
            instaPost.transform.GetChild(0).GetComponent<MeshRenderer>().material.mainTexture = mainPic.texture;
            instaPost.transform.position = _map.GeoToWorldPosition(new Mapbox.Utils.Vector2d(lat, lon))+new Vector3(0,2f,0);
            instaPost.transform.SetParent(_map.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
