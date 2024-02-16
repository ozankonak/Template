using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ParticleStrategy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Containers
{
	[CreateAssetMenu(menuName = "Container/ParticleContainer", fileName = "ParticleContainer", order = 1)]
	public class ParticleContainer : ScriptableObject
	{
		[FormerlySerializedAs("GameStartParticle")] [Header("Basic Particles")] 
		public ParticleStarter SheepCollectParticle;

		public List<ParticleStarter> GetAllParticles()
		{
			var fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return fields
				.Where(field => typeof(ParticleStarter).IsAssignableFrom(field.FieldType))
				.Select(field => (ParticleStarter)field.GetValue(this))
				.ToList();
		}
	}
}