using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerAssigner.Business
{
	public class Container
	{

		/// <summary>
		/// between 4 tons (empty) and 30 tons (fully filled)
		/// </summary>
		public int Weight { get; private set; }
		
		/// <summary>
		/// Type of the container possibilities: Regular, Cooled or Valuable
		/// </summary>
		public ContainerType Type { get; private set; }

		/// <summary>
		/// amounth of weight that can be stacked ontop of this container before this container carrying the weight breaks/gets crushed
		/// </summary>
		public static readonly int ContainerCrushWeight = 120_000;

		/// <summary>
		/// How much a container without content weighs
		/// </summary>
		public static readonly int EmptyContainerWeight = 4_000;

		/// <summary>
		/// How much a full container weighs
		/// </summary>
		public static readonly int FullContainerWeight = 30_000;

		public Container(ContainerType type, int weight)
		{
			if (!CheckIfWeightIsValid(weight))
			{
				throw new ArgumentException($"Weight Invalid (should be 4000 - 30000, was: {weight})");
			}
			Weight = weight;
			Type = type;
		}

		public bool CheckIfWeightIsValid(int weight)
		{
			return weight >= EmptyContainerWeight && weight <= FullContainerWeight;
		}
	}
}