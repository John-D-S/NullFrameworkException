using System.Collections;
using System.Collections.Generic;

using Unity.Collections;

using UnityEditor;

using UnityEngine;

namespace NullFrameworkException.Test
{
	public class AttributeTests : MonoBehaviour
	{
		[Tag, SerializeField] private string playerTag;
		[Tag, SerializeField] private string finishTag = "Finish";

		[SerializeField, ReadOnly] private string thing = "1234";

		[SerializeField, SceneField] private string testLevel;
	}
}
