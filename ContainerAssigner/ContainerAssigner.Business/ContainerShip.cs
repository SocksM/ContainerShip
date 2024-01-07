using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerAssigner.Business
{
	public class ContainerShip
	{
		public List<ContainerStackColumn> ContainerStackColumns { get; private set; }

		private int LongestColumnCount
		{
			get
			{
				int longestColumnCount = 0;
				foreach (ContainerStackColumn containerStackColumn in ContainerStackColumns)
				{
					if (longestColumnCount < containerStackColumn.ContainerStacks.Count)
					{
						longestColumnCount = containerStackColumn.ContainerStacks.Count;
					}
				}
				return longestColumnCount;
			}
		}

		public int TotalCapacity
		{
			get
			{
				return (Container.FullContainerWeight + Container.ContainerCrushWeight) * LongestColumnCount * ContainerStackColumns.Count;
			}
		}

		public void AddContainer(Container container)
		{
			throw new System.NotImplementedException();
		}

		private void AddColumn(Placement placement)
		{
			switch (placement)
			{
				case Placement.Left:
					ContainerStackColumns.Insert(0, new ContainerStackColumn());
					return;
				case Placement.Middle:
					if (int.IsEvenInteger(ContainerStackColumns.Count))
					{
						ContainerStackColumns.Insert(ContainerStackColumns.Count / 2, new ContainerStackColumn());
					}
					else
					{
						throw new Exception("Can't insert column in the middle since the interger isn't even so there is no middle.");
					}
					return;
				case Placement.Right:
					ContainerStackColumns.Add(new ContainerStackColumn());
					return;
				case Placement.LeftRight:
					ContainerStackColumns.Insert(0, new ContainerStackColumn());
					ContainerStackColumns.Add(new ContainerStackColumn());
					return;
			}
		}

		private void AddEmptyStackAtColumn(int containerStackCollumnIndex)
		{
			ContainerStackColumns[containerStackCollumnIndex].AddEmptyStack();
		}

		private void StandarizeRowCount()
		{
			int longestColumnCount = LongestColumnCount; // reason for doing this is so it doesnt run the for loop everytime and the search scope stays smaller.
			for (int i = 0; i < ContainerStackColumns.Count; i++)
			{
				for (int j = 0; j < longestColumnCount - ContainerStackColumns[i].ContainerStacks.Count; j++)
				{
					AddEmptyStackAtColumn(i);
				}
			}
		}
	}
}