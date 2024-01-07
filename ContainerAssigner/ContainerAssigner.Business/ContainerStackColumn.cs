using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerAssigner.Business
{
	public class ContainerStackColumn
	{
		/// <summary>
		/// index 0 is the first container stack from the front of the ship.
		/// </summary>
		public List<ContainerStack> ContainerStacks { get; private set; }

		public ContainerStackColumn()
		{
			ContainerStacks = new List<ContainerStack>();
		}

		public void AddEmptyStack()
		{
			ContainerStacks.Add(new ContainerStack());
		}

		public void TryAddContainerWithoutMakingNewRow(Container container)
		{
			for (int i = ContainerStacks.Count; i < 0; i--)
			{
				if (ContainerStacks[i].CanContainerGoInStack(container))
				{
					if (IsThereNoConflicitingValuableContainer(i))
					{
						ContainerStacks[i].TryAddContainer(container);
					}
				}
			}
			throw new Exception("Couldn't add container to already existing stack.");
		}

		public void TryAddContainer(Container container)
		{
			try
			{
				TryAddContainerWithoutMakingNewRow(container);
			}
			catch (Exception exception)
			{
				if (exception.Message == "Couldn't add container to already existing stack.")
				{
					ContainerStacks.Add(new ContainerStack());
					if (IsThereNoConflicitingValuableContainer(ContainerStacks.Count - 1))
					{
						ContainerStacks[ContainerStacks.Count - 1].TryAddContainer(container);
					}
					else
					{
						ContainerStacks.Add(new ContainerStack());
						ContainerStacks[ContainerStacks.Count - 1].TryAddContainer(container);
					}
				}
				else
				{
					throw;
				}
			}
		}

		public int GetTotalWeight()
		{
			int totalWeight = 0;
			foreach (ContainerStack containerStack in ContainerStacks)
			{
				totalWeight += containerStack.GetTotalWeight();
			}
			return totalWeight;
		}

		public bool CanContainerGoInColumnWithoutMakingNewRow(Container container)
		{
			switch (container.Type)
			{
				case ContainerType.Regular:
					for (int i = 0; i < ContainerStacks.Count; i++)
					{
						if (IsThereNoConflicitingValuableContainer(i))
						{
							if (ContainerStacks[i].CanContainerGoInStack(container))
							{
								return true;
							}
						}
					}
					return false;
				case ContainerType.Valuable:
					for (int i = 0; i < ContainerStacks.Count; i++)
					{
						if (IsThereNoConflicitingValuableContainer(i))
						{
							if (ContainerStacks[i].CanContainerGoInStack(container))
							{
								return true;
							}
						}
					}
					return false;
				case ContainerType.Coolable:
					if (!IsThereNoConflicitingValuableContainer(0))
					{
						return false;
					}
					return ContainerStacks[0].CanContainerGoInStack(container);
			}
			throw new Exception("No or unkown container type given (?)");
		}

		public bool CanContainerGoInColumn(Container container)
		{
			if (container.Type == ContainerType.Coolable)
			{
				int NewContainerStackCount = ContainerStacks[0].Containers.Count + 1;
				if (!ContainerStacks[0].CanContainerGoInStack(container))
				{
					return false;
				}
				if (ContainerStacks.Count >= 2)
				{
					if (ContainerStacks[1].IsThereAValuableContainer())
					{
						if (NewContainerStackCount >= ContainerStacks[1].Containers.Count)
						{
							return false;
						}
					}
					return ContainerStacks[0].CanContainerGoInStack(container);
				}
				return true;
				// im aware this line isnt needed but i want it to be here for readability 
			}
			return true;
		}

		public bool IsThereNoConflicitingValuableContainer(int indexToPutContainerIn)
		{
			int maxListIndex = ContainerStacks.Count - 1;
			if (maxListIndex + 1 < indexToPutContainerIn)
			{
				return true;
			}
			if (maxListIndex >= indexToPutContainerIn + 1)
			{
				if (ContainerStacks[indexToPutContainerIn + 1].IsThereAValuableContainer())
				{
					if (ContainerStacks[indexToPutContainerIn].Containers.Count + 1 >= ContainerStacks[indexToPutContainerIn + 1].Containers.Count)
					{
						return false;
					}
				}
			}
			if (indexToPutContainerIn > 0)
			{
				if (ContainerStacks[indexToPutContainerIn - 1].IsThereAValuableContainer())
				{
					if (ContainerStacks[indexToPutContainerIn].Containers.Count + 1 >= ContainerStacks[indexToPutContainerIn - 1].Containers.Count)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}