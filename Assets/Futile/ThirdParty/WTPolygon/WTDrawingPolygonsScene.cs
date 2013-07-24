using UnityEngine;
using System.Collections;

// This is just an example of how to use the polygon sprite
public class WTDrawingPolygonsScene : MonoBehaviour {
	void Start () {
		FutileParams fp = new FutileParams(true, true, false, false);
		fp.AddResolutionLevel(480f, 1.0f, 1.0f, "-res1");
		fp.backgroundColor = Color.black;
		fp.origin = Vector2.zero;

		Futile.instance.Init(fp);
	    	
		Vector2[] vertices = new Vector2[] {
			new Vector2(0, 0),
			new Vector2(25, 5),
			new Vector2(35, 45),
			new Vector2(20, 23),
			new Vector2(5, 57)
		};

		WTPolygonSprite s = new WTPolygonSprite(new WTPolygonData(vertices));
		s.color = Color.green;
		s.x = Futile.screen.halfWidth - 130;
		s.y = Futile.screen.halfHeight;
		Futile.stage.AddChild(s);

		s = new WTPolygonSprite(new WTPolygonData(vertices));
		s.color = Color.green;
		s.x = Futile.screen.halfWidth - 20;
	    	s.y = Futile.screen.halfHeight;
	    	s.scaleX = 2.0f;
	    	s.scaleY = 0.5f;
	    	s.rotation = 143;
	  	Futile.stage.AddChild(s);

		// This will log the original values of the vertices
		Vector2[] originalVertices = s.polygonData.polygonVertices;
		for (int i = 0; i < originalVertices.Length; i++) {
			Vector2 v = originalVertices[i];
			Debug.Log("Original vertex " + i + ": " + v);
		}

		Debug.Log("===================================================================================================");

		// This will log the values of the vertices after taking rotation, scale, and position into account
		Vector2[] adjustedVertices = s.GetAdjustedPolygonData().polygonVertices;
		for (int i = 0; i < adjustedVertices.Length; i++) {
			Vector2 v = adjustedVertices[i];
			Debug.Log("Adjusted vertex " + i + ": " + v);
		}

		int circleResolution = 50;
		float radius = 50;
		Vector2[] circleVertices = new Vector2[circleResolution];
		for (int i = 0; i < circleResolution; i++) {
			float x = Mathf.Cos(2 * Mathf.PI / circleResolution * (i + 1)) * radius;
			float y = Mathf.Sin(2 * Mathf.PI / circleResolution * (i + 1)) * radius;
			circleVertices[i] = new Vector2(x, y);
		}

		WTPolygonSprite sCircle = new WTPolygonSprite(new WTPolygonData(circleVertices));
		sCircle.color = Color.cyan;
		sCircle.x = Futile.screen.halfWidth + 100;
		sCircle.y = Futile.screen.halfHeight;
		Futile.stage.AddChild(sCircle);
	}
}