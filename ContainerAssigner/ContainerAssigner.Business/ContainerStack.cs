namespace ContainerAssigner.Business
{
	public class ContainerStack
	{
		/// <summary>
		/// index 0 is bottom of the container stack
		/// </summary>
		public List<Container> Containers { get; private set; }

		public ContainerStack()
		{
			Containers = new List<Container>();
		}

		public int GetTotalWeight()
		{
			int totalWeight = 0;
			foreach (Container container in Containers)
			{
				totalWeight += container.Weight;
			}
			return totalWeight;
		}

		public int GetWeightOntopOfBottomContainer()
		{
			int WeightOntopOfBottomContainer = 0;
			if (Containers.Count == 0)
			{
				throw new Exception("No containers in stack.");
			}
			for (int i = 1; i < Containers.Count; i++)
			{
				WeightOntopOfBottomContainer += Containers[i].Weight;
			}
			return WeightOntopOfBottomContainer;
		}

		public bool IsThereAValuableContainer()
		{
			foreach (Container container in Containers)
			{
				if (container.Type == ContainerType.Valuable)
				{
					return true;
				}
			}
			return false;
		}

		public bool CanContainerGoInStack(Container container)
		{
			if (container.Type == ContainerType.Valuable && IsThereAValuableContainer())
			{
				return false;
			}
			if (GetWeightOntopOfBottomContainer() + container.Weight > Container.ContainerCrushWeight)
			{
				return false;
			}
			return true;
		}

		public void TryAddContainer(Container container)
		{
			if (container.Type == ContainerType.Valuable && IsThereAValuableContainer())
			{
				throw new Exception("Already a Valuable container present.");
				// throws error since a valuable container cant have another container ontop and if there are 2 in a single stack this can't be the case. 
			}
			if (GetWeightOntopOfBottomContainer() + container.Weight > Container.ContainerCrushWeight) 
			{
				throw new Exception("Maximum crush weight of bottom and possible containers ontop of that exceeded.");
				// throws error since a container will succumb to a certain amount of weight placed upon it.
			}
			// i'm aware this couldve been done by calling CanContinerGoInStack(container) but then I can't give a non generic error now I can add a proper message
			
			if (container.Type == ContainerType.Valuable)
			{
				Containers.Add(container);
				return;
			} 
			else
			{
				Containers.Insert(0, container);
				return;
			}
		}
	}
}