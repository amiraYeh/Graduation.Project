using GP.Focusi.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Repository.Data.Configurations
{
	public class ChildClassConfigurations : IEntityTypeConfiguration<ChildClass>
	{
		public void Configure(EntityTypeBuilder<ChildClass> builder)
		{
			builder.OwnsOne(C => C.Game, G => G.WithOwner());
			builder.OwnsOne(C => C.Video, V => V.WithOwner());

		}
	}
}
