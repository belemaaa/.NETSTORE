using System;
using _netstore.Models;

namespace _netstore.Interfaces
{
	public interface IOwnerRepository
	{
		ICollection<Owner> GetOwners();

		Owner GetOwner(int ownerId);

		bool AddOwner(Owner owner);

		bool OwnerExists(int ownerId);

		bool Save();
	}
}

