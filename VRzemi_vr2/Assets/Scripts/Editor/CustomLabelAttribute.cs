using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLabelAttribute : PropertyAttribute {
	public readonly string _value;

	public CustomLabelAttribute( string value ) { 
		_value = value;
	}
}

//正直なにしてるかわからん