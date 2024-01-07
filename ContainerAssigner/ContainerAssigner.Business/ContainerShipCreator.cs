using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerAssigner.Business
{
	public class ContainerShipCreator
	{
		public ContainerShipCreator(List<Container> ContainersToBeInserted)
		{
			NotInsertedContainers = ContainersToBeInserted;
			ContainerShip = new ContainerShip();

			SortContainers();

			while (ContainersToBeInserted.Count > 0)
			{
				InsertConainer(ContainersToBeInserted.First());
			}
		}

		public List<Container> NotInsertedContainers { get; set; }

		public ContainerShip ContainerShip { get; private set; }

		private void SortContainers()
		{
			throw new NotImplementedException();
		}

		private void InsertConainer(Container container)
		{
			ContainerShip.AddContainer(container);
		}
	}
}