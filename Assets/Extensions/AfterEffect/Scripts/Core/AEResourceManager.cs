////////////////////////////////////////////////////////////////////////////////
//  
// @module Affter Effect Importer
// @author Osipov Stanislav lacost.st@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class AEResourceManager  {


	public static AEFootage CreateFootage() {

		return (Object.Instantiate(Resources.Load("AEFootage")) as GameObject).GetComponent<AEFootage>();
	}


	public static AEComposition CreateComposition() {
		AEComposition comp = new GameObject ("AEComposition").AddComponent<AEComposition> ();

		GameObject p =  new GameObject("Composition");
		p.transform.parent = comp.transform;
		p.transform.localPosition = Vector3.zero;
		p.transform.localScale = Vector3.one;

		comp.plane = p.transform;

		return comp;
	}
}
